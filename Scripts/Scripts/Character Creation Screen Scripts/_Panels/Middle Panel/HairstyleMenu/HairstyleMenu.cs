using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class HairstyleMenu : MonoBehaviour
    {
        [SerializeField]
        HairstyleButtonParent _hairstyleButtonParent;
        [SerializeField]
        Scrollbar _scrollbar;
        private void Awake()
        {
            _hairstyleButtonParent = GetComponentInChildren<HairstyleButtonParent>();
            _scrollbar = GetComponentInChildren<Scrollbar>();
            FormatScrollbar();
        }

        public void SelectFirstButton()
        {
            _hairstyleButtonParent.SelectFirstButton();
        }

        public void SetHairstyleMenuActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public Scrollbar GetScrollbar()
        {
            return _scrollbar;
        }

        private void FormatScrollbar()
        {
            if (_scrollbar == null)
            {
                Debug.LogError("Scrollbar is NULL");
                return;
            }
            _scrollbar.size = 0.2f;
            _scrollbar.direction = Scrollbar.Direction.TopToBottom;
        }
    }
}

