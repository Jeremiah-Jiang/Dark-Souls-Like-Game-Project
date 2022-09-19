using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class EquipmentItem : Item
    {
        [Header("Damage Negation")]
        [SerializeField] protected float physicalDamageNegation;
        [SerializeField] protected float magicDamageNegation;
        [SerializeField] protected float fireDamageNegation;
        [SerializeField] protected float lightningDamageNegation;
        [SerializeField] protected float umbraDamageNegation;
        [SerializeField] protected float holyDamageNegation;

        [Header("Status Resistances")]
        [SerializeField] protected float bleedResistance;
        [SerializeField] protected float burnResistance;
        [SerializeField] protected float poisonResistance;
        [SerializeField] protected float darknessResistance;
        [SerializeField] protected float frostResistance;
        [SerializeField] protected float poise;

        [Header("Passive Effects")]
        [SerializeField] protected string passiveEfect01 = "-";
        [SerializeField] protected string passiveEfect02 = "-";
        [SerializeField] protected string passiveEfect03 = "-";

        [Header("Equipment Type")]
        public EquipmentType equipmentType;
        
        public float GetPhysicalDamageNegation()
        {
            return physicalDamageNegation;
        }

        public float GetMagicDamageNegation()
        {
            return magicDamageNegation;
        }

        public float GetFireDamageNegation()
        {
            return fireDamageNegation;
        }

        public float GetLightningDamageNegation()
        {
            return lightningDamageNegation;
        }

        public float GetUmbraDamageNegation()
        {
            return umbraDamageNegation;
        }

        public float GetHolyDamageNegation()
        {
            return holyDamageNegation;
        }

        public float GetBleedResistance()
        {
            return bleedResistance;
        }

        public float GetBurnResistance()
        {
            return burnResistance;
        }

        public float GetPoisonResistance()
        {
            return poisonResistance;
        }

        public float GetDarknessResistance()
        {
            return darknessResistance;
        }

        public float GetPoise()
        {
            return poise;
        }

        public float GetFrostResistance()
        {
            return frostResistance;
        }

        public string GetPassiveEffect01()
        {
            return passiveEfect01;
        }

        public string GetPassiveEffect02()
        {
            return passiveEfect02;
        }

        public string GetPassiveEffect03()
        {
            return passiveEfect03;
        }
    }
}

