using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JJ
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(EventTrigger))]
    public class WeaponEquipmentSlotUI : MonoBehaviour
    {
        UIManager uiManager;
        public Image itemIcon; //Assign in inspector, there are 2 image components under this Script, so can't use GetComponentInChildren
        WeaponItem weapon;

        public Button equipmentSlotButton;
        public EventTrigger eventTrigger;
        public bool rightHandSlot01;
        public bool rightHandSlot02;
        public bool leftHandSlot01;
        public bool leftHandSlot02;

        private void Awake()
        {
            uiManager = GetComponentInParent<UIManager>();
            equipmentSlotButton = GetComponent<Button>();
            eventTrigger = GetComponent<EventTrigger>();
            itemIcon = transform.GetChild(0).GetChild(0).GetComponent<Image>();
            itemIcon.enabled = false;
            equipmentSlotButton.onClick.AddListener(ClickedThisSlot);
            AddEventTrigger(eventTrigger, EventTriggerType.PointerEnter, OnPointerEnter);
            AddEventTrigger(eventTrigger, EventTriggerType.Select, OnSelect);
        }

        public void AddItem(WeaponItem newWeapon)
        {
            weapon = newWeapon;
            if (newWeapon != null)
            {
                itemIcon.sprite = weapon.itemIcon;
                itemIcon.enabled = true;
            }
            else
            {
                weapon = null;
                itemIcon.sprite = null;
                itemIcon.enabled = false;
            }
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            weapon = null;
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }

        private void SelectThisSlot()
        {
            uiManager.ResetAllSelectedSlots();
            if(rightHandSlot01)
            {
                uiManager.rightHandSlot01Selected = true;
            }
            else if(rightHandSlot02)
            {
                uiManager.rightHandSlot02Selected = true;
            }
            else if(leftHandSlot01)
            {
                uiManager.leftHandSlot01Selected = true;
            }
            else
            {
                uiManager.leftHandSlot02Selected = true;
            }
            uiManager.UpdateWeaponItemStats(weapon);
        }

        private void ClickedThisSlot()
        {
            uiManager.uiWindowsLeftPanel.SetWeaponInventoryWindowActive(true);
            uiManager.uiWindowsLeftPanel.SetEquipmentScreenWindowActive(false);
            SelectThisSlot();
        }

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
    }
}

