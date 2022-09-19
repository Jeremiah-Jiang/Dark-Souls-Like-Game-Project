using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class PlayerLevelAndRunesRightPanelUI : MonoBehaviour
    {
        [SerializeField]
        Text[] _statTitles;
        Text _playerLevelText;
        [SerializeField]
        Text _playerLevelValueText;
        Text _playerSoulsText;
        [SerializeField]
        Text _playerSoulsValueText;
        private void Awake()
        {
            _statTitles = GetComponentsInChildren<Text>(true);
            _playerLevelText = _statTitles[0];
            _playerLevelValueText = _statTitles[1];

            _playerSoulsText = _statTitles[2];
            _playerSoulsValueText = _statTitles[3];
        }

        public void SetPlayerLevelValueText(string levelValue)
        {
            _playerLevelValueText.text = levelValue;
        }

        public void SetPlayerSoulsValueText(string soulsValue)
        {
            _playerSoulsValueText.text = soulsValue;
        }
    }
}

