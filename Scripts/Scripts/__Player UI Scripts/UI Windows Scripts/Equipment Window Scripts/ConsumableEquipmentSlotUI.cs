using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JJ
{

    public class ConsumableEquipmentSlotUI : MonoBehaviour
    {
        UIManager uiManager;
        public Image itemIcon;
        ConsumableItem consumableItem;

        public Button equipmentSlotButton;
        public EventTrigger eventTrigger;
        public bool consumableSlot01;
        public bool consumableSlot02;

        private void Awake()
        {
            uiManager = GetComponentInParent<UIManager>();
            equipmentSlotButton = GetComponent<Button>();
            eventTrigger = GetComponent<EventTrigger>();
            itemIcon = transform.GetChild(0).GetChild(0).GetComponent<Image>();
            equipmentSlotButton.onClick.AddListener(ClickedThisSlot);
            AddEventTrigger(eventTrigger, EventTriggerType.PointerEnter, OnPointerEnter);
            AddEventTrigger(eventTrigger, EventTriggerType.Select, OnSelect);
        }

        public void AddItem(ConsumableItem newConsumable)
        {
            consumableItem = newConsumable;
            if (newConsumable != null)
            {
                itemIcon.sprite = newConsumable.itemIcon;
                itemIcon.enabled = true;
            }
            else
            {
                newConsumable = null;
                itemIcon.sprite = null;
                itemIcon.enabled = false;
            }
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            consumableItem = null;
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }
        private void SelectThisSlot()
        {
            uiManager.ResetAllSelectedSlots();
            if (consumableSlot01)
            {
                uiManager.consumableSlot01Selected = true;
            }
            else if (consumableSlot02)
            {
                uiManager.consumableSlot02Selected = true;
            }
            uiManager.UpdateConsumableItemStats(consumableItem);
        }

        private void ClickedThisSlot()
        {
            uiManager.uiWindowsLeftPanel.SetConsumableInventoryWindowActive(true);
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

