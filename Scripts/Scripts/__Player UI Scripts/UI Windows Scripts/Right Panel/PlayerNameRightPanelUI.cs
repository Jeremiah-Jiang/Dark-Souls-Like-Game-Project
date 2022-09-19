using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class PlayerNameRightPanelUI : MonoBehaviour
    {
        [SerializeField]
        Text _playerName;

        private void Awake()
        {
            _playerName = GetComponentInChildren<Text>();
        }

        public void SetPlayerName(string playerName)
        {
            _playerName.text = playerName;
        }
    }
}

