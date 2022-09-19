using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class PopUpUI : MonoBehaviour
    {
        [Header("Pop ups")]
        [SerializeField]
        BonfirePopUpUI _bonfirePopUpUI;

        private void Awake()
        {
            _bonfirePopUpUI = GetComponentInChildren<BonfirePopUpUI>();
        }
        public void DisplayBonfireLitTextPopUP()
        {
            _bonfirePopUpUI.DisplayBonfireLitTextPopUP();
        }
    }
}

