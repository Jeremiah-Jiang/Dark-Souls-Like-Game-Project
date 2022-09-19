using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace JJ
{
    public class CharacterCreationUIManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerManager _playerManager;
        [SerializeField]
        private SpawnPlayerTransform _spawnPlayerTransform;
        [Header("Cameras")]
        [SerializeField]
        private CharacterPreviewCamera _characterPreviewCamera;
        [SerializeField]
        private CharacterFacialPreviewCamera _characterFacialPreviewCamera;

        [Header("UI Panels")]
        [SerializeField]
        public CharacterCreationUILeftPanel characterCreationUILeftPanel;
        [SerializeField]
        public CharacterCreationUIMiddlePanel characterCreationUIMiddlePanel;
        [SerializeField]
        public CharacterCreationUIRightPanel characterCreationUIRightPanel;

        private void Awake()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
            if (_playerManager != null)
            {
                _playerManager.EnableInputManager(false);
            }
            _spawnPlayerTransform = FindObjectOfType<SpawnPlayerTransform>();
            _characterPreviewCamera = FindObjectOfType<CharacterPreviewCamera>();
            _characterFacialPreviewCamera = FindObjectOfType<CharacterFacialPreviewCamera>(true);

            characterCreationUILeftPanel = GetComponentInChildren<CharacterCreationUILeftPanel>();
            characterCreationUIMiddlePanel = GetComponentInChildren<CharacterCreationUIMiddlePanel>();
            characterCreationUIRightPanel = GetComponentInChildren<CharacterCreationUIRightPanel>();
        }

        private void Start()
        {
            //_playerManager.cameraHandler.gameObject.SetActive(false);
            ZoomInOnFace(false);
        }

        public void SetPlayerNakedHeadModelPrefab(GameObject head)
        {
            _playerManager.SetNakedHeadModelPrefab(head);
        }

        public void SetPlayerDefaultHairModelPrefab(GameObject hair)
        {
            _playerManager.SetDefaultHairModelPrefab(hair);
        }

        public void SetPlayerDefaultEyebrowModelPrefab(GameObject eyebrow)
        {
            _playerManager.SetDefaultEyebrowModelPrefab(eyebrow);
        }

        public void SetPlayerDefaultFacialHairModelPrefab(GameObject facialHair)
        {
            _playerManager.SetDefaultFacialHairModelPrefab(facialHair);
        }
    //Camera Methods
        #region Camera Methods
        public void ZoomInOnFace(bool value)
        {
            _characterFacialPreviewCamera.gameObject.SetActive(value);
            _characterPreviewCamera.gameObject.SetActive(!value);
        }
        #endregion

    //Left Panel Methods
        #region Left Panel Methods
        public void SetAllCharacterMenuButtonsInteractable(bool value)
        {
            characterCreationUILeftPanel.SetAllCharacterMenuButtonsInteractable(value);
        }

        public void SetLastCharacterMenuButtonSelected(CharacterMenuButton characterMenuButton)
        {

        }

        public void SelectLastCharacterMenuButtonSelected()
        {
            characterCreationUILeftPanel.SelectLastCharacterMenuButtonSelected();
        }

        public void StartGame()
        {
            _playerManager.EnableInputManager(true);
            //SceneManager.LoadScene(1);
            _playerManager.gameObject.transform.parent = null;
            _playerManager.transform.position = new Vector3(_spawnPlayerTransform.transform.position.x, _spawnPlayerTransform.transform.position.y, _spawnPlayerTransform.transform.position.z);
            _playerManager.gameObject.transform.forward = _spawnPlayerTransform.transform.forward;
            _playerManager.cameraHandler.gameObject.SetActive(true);
            Destroy(_characterFacialPreviewCamera.gameObject);
            Destroy(_characterPreviewCamera.gameObject);
            //_characterFacialPreviewCamera.gameObject.SetActive(false);
            //_characterPreviewCamera.gameObject.SetActive(false);
            Destroy(this.gameObject);
            Destroy(_spawnPlayerTransform.gameObject);
        }
        #endregion

    //Middle Panel Methods
        #region Middle Panel Methods
        //Head Menu
        public void SetMiddlePanelHeadMenuActive(bool value)
        {
            characterCreationUIMiddlePanel.SetHeadMenuActive(value);
        }

        public void SelectMiddlePanelFirstHeadButton()
        {
            characterCreationUIMiddlePanel.SelectFirstHeadButton();
        }

        //Hairstyle Menu
        public void SetMiddlePanelHairstyleMenuActive(bool value)
        {
            characterCreationUIMiddlePanel.SetHairstyleMenuActive(value);
        }

        public void SelectMiddlePanelFirstHairstyleButton()
        {
            characterCreationUIMiddlePanel.SelectFirstHairstyleButton();
        }

        //Eyebrow Menu
        public void SetMiddlePanelEyebrowMenuActive(bool value)
        {
            characterCreationUIMiddlePanel.SetEyebrowMenuActive(value);
        }

        public void SelectMiddlePanelFirstEyebrowButton()
        {
            characterCreationUIMiddlePanel.SelectFirstEyebrowButton();
        }

        //Facial Hair Menu
        public void SetMiddlePanelFacialHairMenuActive(bool value)
        {
            characterCreationUIMiddlePanel.SetFacialHairMenuActive(value);
        }

        public void SelectMiddlePanelFirstFacialHairButton()
        {
            characterCreationUIMiddlePanel.SelectFirstFacialHairButton();
        }

        //Eye Color Menu
        public void SetMiddlePanelEyeColorMenuActive(bool value)
        {
            characterCreationUIMiddlePanel.SetEyeColorMenuActive(value);
        }

        public void SelectMiddlePanelFirstEyeColorButton()
        {
            characterCreationUIMiddlePanel.SelectFirstEyeColorButton();
        }

        //Hair Color Menu
        public void SetMiddlePanelHairColorMenuActive(bool value)
        {
            characterCreationUIMiddlePanel.SetHairColorMenuActive(value);
        }

        public void SelectMiddlePanelFirstHairColorButton()
        {
            characterCreationUIMiddlePanel.SelectFirstHairColorButton();
        }

        //Body Art Color Menu
        public void SetMiddlePanelBodyArtColorMenuActive(bool value)
        {
            characterCreationUIMiddlePanel.SetBodyArtColorMenuActive(value);
        }

        public void SelectMiddlePanelFirstBodyArtColorButton()
        {
            characterCreationUIMiddlePanel.SelectFirstBodyArtColorButton();
        }

        //Skin Color Menu
        public void SetMiddlePanelSkinColorMenuActive(bool value)
        {
            characterCreationUIMiddlePanel.SetSkinColorMenuActive(value);
        }

        public void SelectMiddlePanelFirstSkinColorButton()
        {
            characterCreationUIMiddlePanel.SelectFirstSkinColorButton();
        }
        #endregion
    }
}

