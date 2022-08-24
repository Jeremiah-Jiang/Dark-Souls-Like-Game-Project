using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Item Actions/Draw Arrow Action")]
    public class DrawArrowAction : ItemAction
    {
        public override void PerformAction(CharacterManager characterManager)
        {
            if (characterManager.isInteracting)
                return;
            if (characterManager.isHoldingArrow)
                return;
            characterManager.SetAnimatorBool("isHoldingArrow", true);
            characterManager.PlayTargetAnimation("Bow_TH_Draw_01", true);

            GameObject arrowModel = characterManager.GetCurrentAmmoLoadedItemModel();
            Transform leftHandSlotTransform = characterManager.GetLeftHandSlot().transform;
            GameObject loadedArrow = Instantiate(arrowModel, leftHandSlotTransform);
            characterManager.SetCurrentRangeFX(loadedArrow);

            Animator bowAnimator = characterManager.GetRightHandSlot().GetComponentInChildren<Animator>();
            bowAnimator.SetBool("isDrawn", true);
            bowAnimator.Play("Bow_ONLY_Draw_01");
        }

    }
}

