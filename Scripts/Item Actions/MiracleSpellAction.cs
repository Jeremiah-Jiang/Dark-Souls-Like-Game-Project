using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Item Actions/Miracle Spell Action")]

    public class MiracleSpellAction : ItemAction
    {
        public override void PerformAction(CharacterManager characterManager)
        {
            SpellItem currentSpell = characterManager.GetCurrentSpell();
            if (characterManager.IsInteracting())
                return;
            if (currentSpell != null && currentSpell.isFaithSpell)
            {
                if (characterManager.GetCurrentFocusPoints() >= currentSpell.focusPointCost)
                {
                    currentSpell.AttemptToCastSpell(characterManager, characterManager.IsUsingLeftHand());
                }
                else
                {
                    characterManager.PlayTargetAnimation("Frustrated", true);
                }
            }
        }
    }
}

