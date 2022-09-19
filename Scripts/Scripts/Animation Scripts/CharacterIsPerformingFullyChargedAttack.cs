using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIsPerformingFullyChargedAttack : StateMachineBehaviour
{
    public bool characterIsPerformingFullyChargedAttackStatus;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPerformingFullyChargedAttack", characterIsPerformingFullyChargedAttackStatus);
    }
}
