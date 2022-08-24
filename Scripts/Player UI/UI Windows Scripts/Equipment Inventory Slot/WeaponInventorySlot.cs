using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace JJ
{
    public class WeaponInventorySlot : QuickSlotInventorySlot
    {
        public override void AddItem(Item weaponItem)
        {
            item = item as WeaponItem;
            base.AddItem(weaponItem);
        }

        /// <summary>
        /// <para>This function is an OnClickEvent for any of the buttons belonging to an WeaponsInventorySlot</para>
        /// <para>If right/leftHandSlot0X is selected and the weapon in the slot is not null, add the weapon into the WeaponsInventory</para>
        /// <para>The current item that is selected will become the new weapon in the right/leftHandSlot0X</para>
        /// <para>Remove the item from WeaponsInventory to prevent duplicates</para>
        /// <para>Update UI and Reset selected slot</para>
        /// </summary>
        public void EquipThisItem()
        {
            if(uiManager.rightHandSlot01Selected)
            {
                WeaponItem weaponInRightHandSlot01 = uiManager.playerManager.GetWeaponsInRightHandSlots()[0];
                if (weaponInRightHandSlot01 != null)
                {
                    uiManager.playerManager.GetWeaponsInventory().Add(weaponInRightHandSlot01);
                }
                uiManager.playerManager.GetWeaponsInRightHandSlots()[0] = item as WeaponItem;
                uiManager.playerManager.GetWeaponsInventory().Remove(item as WeaponItem);
            }
            else if(uiManager.rightHandSlot02Selected)
            {
                WeaponItem weaponInRightHandSlot02 = uiManager.playerManager.GetWeaponsInRightHandSlots()[1];
                if(weaponInRightHandSlot02 != null)
                {
                    uiManager.playerManager.GetWeaponsInventory().Add(weaponInRightHandSlot02);
                }
                uiManager.playerManager.GetWeaponsInRightHandSlots()[1] = item as WeaponItem;
                uiManager.playerManager.GetWeaponsInventory().Remove(item as WeaponItem);
            }
            else if(uiManager.leftHandSlot01Selected)
            {
                WeaponItem weaponInLeftHandSlot01 = uiManager.playerManager.GetWeaponsInLeftHandSlots()[0];
                if(weaponInLeftHandSlot01 != null)
                {
                    uiManager.playerManager.GetWeaponsInventory().Add(weaponInLeftHandSlot01);
                }
                uiManager.playerManager.GetWeaponsInLeftHandSlots()[0] = item as WeaponItem;
                uiManager.playerManager.GetWeaponsInventory().Remove(item as WeaponItem);
            }
            else if(uiManager.leftHandSlot02Selected)
            {
                WeaponItem weaponInLeftHandSlot02 = uiManager.playerManager.GetWeaponsInLeftHandSlots()[1];
                if(weaponInLeftHandSlot02 != null)
                {
                    uiManager.playerManager.GetWeaponsInventory().Add(weaponInLeftHandSlot02);
                }
                uiManager.playerManager.GetWeaponsInLeftHandSlots()[1] = item as WeaponItem;
                uiManager.playerManager.GetWeaponsInventory().Remove(item as WeaponItem);
            }
            else
            {
                return;
            }
            if(uiManager.playerManager.GetCurrentRightWeaponIndex() >= 0)
            {
                uiManager.playerManager.SetRightWeapon(uiManager.playerManager.GetWeaponsInRightHandSlots()[uiManager.playerManager.GetCurrentRightWeaponIndex()]);
                uiManager.playerManager.LoadWeaponOnSlot(uiManager.playerManager.GetRightWeapon(), false);
            }
            if (uiManager.playerManager.GetCurrentLeftWeaponIndex() >= 0)
            {
                uiManager.playerManager.SetLeftWeapon(uiManager.playerManager.GetWeaponsInLeftHandSlots()[uiManager.playerManager.GetCurrentLeftWeaponIndex()]);
                uiManager.playerManager.LoadWeaponOnSlot(uiManager.playerManager.GetLeftWeapon(), true);
            }
            if (uiManager.playerManager.GetRightWeapon().weaponEffect == WeaponEffect.Darkness && uiManager.playerManager.GetLeftWeapon().weaponEffect == WeaponEffect.Darkness)
            {
                Debug.Log("This is where the temporary logic for setting Darkness Moral Alignment Icon is");
                uiManager.playerManager.SetIsDark();
            }
            else if (uiManager.playerManager.GetRightWeapon().weaponEffect == WeaponEffect.Holy && uiManager.playerManager.GetLeftWeapon().weaponEffect == WeaponEffect.Holy)
            {
                Debug.Log("This is where the temporary logic for setting Holy Moral Alignment Icon is");
                uiManager.playerManager.SetIsHoly();
            }
            else
            {
                Debug.Log("This is where the temporary logic for setting Neutral Moral Alignment Icon is");
                uiManager.playerManager.SetIsNeutral();
            }
            uiManager.LoadWeaponsOnEquipmentScreen(uiManager.playerManager);
            uiManager.ResetAllSelectedSlots();
            

        }

        public override void SelectThisSlot()
        {
            uiManager.UpdateWeaponItemStats(item as WeaponItem);
        }

        protected override void ClickedThisSlot()
        {
            base.ClickedThisSlot();
            uiManager.uiWindowsLeftPanel.SetWeaponInventoryWindowActive(false);
            EquipThisItem();
        }

        /*
        private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> data)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(data));
            trigger.triggers.Add(entry);
        }

        private void OnPointerEnter(BaseEventData eventData)
        {
            PointerEventData pointerEventData = eventData as PointerEventData;
            SelectThisSlot();
        }

        private void OnSelect(BaseEventData eventData)
        {
            SelectThisSlot();
        }
        */
    }
}

