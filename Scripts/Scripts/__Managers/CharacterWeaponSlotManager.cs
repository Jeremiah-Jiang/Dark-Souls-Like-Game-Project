using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class CharacterWeaponSlotManager : MonoBehaviour
    {
        protected CharacterManager characterManager;

        [Header("Unarmed Weapon")]
        public WeaponItem unarmedWeapon;

        [Header("Weapon Slots")]
        public WeaponHolderSlot leftHandSlot;
        public WeaponHolderSlot rightHandSlot;
        public WeaponHolderSlot backSlot;
        public WeaponHolderSlot shieldSlot;
        public WeaponHolderSlot bowSlot;

        [Header("Damage Colliders")]
        public DamageCollider leftHandDamageCollider;
        public DamageCollider rightHandDamageCollider;

        [Header("Hand IK Targets")]
        public RightHandIKTarget rightHandIKTarget;
        public LeftHandIKTarget leftHandIKTarget;
        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
            LoadWeaponHolderSlots();
        }
        protected virtual void LoadWeaponHolderSlots()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();

            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
                else if (weaponSlot.isBackSlot)
                {
                    backSlot = weaponSlot;
                }
                else if (weaponSlot.isShieldSlot)
                {
                    shieldSlot = weaponSlot;
                }
                else if (weaponSlot.isBowSlot)
                {
                    bowSlot = weaponSlot;
                }
            }
        }

        public virtual void LoadBothWeaponsOnSlots()
        {
            LoadWeaponOnSlot(characterManager.GetRightWeapon(), false);
            LoadWeaponOnSlot(characterManager.GetLeftWeapon(), true);
        }

        public virtual void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (weaponItem != null)
            {
                if (isLeft)
                {
                    leftHandSlot.currentWeapon = weaponItem;
                    leftHandSlot.LoadWeaponModel(weaponItem);
                    LoadLeftWeaponDamageCollider();
                    //characterManager.PlayTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
                }
                else
                {
                    if (characterManager.isTwoHanding)
                    {
                        if (leftHandSlot.currentWeapon.weaponType == WeaponType.StraightSword)
                        {
                            backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                        }
                        else if (leftHandSlot.currentWeapon.weaponType == WeaponType.Shield)
                        {
                            shieldSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                        }
                        else if(leftHandSlot.currentWeapon.weaponType == WeaponType.Bow)
                        {
                            bowSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                        }
                        //Move current left hand weapon to the back or disable it
                        leftHandSlot.UnloadWeaponAndDestroy();
                        characterManager.PlayTargetAnimation("Left Arm Empty", false, true);
                    }
                    else
                    {
                        backSlot.UnloadWeaponAndDestroy();
                        shieldSlot.UnloadWeaponAndDestroy();
                    }
                    rightHandSlot.currentWeapon = weaponItem;
                    rightHandSlot.LoadWeaponModel(weaponItem);
                    LoadRightWeaponDamageCollider();
                    //LoadTwoHandIKTargets(characterManager.isTwoHanding);
                    characterManager.SetAnimatorOverrideController(weaponItem.weaponController);
                }
            }
            else
            {
                weaponItem = unarmedWeapon;

                if (isLeft)
                {
                    characterManager.SetLeftWeapon(weaponItem);
                    characterManager.SetCurrentLeftWeaponIdx(-1);
                    leftHandSlot.currentWeapon = unarmedWeapon;
                    leftHandSlot.LoadWeaponModel(weaponItem);
                    LoadLeftWeaponDamageCollider();
                    //characterManager.PlayTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
                }
                else
                {
                    characterManager.SetRightWeapon(weaponItem);
                    characterManager.SetCurrentRightWeaponIdx(-1);
                    rightHandSlot.currentWeapon = unarmedWeapon;
                    rightHandSlot.LoadWeaponModel(weaponItem);
                    LoadRightWeaponDamageCollider();
                    characterManager.SetAnimatorOverrideController(weaponItem.weaponController);
                }
            }
        }

        /// <summary>
        /// Function to load left hand weapon damage collider's owner and offensive values:<br />
        /// Physical Damage<br />
        /// Fire Damage<br />
        /// Lightning Damage<br />
        /// Umbra Damage<br />
        /// Magic Damage<br />
        /// Poise Break
        /// </summary>
        protected virtual void LoadLeftWeaponDamageCollider()
        {
            WeaponItem leftHandWeapon = characterManager.GetLeftWeapon();
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            leftHandDamageCollider.SetDamageColliderOwner(characterManager);
            leftHandDamageCollider.SetDamageColliderOffensiveValues(leftHandWeapon.GetPhysicalDamage(), 
                leftHandWeapon.GetMagicDamage(), 
                leftHandWeapon.GetFireDamage(), 
                leftHandWeapon.GetLightningDamage(), 
                leftHandWeapon.GetUmbraDamage(), 
                leftHandWeapon.GetHolyDamage(), 
                leftHandWeapon.poiseBreak);
            characterManager.SetLeftWeaponFX(leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>());
        }

        /// <summary>
        /// Function to load right hand weapon damage collider's owner and offensive values:<br />
        /// Physical Damage<br />
        /// Fire Damage<br />
        /// Lightning Damage<br />
        /// Umbra Damage<br />
        /// Magic Damage<br />
        /// Poise Break
        /// </summary>
        protected virtual void LoadRightWeaponDamageCollider()
        {
            WeaponItem rightHandWeapon = characterManager.GetRightWeapon();
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            rightHandDamageCollider.SetDamageColliderOwner(characterManager);
            rightHandDamageCollider.SetDamageColliderOffensiveValues(rightHandWeapon.GetPhysicalDamage(), 
                rightHandWeapon.GetMagicDamage(), 
                rightHandWeapon.GetFireDamage(), 
                rightHandWeapon.GetLightningDamage(), 
                rightHandWeapon.GetUmbraDamage(), 
                rightHandWeapon.GetHolyDamage(),
                rightHandWeapon.poiseBreak);
            characterManager.SetRightWeaponFX(rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>());
        }

        public virtual void LoadTwoHandIKTargets(bool isTwoHanding)
        {
            leftHandIKTarget = rightHandSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
            rightHandIKTarget = rightHandSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
            characterManager.SetHandIKForWeapon(rightHandIKTarget, leftHandIKTarget, isTwoHanding);
        }

        public virtual void OpenDamageCollider() //This is an animation event, keep as public
        {
            //character.characterSoundFXManager.PlayRandomWeaponWhoosh();
            if (characterManager.isUsingRightHand)
            {
                rightHandDamageCollider.EnableDamageCollider();
            }
            if (characterManager.isUsingLeftHand)
            {
                leftHandDamageCollider.EnableDamageCollider();
            }
        }

        public virtual void CloseDamageCollider() //This is an animation event, keep as public
        {
            if (characterManager.isUsingRightHand)
            {
                rightHandDamageCollider.DisableDamageCollider();
            }
            if (characterManager.isUsingLeftHand)
            {
                leftHandDamageCollider.DisableDamageCollider();
            }
        }

        #region Handle Weapon's Poise Bonus
        public virtual void GrantWeaponAttackingPoiseBonus() //This is an animation event, keep as public
        {
            WeaponItem currentWeaponBeingUsed = characterManager.GetCurrentWeapon();
            float poiseDefense = characterManager.GetTotalPoise();
            poiseDefense += currentWeaponBeingUsed.offensivePoiseBonus;
            characterManager.SetTotalPoise(poiseDefense);
        }

        public virtual void ResetWeaponAttackingPoiseBonus() //This is an animation event, keep as public
        {
            WeaponItem currentWeaponBeingUsed = characterManager.GetCurrentWeapon();
            float poiseDefense = characterManager.GetTotalPoise();
            poiseDefense -= currentWeaponBeingUsed.offensivePoiseBonus;
            characterManager.SetTotalPoise(poiseDefense);
        }
        #endregion

    }
}
