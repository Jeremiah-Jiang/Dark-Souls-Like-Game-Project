using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JJ
{
    public class HairstyleButton : AppearanceRelatedButton
    {
        [SerializeField]
        HairstyleButtonParent _hairstyleButtonParent;

        protected override void Awake()
        {
            base.Awake();
            _hairstyleButtonParent = GetComponentInParent<HairstyleButtonParent>();
            _button.onClick.AddListener(ClickedThisButton);
            AddEventTrigger(_eventTrigger, EventTriggerType.PointerEnter, OnPointerEnter);
            AddEventTrigger(_eventTrigger, EventTriggerType.Select, OnSelect);
        }

        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _characterCreationUIManager.SetPlayerDefaultHairModelPrefab(_appearancePrefab);
            _characterCreationUIManager.SetMiddlePanelHairstyleMenuActive(false);
        }
        protected override void OnPointerEnter(BaseEventData eventData)
        {
            base.OnPointerEnter(eventData);
            _hairstyleButtonParent.SetAllHairstylesInactive();
            SetAppearancePrefabActive(true);
        }

        protected override void OnSelect(BaseEventData eventData)
        {
            _hairstyleButtonParent.SetAllHairstylesInactive();
            SetAppearancePrefabActive(true);
        }

    }
}

