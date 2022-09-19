using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class EyebrowMenu : MonoBehaviour
    {
        [SerializeField]
        EyebrowButtonParent _eyebrowButtonParent;

        private void Awake()
        {
            _eyebrowButtonParent = GetComponentInChildren<EyebrowButtonParent>();
        }

        public void SelectFirstButton()
        {
            _eyebrowButtonParent.SelectFirstButton();
        }

        public void SetEyebrowMenuActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}

