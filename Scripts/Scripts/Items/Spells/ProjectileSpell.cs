using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Spells/Projectile Spell")]
    public class ProjectileSpell : SpellItem
    {
        [Header("Projectile Damage")]
        public float baseDamage;

        [Header("Projectile Physics")]
        Rigidbody rigidbody;
        public float projectileMass;
        public bool isAffectedByGravity;
        public float projectileUpwardVelocity;
        public float projectileForwardVelocity;

        public override void AttemptToCastSpell(CharacterManager characterManager, bool isLeftHanded)
        {
            base.AttemptToCastSpell(characterManager, isLeftHanded);
            if(isLeftHanded)
            {
                GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, characterManager.GetLeftHandSlot().transform);
                characterManager.PlayTargetAnimation(spellAnimation, true, false, isLeftHanded);
            }
            else
            {
                GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, characterManager.GetRightHandSlot().transform);
                characterManager.PlayTargetAnimation(spellAnimation, true, false, isLeftHanded);
            }
        }

        public override void SuccessfullyCastedSpell(CharacterManager characterManager, bool isLeftHanded)
        {
            base.SuccessfullyCastedSpell(characterManager, isLeftHanded);
            PlayerManager playerManager = characterManager as PlayerManager;
            if(playerManager != null)
            {
                if (isLeftHanded)
                {
                    GameObject instantiatedSpellFX = Instantiate(spellCastFX, playerManager.GetLeftHandSlot().transform.position, playerManager.cameraHandler.cameraPivotTransform.rotation);
                    SpellDamageCollider spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
                    spellDamageCollider.teamID = playerManager.GetTeamID();
                    rigidbody = instantiatedSpellFX.GetComponent<Rigidbody>();
                    //spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
                    if (playerManager.cameraHandler.currentLockOnTarget != null)
                    {
                        instantiatedSpellFX.transform.LookAt(playerManager.cameraHandler.currentLockOnTarget.transform);
                    }
                    else
                    {
                        //this makes it such that the fireball is thrown in wherever the player is facing on the x plane, as well as whereever the player is facing on the y plane
                        instantiatedSpellFX.transform.rotation = Quaternion.Euler(playerManager.cameraHandler.cameraPivotTransform.eulerAngles.x, playerManager.transform.eulerAngles.y, 0);
                    }
                    rigidbody.AddForce(instantiatedSpellFX.transform.forward * projectileForwardVelocity);
                    rigidbody.AddForce(instantiatedSpellFX.transform.up * projectileUpwardVelocity);
                    rigidbody.useGravity = isAffectedByGravity;
                    rigidbody.mass = projectileMass;
                    instantiatedSpellFX.transform.parent = null; //unparent from parents game object
                }
                else
                {
                    GameObject instantiatedSpellFX = Instantiate(spellCastFX, playerManager.GetRightHandSlot().transform.position, playerManager.cameraHandler.cameraPivotTransform.rotation);
                    SpellDamageCollider spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
                    spellDamageCollider.teamID = playerManager.GetTeamID();
                    rigidbody = instantiatedSpellFX.GetComponent<Rigidbody>();
                    //spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
                    if (playerManager.cameraHandler.currentLockOnTarget != null)
                    {
                        instantiatedSpellFX.transform.LookAt(playerManager.cameraHandler.currentLockOnTarget.transform);
                    }
                    else
                    {
                        //this makes it such that the fireball is thrown in wherever the player is facing on the x plane, as well as whereever the player is facing on the y plane
                        instantiatedSpellFX.transform.rotation = Quaternion.Euler(playerManager.cameraHandler.cameraPivotTransform.eulerAngles.x, playerManager.transform.eulerAngles.y, 0);
                    }
                    rigidbody.AddForce(instantiatedSpellFX.transform.forward * projectileForwardVelocity);
                    rigidbody.AddForce(instantiatedSpellFX.transform.up * projectileUpwardVelocity);
                    rigidbody.useGravity = isAffectedByGravity;
                    rigidbody.mass = projectileMass;
                    instantiatedSpellFX.transform.parent = null; //unparent from parents game object
                }
            }
        }
    }
}

