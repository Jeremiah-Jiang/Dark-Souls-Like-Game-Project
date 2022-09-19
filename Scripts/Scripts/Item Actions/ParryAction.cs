using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Item Actions/Parry Action")]

    public class ParryAction : ItemAction
    {
        public override void PerformAction(CharacterManager characterManager)
        {
            if (characterManager.IsInteracting())
                return;
            characterManager.EraseHandIKForWeapon();

            WeaponItem parryingWeapon = characterManager.GetCurrentWeapon();
            if(parryingWeapon.weaponType == WeaponType.SmallShield)
            {
                characterManager.PlayTargetAnimation("Parry", true);
            }
            else if(parryingWeapon.weaponType == WeaponType.Shield)
            {
                characterManager.PlayTargetAnimation("Parry", true);
            }

        }
    }
}

