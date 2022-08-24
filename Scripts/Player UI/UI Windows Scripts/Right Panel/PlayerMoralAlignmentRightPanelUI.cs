using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace JJ
{
    public class PlayerMoralAlignmentRightPanelUI : MonoBehaviour
    {
        [SerializeField]
        Image[] _moralAlignmentIcons;

        [SerializeField]
        Image _neutralMoralAlignmentIcon;
        [SerializeField]
        Image _darknessMoralAlignmentIcon;
        [SerializeField]
        Image _holyMoralAlignmentIcon;
        private void Awake()
        {
            _moralAlignmentIcons = GetComponentsInChildren<Image>(true);
            _neutralMoralAlignmentIcon = _moralAlignmentIcons[1];
            _darknessMoralAlignmentIcon = _moralAlignmentIcons[2];
            _holyMoralAlignmentIcon = _moralAlignmentIcons[3];
        }

        public void SetDarknessMoralAlignmentIconActive()
        {
            _neutralMoralAlignmentIcon.gameObject.SetActive(false);
            _darknessMoralAlignmentIcon.gameObject.SetActive(true);
            _holyMoralAlignmentIcon.gameObject.SetActive(false);
        }

        public void SetNeutralMoralAlignmentIconActive()
        {
            _neutralMoralAlignmentIcon.gameObject.SetActive(true);
            _darknessMoralAlignmentIcon.gameObject.SetActive(false);
            _holyMoralAlignmentIcon.gameObject.SetActive(false);
        }

        public void SetHolyMoralAlignmentIconActive()
        {
            _neutralMoralAlignmentIcon.gameObject.SetActive(false);
            _darknessMoralAlignmentIcon.gameObject.SetActive(false);
            _holyMoralAlignmentIcon.gameObject.SetActive(true);
        }
    }
}

