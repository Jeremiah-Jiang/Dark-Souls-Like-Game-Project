using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Spells/Healing Spell")]
    public class HealingSpell : SpellItem
    {
        public int healAmount;

        public override void AttemptToCastSpell(CharacterManager characterManager, bool isLeftHanded)
        {
            base.AttemptToCastSpell(characterManager, isLeftHanded);
            GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, characterManager.transform);
            characterManager.PlayTargetAnimation(spellAnimation, true, false, isLeftHanded);
        }

        public override void SuccessfullyCastedSpell(CharacterManager characterManager, bool isLeftHanded)
        {
            base.SuccessfullyCastedSpell(characterManager, isLeftHanded);
            GameObject instatiatedSpellFX = Instantiate(spellCastFX, characterManager.transform);
            PlayerManager playerManager = characterManager as PlayerManager;
            if(playerManager != null)
            {
                playerManager.HealPlayer(healAmount);
            }
        }
    }
}


