using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JJ
{
    public class ColorButton : MonoBehaviour
    {
        [SerializeField]
        protected ColorMenu _colorMenu;

        [Header("Color Values")]
        protected float redAmount;
        protected float greenAmount;
        protected float blueAmount;

        [Header("Essential Components")]
        [SerializeField]
        protected CharacterCreationUIManager _characterCreationUIManager;
        [SerializeField]
        protected Button _button;
        [SerializeField]
        protected Image _buttonImage;
        protected EventTrigger _eventTrigger;

        protected virtual void Awake()
        {
            _characterCreationUIManager = GetComponentInParent<CharacterCreationUIManager>();
            _button = GetComponent<Button>();
            _buttonImage = _button.transform.GetChild(0).GetComponent<Image>();
            _eventTrigger = GetComponent<EventTrigger>();
            _colorMenu = GetComponentInParent<ColorMenu>();
            _button.onClick.AddListener(ClickedThisButton);
            //AddEventTrigger(_eventTrigger, EventTriggerType.PointerEnter, OnPointerEnter);
            //AddEventTrigger(_eventTrigger, EventTriggerType.Select, OnSelect);
            redAmount = _buttonImage.color.r;
            greenAmount = _buttonImage.color.g;
            blueAmount = _buttonImage.color.b;
        }

        public void SelectThisButton()
        {
            _button.Select();
        }

        #region OnClick Events related
        protected virtual void ClickedThisButton()
        {

            SetSliderValuesToImageColor();
        }
        #endregion

        #region Event Trigger related
        protected virtual void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> data)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(data));
            trigger.triggers.Add(entry);
        }


        /*
        protected virtual void OnPointerEnter(BaseEventData eventData)
        {
            PointerEventData pointerEventData = eventData as PointerEventData;
            SetSliderValuesToImageColor();
        }

        protected virtual void OnSelect(BaseEventData eventData)
        {
            SetSliderValuesToImageColor();
        }
        */

        private void SetSliderValuesToImageColor()
        {
            _colorMenu.SetSliderValues(redAmount, greenAmount, blueAmount);
            _colorMenu.SetCurrentColor(redAmount, greenAmount, blueAmount);
            _colorMenu.SetMaterialColor();
        }
        #endregion
    }
}

