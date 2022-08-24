using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class CharacterCreationUIMiddlePanel : MonoBehaviour
    {
        [SerializeField]
        HeadMenu _headMenu;
        [SerializeField]
        HairstyleMenu _hairstyleMenu;
        [SerializeField]
        EyebrowMenu _eyebrowMenu;
        [SerializeField]
        FacialHairMenu _facialHairMenu;
        [SerializeField]
        HairColorMenu _hairColorMenu;
        [SerializeField]
        BodyArtColorMenu _bodyArtColorMenu;
        [SerializeField]
        SkinColorMenu _skinColorMenu;
        [SerializeField]
        EyeColorMenu _eyeColorMenu;
        private void Awake()
        {
            _headMenu = GetComponentInChildren<HeadMenu>(true);
            _hairstyleMenu = GetComponentInChildren<HairstyleMenu>(true);
            _eyebrowMenu = GetComponentInChildren<EyebrowMenu>(true);
            _facialHairMenu = GetComponentInChildren<FacialHairMenu>(true);
            _hairColorMenu = GetComponentInChildren<HairColorMenu>(true);
            _bodyArtColorMenu = GetComponentInChildren<BodyArtColorMenu>(true);
            _skinColorMenu = GetComponentInChildren<SkinColorMenu>(true);
            _eyeColorMenu = GetComponentInChildren<EyeColorMenu>(true);
        }

        private void Start()
        {
            DisableAllMenus();
        }

        private void DisableAllMenus()
        {
            SetHeadMenuActive(false);
            SetHairstyleMenuActive(false);
            SetEyebrowMenuActive(false);
            SetEyeColorMenuActive(false);
            SetFacialHairMenuActive(false);
            SetHairColorMenuActive(false);
            SetBodyArtColorMenuActive(false);
            SetSkinColorMenuActive(false);
        }

        //Head Methods
        #region Head Methods
        public void SetHeadMenuActive(bool value)
        {
            _headMenu.SetHeadMenuActive(value);
        }

        public void SelectFirstHeadButton()
        {
            _headMenu.SelectFirstButton();
        }
        #endregion

        //Hairstyle Methods
        #region Hairstyle Methods
        public void SetHairstyleMenuActive(bool value)
        {
            _hairstyleMenu.SetHairstyleMenuActive(value);
        }

        public void SelectFirstHairstyleButton()
        {
            _hairstyleMenu.SelectFirstButton();
        }
        #endregion

        //Eyebrow Methods
        #region Eyebrow Methods
        public void SetEyebrowMenuActive(bool value)
        {
            _eyebrowMenu.SetEyebrowMenuActive(value);
        }

        public void SelectFirstEyebrowButton()
        {
            _eyebrowMenu.SelectFirstButton();
        }
        #endregion

        //Facial Hair Methods
        #region Facial Hair Methods
        public void SetFacialHairMenuActive(bool value)
        {
            _facialHairMenu.SetFacialHairMenuActive(value);
        }

        public void SelectFirstFacialHairButton()
        {
            _facialHairMenu.SelectFirstButton();
        }
        #endregion

        //Eye Color Methods
        #region Eye Color Methods
        public void SetEyeColorMenuActive(bool value)
        {
            _eyeColorMenu.SetColorMenuActive(value);
        }

        public void SelectFirstEyeColorButton()
        {
            _eyeColorMenu.SelectFirstButton();
        }

        #endregion

        //Hair Color Methods
        #region Hair Color Methods
        public void SetHairColorMenuActive(bool value)
        {
            _hairColorMenu.SetColorMenuActive(value);
        }

        public void SelectFirstHairColorButton()
        {
            _hairColorMenu.SelectFirstButton();
        }
        #endregion

        //Body Art ColorMethods
        #region Body Art Color Methods
        public void SetBodyArtColorMenuActive(bool value)
        {
            _bodyArtColorMenu.SetColorMenuActive(value);
        }

        public void SelectFirstBodyArtColorButton()
        {
            _bodyArtColorMenu.SelectFirstButton();
        }
        #endregion

        //Skin Color Methods
        #region Skin Color Methods
        public void SetSkinColorMenuActive(bool value)
        {
            _skinColorMenu.SetColorMenuActive(value);
        }

        public void SelectFirstSkinColorButton()
        {
            _skinColorMenu.SelectFirstButton();
        }
        #endregion
    }
}

