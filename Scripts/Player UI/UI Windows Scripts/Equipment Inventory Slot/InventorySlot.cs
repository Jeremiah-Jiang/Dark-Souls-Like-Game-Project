using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JJ
{
    public class InventorySlot : MonoBehaviour
    {
        protected UIManager uiManager;
        [SerializeField]
        protected EventTrigger eventTrigger;
        public Image icon;
        [SerializeField]
        protected Button selectInventorySlotButton;
        protected EquipmentItem item;

        protected virtual void Awake()
        {
            uiManager = GetComponentInParent<UIManager>();
            selectInventorySlotButton = GetComponentInChildren<Button>(true);
            eventTrigger = selectInventorySlotButton.GetComponent<EventTrigger>();
            
            selectInventorySlotButton.onClick.AddListener(ClickedThisSlot);
            AddEventTrigger(eventTrigger, EventTriggerType.PointerEnter, OnPointerEnter);
            AddEventTrigger(eventTrigger, EventTriggerType.Select, OnSelect);
        }

        public virtual void AddItem(EquipmentItem equipmentItem)
        {
            item = equipmentItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public virtual void ClearInventorySlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public virtual void SelectThisSlot()
        {
            uiManager.UpdateArmourItemStats(item);
        }

        protected virtual void ClickedThisSlot()
        {
            uiManager.uiWindowsLeftPanel.SetEquipmentScreenWindowActive(true);
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

