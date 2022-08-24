using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class SoulCount : MonoBehaviour
    {
        public Text soulCountText;

        private void Awake()
        {
            soulCountText = GetComponentInChildren<Text>();
            if(soulCountText == null)
            {
                Debug.LogError("soulCountText is null");
            }
        }

        public Text GetSoulCountText()
        {
            return soulCountText;
        }

        public void SetSoulCountText(int souls)
        {
            soulCountText.text = souls.ToString();
        }
    }
}

