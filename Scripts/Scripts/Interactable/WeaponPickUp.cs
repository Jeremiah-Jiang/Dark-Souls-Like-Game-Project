using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class WeaponPickUp : Interactable
    {
        public WeaponItem weapon;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventoryManager playerInventory;
            PlayerLocomotionManager playerLocomotion;
            PlayerAnimatorManager playerAnimatorManager;
            playerInventory = playerManager.GetComponent<PlayerInventoryManager>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = playerManager.GetComponentInChildren<PlayerAnimatorManager>();

            playerLocomotion.rigidbody.velocity = Vector3.zero; //Stop player from moving when picking up an item
            playerAnimatorManager.PlayTargetAnimation("Pick Up Item", true);
            playerInventory.weaponsInventory.Add(weapon);
            GameObject itemInteractableGameObject = playerManager.GetItemPopUp().gameObject;
            itemInteractableGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
            itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;
            itemInteractableGameObject.SetActive(true);
            Destroy(gameObject);

        }
    }
}

