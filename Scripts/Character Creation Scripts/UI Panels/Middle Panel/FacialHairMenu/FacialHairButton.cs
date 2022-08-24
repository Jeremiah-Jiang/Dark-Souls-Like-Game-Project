using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JJ
{
    public class FacialHairButton : AppearanceRelatedButton
    {
        [SerializeField]
        FacialHairButtonParent _facialHairButtonParent;

        protected override void Awake()
        {
            base.Awake();
            _facialHairButtonParent = GetComponentInParent<FacialHairButtonParent>();
            _button.onClick.AddListener(ClickedThisButton);
            AddEventTrigger(_eventTrigger, EventTriggerType.PointerEnter, OnPointerEnter);
            AddEventTrigger(_eventTrigger, EventTriggerType.Select, OnSelect);
        }

        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _characterCreationUIManager.SetPlayerDefaultFacialHairModelPrefab(_appearancePrefab);
            _characterCreationUIManager.SetMiddlePanelFacialHairMenuActive(false);
        }

        protected override void OnPointerEnter(BaseEventData eventData)
        {
            base.OnPointerEnter(eventData);
            _facialHairButtonParent.SetAllFacialHairsInactive();
            SetAppearancePrefabActive(true);
        }

        protected override void OnSelect(BaseEventData eventData)
        {
            _facialHairButtonParent.SetAllFacialHairsInactive();
            SetAppearancePrefabActive(true);
        }
    }
}

