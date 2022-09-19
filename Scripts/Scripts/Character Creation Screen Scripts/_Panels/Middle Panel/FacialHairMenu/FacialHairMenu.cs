using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class FacialHairMenu : MonoBehaviour
    {
        [SerializeField]
        FacialHairButtonParent _facialHairButtonParent;

        private void Awake()
        {
            _facialHairButtonParent = GetComponentInChildren<FacialHairButtonParent>();
        }

        public void SelectFirstButton()
        {
            _facialHairButtonParent.SelectFirstButton();
        }

        public void SetFacialHairMenuActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}

