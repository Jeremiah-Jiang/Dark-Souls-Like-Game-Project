using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class SpellItem : Item
    {
        public GameObject spellWarmUpFX;
        public GameObject spellCastFX;
        public string spellAnimation;

        [Header("Spell Cost")]
        public int focusPointCost;

        [Header("Spell Type")]
        public bool isFaithSpell;
        public bool isMagicSpell;
        public bool isPyroSpell;

        [Header("Spell Description")]
        [TextArea]
        public string spellDescription;

        public virtual void AttemptToCastSpell(CharacterManager characterManager, bool isLeftHanded)
        {
            Debug.Log("You attempted to cast a spell");
        }
        public virtual void SuccessfullyCastedSpell(CharacterManager characterManager, bool isLeftHanded)
        {
            Debug.Log("You successfully casted a spell");
            characterManager.DeductFocusPoints(focusPointCost);
        }
    }
}

