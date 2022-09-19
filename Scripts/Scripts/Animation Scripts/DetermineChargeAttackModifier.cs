using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class DetermineChargeAttackModifier : StateMachineBehaviour
    {
        private float entryTime;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            entryTime = Time.time;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterManager characterManager = animator.GetComponent<CharacterManager>();
            if(characterManager != null)
            {
                float stateMoveTime = Time.time;
                float percentageOfAnimationPlayed = (stateMoveTime - entryTime) / stateInfo.length;
                float chargeAttackModifier = percentageOfAnimationPlayed + 0.5f;
                characterManager.SetChargeAttackModifier((float)System.Math.Round((double)chargeAttackModifier, 2));
                Debug.Log("Percentage of animation played = " + (float)System.Math.Round((double)percentageOfAnimationPlayed, 2));
                Debug.Log("Charge Attack Modifier = " + (float)System.Math.Round((double)chargeAttackModifier, 2));
            }
        }
    }
}

