using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class EyebrowButtonParent : MonoBehaviour
    {
        private EyebrowMenu _eyebrowMenu;
        [Header("Formatting")]
        private GridLayoutGroup _gridLayoutGroup;
        private Vector2 _preferredCellSize = new Vector2(100, 100);
        private Vector2 _preferredSpacing = new Vector2(5, 5);
        private int paddingSize = 50;

        [Header("Eyebrow Buttons")]
        [SerializeField]
        private EyebrowButton[] _eyebrowButtons;
        private void Awake()
        {
            _eyebrowMenu = GetComponentInParent<EyebrowMenu>();
            _eyebrowButtons = GetComponentsInChildren<EyebrowButton>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            FormatGridLayoutGroup();
        }

        public void SetAllHeadsInactive()
        {
            foreach (var eyebrowButton in _eyebrowButtons)
            {
                eyebrowButton.SetAppearancePrefabActive(false);
            }
        }

        public void SelectFirstButton()
        {
            _eyebrowButtons[0].SelectThisButton();
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

