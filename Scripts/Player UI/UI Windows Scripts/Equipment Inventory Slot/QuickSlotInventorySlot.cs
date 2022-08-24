using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JJ
{
    public class QuickSlotInventorySlot : MonoBehaviour
    {
        [SerializeField]
        protected UIManager uiManager;
        [SerializeField]
        protected Button selectWeaponInventorySlotButton;
        [SerializeField]
        protected EventTrigger eventTrigger;
        [SerializeField]
        protected Image icon;
        [SerializeField]
        protected Item item;
        // Start is called before the first frame update
        private void Awake()
        {
            uiManager = GetComponentInParent<UIManager>();
            selectWeaponInventorySlotButton = GetComponentInChildren<Button>(true);
            eventTrigger = selectWeaponInventorySlotButton.GetComponent<EventTrigger>();

            selectWeaponInventorySlotButton.onClick.AddListener(ClickedThisSlot);
            AddEventTrigger(eventTrigger, EventTriggerType.PointerEnter, OnPointerEnter);
            AddEventTrigger(eventTrigger, EventTriggerType.Select, OnSelect);
        }
        public virtual void AddItem(Item newItem)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        

        public virtual void SelectThisSlot()
        {
            //uiManager.ResetAllSelectedSlots();
            //uiManager.UpdateWeaponItemStats(item);
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

        protected virtual void OnPointerEnter(BaseEventData eventData)
        {
            PointerEventData pointerEventData = eventData as PointerEventData;
            SelectThisSlot();
        }

        protected virtual void OnSelect(BaseEventData eventData)
        {
            SelectThisSlot();
        }
    }
}

