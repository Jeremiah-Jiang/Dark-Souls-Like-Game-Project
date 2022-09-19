using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JJ
{
    public class EyebrowButton : AppearanceRelatedButton
    {
        [SerializeField]
        EyebrowButtonParent _eyebrowButtonParent;

        protected override void Awake()
        {
            base.Awake();
            _eyebrowButtonParent = GetComponentInParent<EyebrowButtonParent>();
            _button.onClick.AddListener(ClickedThisButton);
            AddEventTrigger(_eventTrigger, EventTriggerType.PointerEnter, OnPointerEnter);
            AddEventTrigger(_eventTrigger, EventTriggerType.Select, OnSelect);
        }

        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _characterCreationUIManager.SetPlayerDefaultEyebrowModelPrefab(_appearancePrefab);
            _characterCreationUIManager.SetMiddlePanelEyebrowMenuActive(false);
        }

        protected override void OnPointerEnter(BaseEventData eventData)
        {
            base.OnPointerEnter(eventData);
            _eyebrowButtonParent.SetAllHeadsInactive();
            SetAppearancePrefabActive(true);
        }

        protected override void OnSelect(BaseEventData eventData)
        {
            _eyebrowButtonParent.SetAllHeadsInactive();
            SetAppearancePrefabActive(true);
        }
    }

}
