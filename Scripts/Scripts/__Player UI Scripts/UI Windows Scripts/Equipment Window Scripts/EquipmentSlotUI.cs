using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JJ
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(EventTrigger))]
    public class EquipmentSlotUI : MonoBehaviour//, IPointerClickHandler
    {
        protected UIManager _uiManager;
        [SerializeField]
        protected Image _itemIcon; //Assign in Inspector, there are 2 image components
        protected EquipmentItem _item;
        [SerializeField]
        protected Button _equipmentSlotButton;
        [SerializeField]
        protected EventTrigger _eventTrigger;

        protected virtual void Awake()
        {
            _uiManager = GetComponentInParent<UIManager>();
            _equipmentSlotButton = GetComponent<Button>();
            _eventTrigger = GetComponent<EventTrigger>();
            _itemIcon = transform.GetChild(0).GetChild(0).GetComponent<Image>();
            _itemIcon.enabled = false;
            _equipmentSlotButton.onClick.AddListener(ClickedThisSlot);
            AddEventTrigger(_eventTrigger, EventTriggerType.PointerEnter, OnPointerEnter);
            AddEventTrigger(_eventTrigger, EventTriggerType.Select, OnSelect);
        }

        public virtual void AddItem(EquipmentItem equipmentItem)
        {
            _item = equipmentItem;
            _itemIcon.sprite = _item.itemIcon;
            _itemIcon.enabled = true;
            gameObject.SetActive(true);
        }

        public virtual void ClearItem()
        {
            _item = null;
            _itemIcon.sprite = null;
            _itemIcon.enabled = false;
        }

        protected virtual void SelectThisSlot()
        {
            _uiManager.ResetAllSelectedSlots();
            _uiManager.UpdateArmourItemStats(_item);
        }

        
        protected virtual void ClickedThisSlot()
        {
            _uiManager.uiWindowsLeftPanel.SetEquipmentScreenWindowActive(false);
        }
        

        private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> data)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(data));
            trigger.triggers.Add(entry);
        }

        /*
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                _uiManager.uiWindowsLeftPanel.SetEquipmentScreenWindowActive(false);

            }
            else if(eventData.button == PointerEventData.InputButton.Right)
            {
                Debug.Log("Right Button Clicked");
            }
        }
        */

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

