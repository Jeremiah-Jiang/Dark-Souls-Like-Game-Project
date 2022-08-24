using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class WeaponStatsMiddlePanelUI : MonoBehaviour
    {
        [Header("Weapon Information Text")]
        [SerializeField] private Text weaponTypeText;
        [SerializeField] private Text weaponEffectText;

        [Header("Weapon Damage Stats")]
        [SerializeField] private Text physicalDamageText;
        [SerializeField] private Text magicDamageText;
        [SerializeField] private Text fireDamageText;
        [SerializeField] private Text lightningDamageText;
        [SerializeField] private Text umbraDamageText;
        [SerializeField] private Text holyDamageText;
        [SerializeField] private Text criticalDamageText;

        [Header("Weapon Guarded Damage Negation Stats")]
        [SerializeField] private Text weaponGuardedPhysicalDamageNegationText;
        [SerializeField] private Text weaponGuardedMagicDamageNegationText;
        [SerializeField] private Text weaponGuardedFireDamageNegationText;
        [SerializeField] private Text weaponGuardedLightningDamageNegationText;
        [SerializeField] private Text weaponGuardedUmbraDamageNegationText;
        [SerializeField] private Text weaponGuardedHolyDamageNegationText;
        [SerializeField] private Text weaponStabilityText;

        [Header("Weapon Attribute Requirements")]
        [SerializeField] private Text weaponStrengthLevelRequirementText;
        [SerializeField] private Text weaponIntelligenceLevelRequirementText;
        [SerializeField] private Text weaponArcaneLevelRequirementText;
        [SerializeField] private Text weaponDexterityLevelRequirementText;
        [SerializeField] private Text weaponFaithLevelRequirementText;

        [Header("Weapon Attribute Scaling")]
        [SerializeField] private Text weaponStrengthScalingText;
        [SerializeField] private Text weaponIntelligenceScalingText;
        [SerializeField] private Text weaponArcaneScalingText;
        [SerializeField] private Text weaponDexterityScalingText;
        [SerializeField] private Text weaponFaithScalingText;

        [Header("Weapon Passive Effects")]
        [SerializeField] private Text weaponPassiveEffect01;
        [SerializeField] private Text weaponPassiveEffect02;
        [SerializeField] private Text weaponPassiveEffect03;

        /// <summary>
        /// Sets all the weapon information on the middle panel and activates the panel to show the information
        /// </summary>
        /// <param name="weaponItem"></param>
        public void SetAllWeaponStatTexts(WeaponItem weaponItem)
        {
            SetWeaponInformationText(weaponItem);
            SetWeaponAttackPowerText(weaponItem);
            SetWeaponGuardedDamageNegationText(weaponItem);
            SetWeaponAttributeRequirementText(weaponItem);
            SetWeaponAttributeScalingText(weaponItem);
            SetWeaponPassiveEffectsText(weaponItem);
            SetWeaponStatsWindowActive(true);
        }
        private void SetWeaponInformationText(WeaponItem weaponItem)
        {
            weaponTypeText.text = weaponItem.weaponType.ToString();
            weaponEffectText.text = weaponItem.weaponEffect.ToString();
        }

        private void SetWeaponAttackPowerText(WeaponItem weaponItem)
        {
            physicalDamageText.text = weaponItem.GetPhysicalDamage().ToString();
            magicDamageText.text = weaponItem.GetMagicDamage().ToString();
            fireDamageText.text = weaponItem.GetFireDamage().ToString();
            lightningDamageText.text = weaponItem.GetLightningDamage().ToString();
            umbraDamageText.text = weaponItem.GetUmbraDamage().ToString();
            holyDamageText.text = weaponItem.GetHolyDamage().ToString();
            criticalDamageText.text = (weaponItem.GetWeaponCriticalDamageMultiplier() * 100).ToString() + "%";
        }

        private void SetWeaponGuardedDamageNegationText(WeaponItem weaponItem)
        {
            weaponGuardedPhysicalDamageNegationText.text = weaponItem.GetGuardedPhysicalDamageNegation().ToString();
            weaponGuardedMagicDamageNegationText.text = weaponItem.GetGuardedMagicDamageNegation().ToString();
            weaponGuardedFireDamageNegationText.text = weaponItem.GetGuardedFireDamageNegation().ToString();
            weaponGuardedLightningDamageNegationText.text = weaponItem.GetGuardedLightningDamageNegation().ToString();
            weaponGuardedUmbraDamageNegationText.text = weaponItem.GetGuardedUmbraDamageNegation().ToString();
            weaponGuardedHolyDamageNegationText.text = weaponItem.GetGuardedHolyDamageNegation().ToString();
            weaponStabilityText.text = weaponItem.GetWeaponStability().ToString() + "%";
        }

        private void SetWeaponAttributeRequirementText(WeaponItem weaponItem)
        {
            weaponStrengthLevelRequirementText.text = weaponItem.GetStrengthLevelRequirement().ToString();
            weaponIntelligenceLevelRequirementText.text = weaponItem.GetIntelligenceLevelRequirement().ToString();
            weaponArcaneLevelRequirementText.text = weaponItem.GetArcaneLevelRequirement().ToString();
            weaponDexterityLevelRequirementText.text = weaponItem.GetDexterityLevelRequirement().ToString();
            weaponFaithLevelRequirementText.text = weaponItem.GetFaithLevelRequirment().ToString();
        }

        private void SetWeaponAttributeScalingText(WeaponItem weaponItem)
        {
            weaponStrengthScalingText.text = weaponItem.GetStrengthScaling().ToString();
            weaponIntelligenceScalingText.text = weaponItem.GetIntelligenceScaling().ToString();
            weaponArcaneScalingText.text = weaponItem.GetArcaneScaling().ToString();
            weaponDexterityLevelRequirementText.text = weaponItem.GetDexterityScaling().ToString();
            weaponFaithScalingText.text = weaponItem.GetFaithScaling().ToString();
        }

        private void SetWeaponPassiveEffectsText(WeaponItem weaponItem)
        {
            weaponPassiveEffect01.text = weaponItem.GetPassiveEffect01();
            weaponPassiveEffect02.text = weaponItem.GetPassiveEffect02();
            weaponPassiveEffect03.text = weaponItem.GetPassiveEffect03();
        }

        public void SetWeaponStatsWindowActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}

