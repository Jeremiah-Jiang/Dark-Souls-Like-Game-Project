using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public enum WeaponType
    {
        PyromancyCaster,
        FaithCaster,
        SpellCaster,
        StraightSword,
        Shield,
        Dagger,
        Axe,
        Unarmed,
        Bow,
        SmallShield
    }

    public enum DamageType
    {
        Physical = 0,
        Magic = 1,
        Fire = 2,
        Lightning = 3,
        Umbra = 4,
        Holy = 5
    }
    public enum WeaponEffect
    {
        Normal,
        Darkness,
        Holy
    }

    public enum EquipmentType
    {
        Normal,
        Darkness,
        Holy
    }

    public enum AmmoType
    {
        Arrow,
        Bolt
    }

    public enum AttackType
    {
        lightAttack01,
        lightAttack02,
        heavyAttack01,
        heavyAttack02,
    }

    public enum Allies
    {
        Player,
        Vitrusian,
        CreatureOfDarkness,
    }

    public enum AICombatStyle
    {
        common,
        swordAndShield,
        heavy,
        elite,
        archer
    }

    public enum AIAttackActionType
    {
        meleeAttackAction,
        magicAttackAction,
        rangedAttackAction
    }
    public class Enums : MonoBehaviour
    {

    }
}

