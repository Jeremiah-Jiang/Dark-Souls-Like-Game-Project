using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class PlayerWeaponSlotManager : CharacterWeaponSlotManager
    {
        PlayerManager playerManager;

        protected override void Awake()
        {
            base.Awake();
            playerManager = GetComponent<PlayerManager>();
        }

        /// <summary>
        /// Function to load the parameter weaponItem on the WeaponHolderSlot determined by the parameter isLeft.
        /// </summary>
        /// <param name="weaponItem">The weapon item to load</param>
        /// <param name="isLeft">Whether player is loading the weapon on the left hand slot</param>
        /* Implementation Logic:
         * If the weaponItem is not null,
         *      If the player is loading the weaponItem on the leftHandSlot, check if player is currently two handing the rightWeapon.
         *          If player is currently two handing, check for the weaponItem weapon type.
         *              If it is an unarmed weapon, remove the weapon models from the back and shield slots.
         *              Else if it is a shield, remove any weapon model from the back slot and load the weaponItem onto the shield slot.
         *              Else if it is a straight sword, remove any weapon model from the shield slot and load the weaponItem onto the back slot.
         *          If player is NOT two handing, set the current weapon of the leftHandSlot to weaponItem, load it onto the leftHandSlot
         *           and load its damage collider
         *      else if the player is loading the weaponItem on the rightHandSlot,
         *          If the two hand flag is true, check for the weapon type of the left hand weapon and load the left hand weapon onto the
         *          back/shield slot respectively.
         *          Otherwise, remove the weapon models from the back and shield slots.
         *          
         *          Set the current weapon of the rightHandSlot to weaponItem, load it onto the rightHandSlot and load its damage collider.
         *          Set the animator override controller as the right weapon's animator controller
         *          
         * If the weaponItem is null, set it as an unarmed weapon and load it onto the left/rightHandSlot respectively as well as its damagecollider.
         * Set the currentLeft/RightWeaponIndex to -1 as that is the index for unarmed weapons
         * Set the animator override controller as the unarmed weapon's animator controller
         * 
         * Update the QuickSlotUI within the if-else statements pertaining to whether or not the player is using the left/right hand
         */
        public override void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if(weaponItem != null)
            {
                if(isLeft)
                {
                    if(playerManager.IsTwoHanding() == true)
                    {
                        if (weaponItem.weaponType == WeaponType.Unarmed)
                        {
                            Debug.Log("Player is changing left weapon while two handing and the new left weapon is unarmed");
                            Debug.Log("Backslots should be empty now");
                            shieldSlot.UnloadWeaponAndDestroy();
                            backSlot.UnloadWeaponAndDestroy();
                            bowSlot.UnloadWeaponAndDestroy();
                        }
                        else if (weaponItem.weaponType == WeaponType.Shield)
                        {
                            Debug.Log("Player is changing left weapon while two handing and the new left weapon is Shield");
                            Debug.Log("Backslots should have the new shield now");
                            bowSlot.UnloadWeaponAndDestroy(); 
                            backSlot.UnloadWeaponAndDestroy();
                            shieldSlot.LoadWeaponModel(weaponItem);
                        }
                        else if(weaponItem.weaponType == WeaponType.Bow)
                        {
                            Debug.Log("Player is changing left weapon while two handing and the new left weapon is Bow");
                            Debug.Log("Backslots should have the new bow now");
                            shieldSlot.UnloadWeaponAndDestroy();
                            backSlot.UnloadWeaponAndDestroy();
                            bowSlot.LoadWeaponModel(weaponItem);
                        }
                        else if (weaponItem.weaponType == WeaponType.StraightSword)
                        {
                            Debug.Log("Player is changing left weapon while two handing and the new left weapon is Straight Sword");
                            Debug.Log("Backslots should have the new straight sword now");
                            bowSlot.UnloadWeaponAndDestroy();
                            shieldSlot.UnloadWeaponAndDestroy();
                            backSlot.LoadWeaponModelWithSheath(weaponItem);
                        }
                        leftHandSlot.currentWeapon = weaponItem;
                    }
                    else
                    {
                        Debug.Log("Player changing left weapon and is not two handing, so left hand should have the new left weapon");
                        shieldSlot.UnloadWeaponAndDestroy();
                        backSlot.UnloadWeaponAndDestroy();
                        bowSlot.UnloadWeaponAndDestroy();
                        leftHandSlot.currentWeapon = weaponItem;
                        leftHandSlot.LoadWeaponModel(weaponItem);
                        LoadLeftWeaponDamageCollider();
                        //playerManager.PlayTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
                    }
                    playerManager.uiManager.UpdateWeaponQuickSlotsUI(true, weaponItem);
                }
                else
                {
                    if (playerManager.IsTwoHanding() == true)
                    {
                        Debug.Log("Player is changing right weapon and is two handing, so back slot should have the left weapon");
                        if (leftHandSlot.currentWeapon.weaponType == WeaponType.StraightSword)
                        {
                            backSlot.LoadWeaponModelWithSheath(leftHandSlot.currentWeapon);
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
                        playerManager.PlayTargetAnimation("Left Arm Empty", false, true);
                    }
                    else
                    {
                        Debug.Log("Player is changing right weapon and is not two handing, so backslots should not have anything");
                        backSlot.UnloadWeaponAndDestroy();
                        shieldSlot.UnloadWeaponAndDestroy();
                        bowSlot.UnloadWeaponAndDestroy();
                    }
                    rightHandSlot.currentWeapon = weaponItem;
                    rightHandSlot.LoadWeaponModel(weaponItem);
                    LoadRightWeaponDamageCollider();
                    playerManager.uiManager.UpdateWeaponQuickSlotsUI(false, weaponItem);
                    playerManager.SetAnimatorOverrideController(weaponItem.weaponController);
                    Debug.Log(weaponItem.weaponController);
                }
            }
            else
            {
                weaponItem = unarmedWeapon;
                if(isLeft)
                {
                    Debug.Log("Left Weapon is unarmed");

                    shieldSlot.UnloadWeaponAndDestroy();
                    backSlot.UnloadWeaponAndDestroy();
                    bowSlot.UnloadWeaponAndDestroy();
                    playerManager.SetLeftWeapon(weaponItem);
                    playerManager.SetCurrentLeftWeaponIdx(-1);
                    leftHandSlot.currentWeapon = unarmedWeapon;
                    leftHandSlot.LoadWeaponModel(weaponItem);
                    LoadLeftWeaponDamageCollider();
                    playerManager.uiManager.UpdateWeaponQuickSlotsUI(true, weaponItem);
                    //playerManager.PlayTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
                }
                else
                {
                    Debug.Log("Right Weapon is unarmed");

                    playerManager.SetRightWeapon(weaponItem);
                    playerManager.SetCurrentRightWeaponIdx(-1);
                    rightHandSlot.currentWeapon = unarmedWeapon;
                    rightHandSlot.LoadWeaponModel(weaponItem);
                    LoadRightWeaponDamageCollider();
                    playerManager.uiManager.UpdateWeaponQuickSlotsUI(false, weaponItem);
                    playerManager.SetAnimatorOverrideController(weaponItem.weaponController);
                }
            }
        }

        public void SuccessfullyThrowFirebomb()
        {
            //Destroy(player.playerEffectsManager.instantiatedFXModel.gameObject);
            Destroy(playerManager.GetInstantiatedFXModel().gameObject);

            BombConsumableItem fireBombItem = playerManager.GetCurrentConsumableItem() as BombConsumableItem;

            GameObject activeModelBomb = Instantiate(fireBombItem.liveBombModel, rightHandSlot.transform.position, playerManager.cameraHandler.cameraPivotTransform.rotation);
            BombDamageCollider damageCollider = activeModelBomb.GetComponentInChildren<BombDamageCollider>();
            damageCollider.impactDamage = fireBombItem.baseDamage;
            damageCollider.explosionSplashDamage = fireBombItem.explosiveDamage;

            activeModelBomb.transform.rotation = Quaternion.Euler(playerManager.cameraHandler.cameraPivotTransform.eulerAngles.x, playerManager.lockOnTransform.eulerAngles.y, 0);
            damageCollider.bombRigidbody.AddForce(activeModelBomb.transform.forward * fireBombItem.forwardVelocity);
            damageCollider.bombRigidbody.AddForce(activeModelBomb.transform.up * fireBombItem.upwardsVelocity);
            damageCollider.teamID = playerManager.GetTeamID();
            LoadWeaponOnSlot(playerManager.GetRightWeapon(), false);
        }
    }
}

