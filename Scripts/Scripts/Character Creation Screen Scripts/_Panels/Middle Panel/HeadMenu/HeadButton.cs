using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JJ
{
    public class HeadButton : AppearanceRelatedButton
    {
        [SerializeField]
        HeadButtonParent _headButtonParent;

        protected override void Awake()
        {
            base.Awake();
            _headButtonParent = GetComponentInParent<HeadButtonParent>();
            _button.onClick.AddListener(ClickedThisButton);
            AddEventTrigger(_eventTrigger, EventTriggerType.PointerEnter, OnPointerEnter);
            AddEventTrigger(_eventTrigger, EventTriggerType.Select, OnSelect);
        }

        protected override void ClickedThisButton()
        {
            base.ClickedThisButton();
            _characterCreationUIManager.SetPlayerNakedHeadModelPrefab(_appearancePrefab);
            _characterCreationUIManager.SetMiddlePanelHeadMenuActive(false);
        }

        protected override void OnPointerEnter(BaseEventData eventData)
        {
            base.OnPointerEnter(eventData);
            _headButtonParent.SetAllHeadsInactive();
            SetAppearancePrefabActive(true);
        }

        protected override void OnSelect(BaseEventData eventData)
        {
            _headButtonParent.SetAllHeadsInactive();
            SetAppearancePrefabActive(true);
        }
    }
}

