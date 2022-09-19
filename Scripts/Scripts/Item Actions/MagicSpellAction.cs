using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Item Actions/Magic Spell Action")]

    public class MagicSpellAction : ItemAction
    {
        public override void PerformAction(CharacterManager characterManager)
        {
            SpellItem currentSpell = characterManager.GetCurrentSpell();
            if (characterManager.isInteracting)
                return;
            if (currentSpell != null && currentSpell.isMagicSpell)
            {
                if (characterManager.GetCurrentFocusPoints() >= currentSpell.focusPointCost)
                {
                    currentSpell.AttemptToCastSpell(characterManager, characterManager.isUsingLeftHand);
                }
                else
                {
                    characterManager.PlayTargetAnimation("Frustrated", true);
                }
            }
        }
    }
}

