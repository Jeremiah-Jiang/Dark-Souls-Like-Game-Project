using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to prevent player from using consumable items while in the process of using one
/// </summary>
public class CharacterIsUsingConsumable : StateMachineBehaviour
{
    public bool isUsingConsumableStatus;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isUsingConsumable", isUsingConsumableStatus);
    }
}
