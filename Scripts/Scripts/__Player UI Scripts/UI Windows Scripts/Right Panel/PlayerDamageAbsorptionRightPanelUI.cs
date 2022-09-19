using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class PlayerDamageAbsorptionRightPanelUI : MonoBehaviour
    {
        [SerializeField]
        Text _physicalDamageAbsorptionValueText;
        [SerializeField]
        Text _fireDamageAbsorptionValueText;
        [SerializeField]
        Text _lightningDamageAbsorptionValueText;
        [SerializeField]
        Text _umbraDamageAbsorptionValueText;
        [SerializeField]
        Text _magicDamageAbsorptionValueText;

        public void SetPhysicalDamageAbsorptionValueText(float physicalDamageAbsorptionValue)
        {
            _physicalDamageAbsorptionValueText.text = physicalDamageAbsorptionValue.ToString();
        }

        public void SetFireDamageAbsorptionValueText(float fireDamageAbsorptionValue)
        {
            _fireDamageAbsorptionValueText.text = fireDamageAbsorptionValue.ToString();
        }

        public void SetLightningDamageAbsorptionValueText(float lightningDamageAbsorptionValue)
        {
            _lightningDamageAbsorptionValueText.text = lightningDamageAbsorptionValue.ToString();
        }

        public void SetUmbraDamageAbsorptionValueText(float umbraDamageAbsorptionValue)
        {
            _umbraDamageAbsorptionValueText.text = umbraDamageAbsorptionValue.ToString();
        }

        public void SetMagicDamageAbsorptionValueText(float magicDamageAbsorptionValue)
        {
            _magicDamageAbsorptionValueText.text = magicDamageAbsorptionValue.ToString();
        }


    }
}

