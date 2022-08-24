using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class SelectWindow : MonoBehaviour
    {
        [SerializeField]
        UIManager _uiManager;
        public Button selectInventoryButton;
        public Button selectEquipmentButton;
        public Button selectGameOptionsButton;
        private Button[] _buttons;
        
        private void Awake()
        {
            _uiManager = GetComponentInParent<UIManager>(); //Might need to change this since this isn't called until the SelectWindow game object is active
            _buttons = GetComponentsInChildren<Button>(true);

            selectInventoryButton = _buttons[0];
            selectEquipmentButton = _buttons[1];
            selectGameOptionsButton = _buttons[2];
            selectInventoryButton.onClick.AddListener(ClickedSelectInventoryButton);
            selectEquipmentButton.onClick.AddListener(ClickedSelectEquipmentButton);
            selectGameOptionsButton.onClick.AddListener(TaskOnClick);
        }

        private void Start()
        {
            SetSelectWindowActive(false);
        }

        private void TaskOnClick()
        {
            Debug.Log("Select Game Options Button Clicked");
        }
        private void ClickedSelectInventoryButton()
        {
            _uiManager.uiWindowsLeftPanel.CloseAllLeftPanelWindows();
            _uiManager.uiWindowsLeftPanel.SetWeaponInventoryWindowActive(true);
            _uiManager.uiWindowsMiddlePanel.SetItemStatsWindowActive(true);
            _uiManager.uiWindowsRightPanel.SetPlayerStatsWindowActive(true);
        }

        private void ClickedSelectEquipmentButton()
        {
            _uiManager.uiWindowsLeftPanel.CloseAllLeftPanelWindows();
            _uiManager.uiWindowsLeftPanel.SetEquipmentScreenWindowActive(true);
            _uiManager.uiWindowsMiddlePanel.SetItemStatsWindowActive(true);
            _uiManager.uiWindowsRightPanel.SetPlayerStatsWindowActive(true);
        }

        public void SetSelectWindowActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}

