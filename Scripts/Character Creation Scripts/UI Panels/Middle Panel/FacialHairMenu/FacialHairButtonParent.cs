using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class FacialHairButtonParent : MonoBehaviour
    {
        private FacialHairMenu _facialHairMenu;
        [Header("Formatting")]
        private GridLayoutGroup _gridLayoutGroup;
        private Vector2 _preferredCellSize = new Vector2(100, 100);
        private Vector2 _preferredSpacing = new Vector2(5, 5);
        private int paddingSize = 50;

        [Header("Facial Hair Buttons")]
        [SerializeField]
        private FacialHairButton[] _facialHairButtons;
        private void Awake()
        {
            _facialHairMenu = GetComponentInParent<FacialHairMenu>();
            _facialHairButtons = GetComponentsInChildren<FacialHairButton>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            FormatGridLayoutGroup();
        }

        public void SetAllFacialHairsInactive()
        {
            foreach (var facialHairButton in _facialHairButtons)
            {
                facialHairButton.SetAppearancePrefabActive(false);
            }
        }

        public void SelectFirstButton()
        {
            _facialHairButtons[0].SelectThisButton();
        }
        #region Formatting Methods
        private void FormatGridLayoutGroup()
        {
            if (_gridLayoutGroup == null)
            {
                Debug.LogError("GridLayoutGroup is NULL");
                return;
            }
            _gridLayoutGroup.padding.left = paddingSize;
            _gridLayoutGroup.padding.right = paddingSize;
            _gridLayoutGroup.padding.top = paddingSize;
            _gridLayoutGroup.padding.bottom = paddingSize;

            _gridLayoutGroup.cellSize = _preferredCellSize;
            _gridLayoutGroup.spacing = _preferredSpacing;
            _gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
        }
        #endregion
    }
}

