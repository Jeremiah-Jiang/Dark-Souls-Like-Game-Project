using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class HeadButtonParent : MonoBehaviour
    {
        private HeadMenu _headMenu;

        [Header("Formatting")]
        private GridLayoutGroup _gridLayoutGroup;
        private Vector2 _preferredCellSize = new Vector2(100, 100);
        private Vector2 _preferredSpacing = new Vector2(5, 5);
        private int paddingSize = 50;

        [Header("Head Buttons")]
        [SerializeField]
        private HeadButton[] _headButtons;

        private void Awake()
        {
            _headMenu = GetComponentInParent<HeadMenu>();
            _headButtons = GetComponentsInChildren<HeadButton>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            FormatGridLayoutGroup();
        }

        public void SetAllHeadsInactive()
        {
            foreach (var headButton in _headButtons)
            {
                headButton.SetAppearancePrefabActive(false);
            }
        }

        public void SelectFirstButton()
        {
            _headButtons[0].SelectThisButton();
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

