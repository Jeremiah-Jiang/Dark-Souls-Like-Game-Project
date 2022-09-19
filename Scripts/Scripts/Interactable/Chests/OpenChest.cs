using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class OpenChest : Interactable
    {
        [SerializeField]
        Transform playerInteractionPosition;
        Animator animator;
        OpenChest openChest;
        [SerializeField]
        GameObject itemSpawner;
        [SerializeField]
        public WeaponItem itemInChest;
        public Transform itemSpawnTransform;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            openChest = GetComponent<OpenChest>();
        }
        public override void Interact(PlayerManager playerManager)
        {
            #region Handle Rotate Player to Chest
            Vector3 rotationDirection = transform.position - playerManager.transform.position;
            rotationDirection.y = 0;
            rotationDirection.Normalize();

            Quaternion tr = Quaternion.LookRotation(rotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 300 * Time.deltaTime);
            playerManager.transform.rotation = targetRotation;
            #endregion

            playerManager.OpenChestInteraction(playerInteractionPosition);
            animator.Play("Chest Open");
            StartCoroutine(SpawnItemInChest());
            this.tag = "Untagged"; 
            playerManager.GetInteractionPopUp().gameObject.SetActive(false); //disable open chest text pop up
            
            WeaponPickUp weaponPickUp = itemSpawner.GetComponent<WeaponPickUp>();
            if(weaponPickUp != null)
            {
                weaponPickUp.weapon = itemInChest;
            }
            
        }

        IEnumerator SpawnItemInChest()
        {
            yield return new WaitForSeconds(1f);
            Instantiate(itemSpawner, itemSpawnTransform);
            Destroy(openChest);
        }
    }
}

