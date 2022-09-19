using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class NameCharacter : MonoBehaviour
    {
        PlayerManager playerManager;
        //public GameObject inputField;
        public InputField inputField;
        public Text nameButtonText;

        private void Awake()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            inputField = GetComponent<InputField>();
        }
        public void NameMyCharacter()
        {
            playerManager.SetCharacterName(inputField.text);
            if(playerManager.GetCharacterName() == "")
            {
                playerManager.SetCharacterName("Nameless");
            }
            nameButtonText.text = playerManager.GetCharacterName();
        }
    }
}

