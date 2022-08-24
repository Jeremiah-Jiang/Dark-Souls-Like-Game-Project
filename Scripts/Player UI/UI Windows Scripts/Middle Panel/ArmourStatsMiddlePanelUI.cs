using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class ArmourStatsMiddlePanelUI : MonoBehaviour
    {
        [Header("Armour Damage Negation Stats")]
        [SerializeField] private Text armourPhysicalDamageNegationText;
        [SerializeField] private Text armourMagicDamageNegationText;
        [SerializeField] private Text armourFireDamageNegationText;
        [SerializeField] private Text armourLightningDamageNegationText;
        [SerializeField] private Text armourUmbraDamageNegationText;
        [SerializeField] private Text armourHolyDamageNegationText;

        [Header("Armour Resistance Stats")]
        [SerializeField] private Text armourBleedResistanceText;
        [SerializeField] private Text armourBurnResistanceText;
        [SerializeField] private Text armourPoisonResistanceText;
        [SerializeField] private Text armourDarknessResistanceText;
        [SerializeField] private Text armourFrostResistanceText;
        [SerializeField] private Text armourPoiseText;

        [Header("Armour Passive Effects")]
        [SerializeField] private Text armourPassiveEffect01;
        [SerializeField] private Text armourPassiveEffect02;
        [SerializeField] private Text armourPassiveEffect03;

        /// <summary>
        /// Sets all the armour information on the middle panel and activates the panel to show the information
        /// </summary>
        /// <param name="armourItem"></param>
        public void SetAllArmourStatTexts(EquipmentItem armour)
        {
            SetArmourDamageNegationText(armour);
            SetArmourResistanceText(armour);
            SetArmourPassiveEffectsText(armour);
            SetArmourStatsWindowActive(true);
        }
        private void SetArmourDamageNegationText(EquipmentItem armour)
        {
            armourPhysicalDamageNegationText.text = armour.GetPhysicalDamageNegation().ToString();
            armourMagicDamageNegationText.text = armour.GetMagicDamageNegation().ToString();
            armourFireDamageNegationText.text = armour.GetFireDamageNegation().ToString();
            armourLightningDamageNegationText.text = armour.GetLightningDamageNegation().ToString();
            armourUmbraDamageNegationText.text = armour.GetUmbraDamageNegation().ToString();
            armourHolyDamageNegationText.text = armour.GetHolyDamageNegation().ToString();
        }

        private void SetArmourResistanceText(EquipmentItem armour)
        {
            armourBleedResistanceText.text = armour.GetBleedResistance().ToString();
            armourPoisonResistanceText.text = armour.GetPoisonResistance().ToString();
            armourBurnResistanceText.text = armour.GetBurnResistance().ToString();
            armourDarknessResistanceText.text = armour.GetDarknessResistance().ToString();
            armourFrostResistanceText.text = armour.GetFrostResistance().ToString();
            armourPoiseText.text = armour.GetPoise().ToString();
        }

        private void SetArmourPassiveEffectsText(EquipmentItem armour)
        {
            armourPassiveEffect01.text = armour.GetPassiveEffect01();
            armourPassiveEffect02.text = armour.GetPassiveEffect02();
            armourPassiveEffect03.text = armour.GetPassiveEffect03();
        }

        public void SetArmourStatsWindowActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}

