using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    [RequireComponent(typeof(ScrollRect))]
    [RequireComponent(typeof(RectMask2D))]
    [RequireComponent(typeof(GridLayoutGroup))]
    public class HairstyleButtonParent : MonoBehaviour
    {
        private HairstyleMenu _hairstyleMenu;

        [Header("Formatting")]
        [SerializeField]
        private ScrollRect _scrollRect;
        [SerializeField]
        private RectMask2D _rectMask2D;
        [SerializeField]
        private GridLayoutGroup _gridLayoutGroup;

        private Vector2 _preferredCellSize = new Vector2(100, 100);

        private Vector2 _preferredSpacing = new Vector2(5, 5);

        private int paddingSize = 50;
        

        [Header("Hairstyle Buttons")]
        [SerializeField]
        private HairstyleButton[] _hairstyleButtons;


        private void Awake()
        {
            _hairstyleMenu = GetComponentInParent<HairstyleMenu>();
            _hairstyleButtons = GetComponentsInChildren<HairstyleButton>();
            _scrollRect = GetComponent<ScrollRect>();
            _rectMask2D = GetComponent<RectMask2D>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            
            InitializeFormatting();
        }


        public void SetAllHairstylesInactive()
        { 
            foreach(var hairstyleButton in _hairstyleButtons)
            {
                hairstyleButton.SetAppearancePrefabActive(false);
            }
        }

        public void SelectFirstButton()
        {
            _hairstyleButtons[0].SelectThisButton();
        }

        #region Formatting Methods
        public void InitializeFormatting()
        {
            FormatGridLayoutGroup();
            FormatScrollRect();
        }

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

        private void FormatScrollRect()
        {
            if (_scrollRect == null)
            {
                Debug.LogError("ScrollRect is NULL");
                return;
            }
            _scrollRect.content = GetComponent<RectTransform>();
            _scrollRect.horizontal = false;
            _scrollRect.vertical = true;
            _scrollRect.viewport = GetComponent<RectTransform>();
            if(_hairstyleMenu.GetScrollbar() != null)
            {
                Debug.Log("Formatting Scrollbar");
                _scrollRect.verticalScrollbar = _hairstyleMenu.GetScrollbar();
            }
            else
            {
                Debug.Log("Scrollbar is NULL");
            }
        }


        #endregion

    }
}

