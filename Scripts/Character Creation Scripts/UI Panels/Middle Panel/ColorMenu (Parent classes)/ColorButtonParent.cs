using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class ColorButtonParent : MonoBehaviour
    {
        [Header("Formatting")]
        protected GridLayoutGroup _gridLayoutGroup;
        protected Vector2 _preferredCellSize = new Vector2(100, 100);
        protected Vector2 _preferredSpacing = new Vector2(1, 1);
        protected int leftPaddingSize = 55;
        protected int rightPaddingSize = 55;
        protected int topPaddingSize = 90;
        protected int bottomPaddingSize = 60;

        [Header("Appearance Related Buttons")]
        [SerializeField]
        protected ColorButton[] _colorButtons;

        protected virtual void Awake()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _colorButtons = GetComponentsInChildren<ColorButton>();
        }

        public void SelectFirstButton()
        {
            _colorButtons[0].SelectThisButton();
        }

        #region Formatting Methods
        private void FormatGridLayoutGroup()
        {
            if (_gridLayoutGroup == null)
            {
                Debug.LogError("GridLayoutGroup is NULL");
                return;
            }
            _gridLayoutGroup.padding.left = leftPaddingSize;
            _gridLayoutGroup.padding.right = rightPaddingSize;
            _gridLayoutGroup.padding.top = topPaddingSize;
            _gridLayoutGroup.padding.bottom = bottomPaddingSize;

            _gridLayoutGroup.cellSize = _preferredCellSize;
            _gridLayoutGroup.spacing = _preferredSpacing;
            _gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
        }
        #endregion

    }

}
