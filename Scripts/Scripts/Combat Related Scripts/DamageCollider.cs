using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JJ
{
    public class DamageCollider : MonoBehaviour
    {
        public CharacterManager characterManager;
        protected Collider damageCollider;
        public bool enabledDamageColliderOnStartUp = false;
        public List<CharacterManager> listOfCharactersAlreadyHit = new List<CharacterManager>();

        [Header("Team I.D")]
        public int teamID = 0;
        public List<Allies> allies = new List<Allies>();

        [Header("Poise")]
        public float poiseBreak;
        public float offensePoiseBonus;

        [Header("Damage")]
        public int physicalDamage;
        public int magicDamage;
        public int fireDamage;
        public int lightningDamage;
        public int umbraDamage;
        public int holyDamage;

        [Header("Guard Break Modifier")]
        public float guardBreakModifier;

        protected bool shieldHasBeenHit;
        protected bool hasBeenParried;
        protected string currentDamageAnimation;

        protected virtual void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.enabled = enabledDamageColliderOnStartUp;
            damageCollider.isTrigger = true;
            if(damageCollider == null)
            {
                Debug.Log("damageCollider is Null");
            }
        }

        /// <summary>
        /// Animation event to allow weapon to deal damage.
        /// Every enable clears the list of characters hit already hit by the damage collider.
        /// </summary>
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
            listOfCharactersAlreadyHit.Clear();
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        protected virtual void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Character")
            {

                CharacterManager targetCharacterManager = collision.GetComponent<CharacterManager>();


                if (targetCharacterManager != null)
                {
                    //To prevent multiple hits from the same attack instance
                    if (listOfCharactersAlreadyHit.Contains(targetCharacterManager))
                    {
                        return;
                    }
                    else
                    {
                        listOfCharactersAlreadyHit.Add(targetCharacterManager);
                    }

                    if (targetCharacterManager.GetTeamID() == teamID)
                        return;
                    hasBeenParried = CheckForParry(targetCharacterManager);
                    shieldHasBeenHit = CheckForBlock(targetCharacterManager);

                    Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position); ;
                    float damageDirection = (Vector3.SignedAngle(characterManager.transform.forward, targetCharacterManager.transform.forward, Vector3.up));
                    if (targetCharacterManager.GetTeamID() == teamID)
                        return;
                    if (hasBeenParried)
                        return;
                    if (shieldHasBeenHit)
                    {
                        targetCharacterManager.PlayBlockSparksFX(contactPoint);
                        return;
                    }
                    targetCharacterManager.ResetPoiseTimer();
                    float targetTotalPoise = targetCharacterManager.GetTotalPoise();
                    targetTotalPoise -= poiseBreak;
                    targetCharacterManager.SetTotalPoise(targetTotalPoise);
                    //Spawn blood splatter
                    ChooseDamageDirection(damageDirection);
                    if(targetCharacterManager.IsInvulnerable() == false)
                    {
                        targetCharacterManager.PlayBloodSplatterFX(contactPoint);
                    }
                    DealDamage(targetCharacterManager);

                }
            }

            if(collision.tag == "Illusionary Wall")
            {
                IllusionaryWall illusionaryWall = collision.GetComponent<IllusionaryWall>();
                illusionaryWall.wallHasBeenHit = true;
            }
        }

        public void SetDamageColliderOffensiveValues(int weaponPhysicalDamage, int weaponMagicDamage, int weaponFireDamage, int weaponLightningDamage, int weaponUmbraDamage, int weaponHolyDamage, float weaponPoiseBreak)
        {
            physicalDamage = weaponPhysicalDamage;
            magicDamage = weaponMagicDamage;
            fireDamage = weaponFireDamage;
            lightningDamage = weaponLightningDamage;   
            umbraDamage = weaponUmbraDamage;
            holyDamage = weaponHolyDamage;
            poiseBreak = weaponPoiseBreak;
        }

        public void SetDamageColliderOwner(CharacterManager owner)
        {
            characterManager = owner;
            teamID = owner.GetTeamID();
            allies = owner.GetAllies();
        }

        protected virtual void ChooseDamageDirection(float direction)
        {
            Debug.Log(direction);
            if(direction >= 145 && direction <= 180)
            {
                currentDamageAnimation = "Damage_Forward_01";
            }
            else if(direction <= -145 && direction >= -180)
            {
                currentDamageAnimation = "Damage_Forward_01";
            }
            else if(direction >= -45 && direction <= 45)
            {
                currentDamageAnimation = "Damage_Back_01";
            }
            else if(direction >= -144 && direction <= -45)
            {
                currentDamageAnimation = "Damage_Left_01";
            }
            else if(direction >= 45 && direction <= 144)
            {
                currentDamageAnimation = "Damage_Right_01";
            }
        }

        /// <summary>
        /// Method to check if target is parrying while being hit by the characterManager of this DamageCollider<br />
        /// If target is parrying, the characterManager of this DamageCollider will play a Parried Animation
        /// </summary>
        /// <param name="targetCharacterManager"></param>
        /// <returns>True if character is parrying, false if they are not</returns>
        protected virtual bool CheckForParry(CharacterManager targetCharacterManager)
        {
            if (targetCharacterManager.isParrying)
            {
                characterManager.PlayTargetAnimation("Parried", true);
                //hasBeenParried = true;
                return true;
            }
            return false;
        }

        protected virtual void DealDamage(CharacterManager target)
        {
            float finalPhysicalDamage = physicalDamage;
            float finalMagicDamage = magicDamage;
            float finalFireDamage = fireDamage;
            float finalLightningDamage = lightningDamage;
            float finalUmbraDamage = umbraDamage;
            float finalHolyDamage = holyDamage;

            WeaponItem currentWeapon = characterManager.GetCurrentWeapon();
            switch(characterManager.GetCurrentAttackType())
            {
                case AttackType.lightAttack01:
                    finalPhysicalDamage *= currentWeapon.lightAttack01DamageModifier;
                    finalMagicDamage *= currentWeapon.lightAttack01DamageModifier;
                    finalFireDamage *= currentWeapon.lightAttack01DamageModifier;
                    finalLightningDamage *= currentWeapon.lightAttack01DamageModifier;
                    finalUmbraDamage *= currentWeapon.lightAttack01DamageModifier;
                    finalMagicDamage *= currentWeapon.lightAttack01DamageModifier;
                    finalHolyDamage *= currentWeapon.lightAttack01DamageModifier;
                    break;

                case AttackType.lightAttack02:
                    finalPhysicalDamage *= currentWeapon.lightAttack02DamageModifier;
                    finalMagicDamage *= currentWeapon.lightAttack02DamageModifier;
                    finalFireDamage *= currentWeapon.lightAttack02DamageModifier;
                    finalLightningDamage *= currentWeapon.lightAttack02DamageModifier;
                    finalUmbraDamage *= currentWeapon.lightAttack02DamageModifier;
                    finalHolyDamage *= currentWeapon.lightAttack02DamageModifier;
                    break;

                case AttackType.heavyAttack01:
                    finalPhysicalDamage *= currentWeapon.heavyAttack01DamageModifier;
                    finalMagicDamage *= currentWeapon.heavyAttack01DamageModifier;
                    finalFireDamage *= currentWeapon.heavyAttack01DamageModifier;
                    finalLightningDamage *= currentWeapon.heavyAttack01DamageModifier;
                    finalUmbraDamage *= currentWeapon.heavyAttack01DamageModifier;
                    finalHolyDamage *= currentWeapon.heavyAttack01DamageModifier;
                    break;

                case AttackType.heavyAttack02:
                    finalPhysicalDamage *= currentWeapon.heavyAttack02DamageModifier;
                    finalMagicDamage *= currentWeapon.heavyAttack02DamageModifier;
                    finalFireDamage *= currentWeapon.heavyAttack02DamageModifier;
                    finalLightningDamage *= currentWeapon.heavyAttack02DamageModifier;
                    finalUmbraDamage *= currentWeapon.heavyAttack02DamageModifier;
                    finalHolyDamage *= currentWeapon.heavyAttack02DamageModifier;
                    break;
                
                default:
                    finalPhysicalDamage = physicalDamage;
                    finalMagicDamage = magicDamage;
                    finalFireDamage = fireDamage;
                    finalLightningDamage = lightningDamage;
                    finalUmbraDamage = umbraDamage;
                    finalHolyDamage = holyDamage;
                    break;
            }
            
            if (target.GetTotalPoise() > poiseBreak)
            {
                target.TakeDamageNoAnimation(characterManager, Mathf.RoundToInt(finalPhysicalDamage), Mathf.RoundToInt(finalMagicDamage), Mathf.RoundToInt(finalFireDamage), Mathf.RoundToInt(finalLightningDamage), Mathf.RoundToInt(finalUmbraDamage), Mathf.RoundToInt(finalHolyDamage));
            }
            else
            {
                if (target is EnemyManager)
                {
                    EnemyManager enemyManager = (EnemyManager)target;
                    if (enemyManager.isBoss)
                    {
                        target.PlayTargetAnimation("Parried", true);
                        target.ResetTotalPoise();
                        return;
                    }
                }
                target.TakeDamage(characterManager, Mathf.RoundToInt(finalPhysicalDamage), Mathf.RoundToInt(finalMagicDamage), Mathf.RoundToInt(finalFireDamage), Mathf.RoundToInt(finalLightningDamage), Mathf.RoundToInt(finalUmbraDamage), Mathf.RoundToInt(finalHolyDamage), currentDamageAnimation);
            }
        }
        
        /// <summary>
        /// Method to check if target is blocking while being hit by this characterManager of this DamageCollider.
        /// </summary>
        /// <param name="target"></param>
        /// <returns>True if target is blocking, false if they are not </returns>
        protected virtual bool CheckForBlock(CharacterManager target)
        {
            Vector3 directionFromCharacterToTarget = (characterManager.transform.position - target.transform.position);
            float dotValueFromCharacterToTarget = Vector3.Dot(directionFromCharacterToTarget, target.transform.forward);
            if (target.isBlocking && dotValueFromCharacterToTarget > 0.3f)
            {
                //shieldHasBeenHit = true;
                int physicalDamageAfterBlock = Mathf.RoundToInt(physicalDamage - (physicalDamage * target.GetGuardedPhysicalDamageNegation()) / 100);
                int magicDamageAfterBlock = Mathf.RoundToInt(magicDamage - (magicDamage * target.GetGuardedMagicDamageNegation()) / 100);
                int fireDamageAfterBlock = Mathf.RoundToInt(fireDamage - (fireDamage * target.GetGuardedFireDamageNegation()) / 100);
                int lightningDamageAfterBlock = Mathf.RoundToInt(lightningDamage - (lightningDamage * target.GetGuardedLightningDamageNegation()) / 100);
                int umbraDamageAfterBlock = Mathf.RoundToInt(umbraDamage - (umbraDamage * target.GetGuardedUmbraDamageNegation()) / 100);
                int holyDamageAfterBlock = Mathf.RoundToInt(holyDamage - (holyDamage* target.GetGuardedHolyDamageNegation()) / 100);
                target.AttemptBlock(this, physicalDamage, fireDamage, lightningDamage, umbraDamage, magicDamage);
                target.TakeDamageAfterBlocking(characterManager, physicalDamageAfterBlock, magicDamageAfterBlock, fireDamageAfterBlock, lightningDamageAfterBlock, umbraDamageAfterBlock, holyDamageAfterBlock);
                return true;
            }
            return false;
        }
    }
}

