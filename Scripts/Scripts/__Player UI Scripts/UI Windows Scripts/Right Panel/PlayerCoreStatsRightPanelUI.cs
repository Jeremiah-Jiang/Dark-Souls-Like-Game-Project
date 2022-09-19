using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class PlayerCoreStatsRightPanelUI : MonoBehaviour
    {
        [SerializeField]
        Text[] _texts;
        [SerializeField]
        Text[] _coreStatsTitles;
        [SerializeField]
        Text[] _coreStatsValues;

        private void Awake()
        {
            _texts = GetComponentsInChildren<Text>();
            _coreStatsValues = new Text[_texts.Length / 2];

            int currIdx = 0;
            for(int i = 0; i < _texts.Length; i++)
            {
                if(i%2 != 0)
                {
                    _coreStatsValues[currIdx] = _texts[i];
                    currIdx++;
                }
            }
        }

        public void SetHealthLevelValue(int healthLevel)
        {
            _coreStatsValues[0].text = healthLevel.ToString();
        }
        public void SetFocusPointsLevelValue(int focusPointsLevel)
        {
            _coreStatsValues[1].text = focusPointsLevel.ToString();
        }

        public void SetStaminaLevelValue(int staminaLevel)
        {
            _coreStatsValues[2].text = staminaLevel.ToString();
        }


        public void SetPoiseLevelValue(int poiseLevel)
        {
            _coreStatsValues[3].text = poiseLevel.ToString();
        }

        public void SetStrengthLevelValue(int strengthLevel)
        {
            _coreStatsValues[4].text = strengthLevel.ToString();
        }

        public void SetDexterityLevelValue(int dexterityLevel)
        {
            _coreStatsValues[5].text = dexterityLevel.ToString();
        }

        public void SetFaithLevelValue(int faithLevel)
        {
            _coreStatsValues[6].text = faithLevel.ToString();
        }

        public void SetIntelligenceLevelValue(int intelligenceLevel)
        {
            _coreStatsValues[7].text = intelligenceLevel.ToString();
        }
    }
}

