using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class HeadMenu : MonoBehaviour
    {
        [SerializeField]
        HeadButtonParent _headButtonParent;

        private void Awake()
        {
            _headButtonParent = GetComponentInChildren<HeadButtonParent>();
        }

        public void SelectFirstButton()
        {
            _headButtonParent.SelectFirstButton();
        }

        public void SetHeadMenuActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}

