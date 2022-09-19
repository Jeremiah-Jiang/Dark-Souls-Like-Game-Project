using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JJ
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(EventTrigger))]
    public class AppearanceRelatedButton : MonoBehaviour
    {
        [Header("Appearance Prefab")]
        [SerializeField]
        protected GameObject _appearancePrefab; //Assign in inspector

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
            //_button.onClick.AddListener(ClickedThisButton);
            _buttonImage = _button.transform.GetChild(0).GetComponent<Image>();
            _eventTrigger = GetComponent<EventTrigger>();
            if(_appearancePrefab != null)
            {
                _buttonImage.sprite = _appearancePrefab.GetComponent<Image>().sprite;
            }
        }

        public void SetAppearancePrefabActive(bool value)
        {
            if (_appearancePrefab != null)
            {
                _appearancePrefab.SetActive(value);
            }
        }

        public void SelectThisButton()
        {
            _button.Select();
        }

        #region OnClick Events related
        protected virtual void ClickedThisButton()
        {
            _characterCreationUIManager.SetAllCharacterMenuButtonsInteractable(true);
            _characterCreationUIManager.SelectLastCharacterMenuButtonSelected();
            _characterCreationUIManager.ZoomInOnFace(false);
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

        protected virtual void OnPointerEnter(BaseEventData eventData)
        {
            PointerEventData pointerEventData = eventData as PointerEventData;

        }

        protected virtual void OnSelect(BaseEventData eventData)
        {

        }
        #endregion
    }
}

