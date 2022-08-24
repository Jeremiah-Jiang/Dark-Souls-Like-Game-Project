using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    /// <summary>
    /// Add this script to every animation state that will result in damage unless it is a charge attack
    /// </summary>
    public class ResetChargeAttackModifier : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterManager characterManager = animator.GetComponent<CharacterManager>();
            if(characterManager != null)
            {
                characterManager.ResetChargeAttackModifier();
            }
        }
    }
}

