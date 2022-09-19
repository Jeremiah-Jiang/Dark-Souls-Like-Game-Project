using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ItemBasedAttackAction : MonoBehaviour
    {
        [Header("Attack Type")]
        public AIAttackActionType attackActionType = AIAttackActionType.meleeAttackAction;
        public AttackType attackType = AttackType.lightAttack01;
        public ItemBasedAttackAction comboAction;

        [Header("Action Combo Settings")]
        public bool actionCanCombo = false;

        [Header("Right Hand or Left Hand Action")]
        public bool isRightHandedAction = true;

        [Header("Action Settings")]
        public int attackScore = 3;
        public float recoveryTime = 2;
        public float maximumAttackAngle = 35.0f;
        public float minimumAttackAngle = -35.0f;
        public float minimumDistanceNeededToAttack = 0.0f;
        public float maximumDistanceNeededToAttack = 3.0f;
        public string actionAnimation;

        public void PerformAttackAction(EnemyManager enemyManager)
        {
            if(isRightHandedAction)
            {
                enemyManager.UpdateWhichHandCharacterIsUsing(true);
                PerformRightHandItemActionBasedOnAttackType(enemyManager);
            }
            else
            {
                enemyManager.UpdateWhichHandCharacterIsUsing(false);
                PerformLeftHandItemActionBasedOnAttackType(enemyManager);
            }
        }

        private void PerformRightHandItemActionBasedOnAttackType(EnemyManager enemyManager)
        {
            switch(attackActionType)
            {
                case AIAttackActionType.meleeAttackAction:
                    PerformRightHandedMeleeAction(enemyManager);
                    break;
                case AIAttackActionType.rangedAttackAction:
                    break;
                case AIAttackActionType.magicAttackAction:
                    break;
            }
        }

        private void PerformLeftHandItemActionBasedOnAttackType(EnemyManager enemyManager)
        {
            switch (attackActionType)
            {
                case AIAttackActionType.meleeAttackAction:
                    PerformLeftHandedMeleeAction(enemyManager);
                    break;
                case AIAttackActionType.rangedAttackAction:
                    break;
                case AIAttackActionType.magicAttackAction:
                    break;
            }
        }

        private void PerformRightHandedMeleeAction(EnemyManager enemyManager)
        {
            if(enemyManager.isTwoHanding)
            {
                if(attackType == AttackType.lightAttack01)
                {
                    enemyManager.GetRightWeapon().th_Tap_RB_Action.PerformAction(enemyManager);
                }
                else if (attackType == AttackType.heavyAttack01)
                {
                    enemyManager.GetRightWeapon().th_Tap_RT_Action.PerformAction(enemyManager);
                }
            }
            else
            {
                if (attackType == AttackType.lightAttack01)
                {
                    enemyManager.GetRightWeapon().oh_Tap_RB_Action.PerformAction(enemyManager);
                }
                else if (attackType == AttackType.heavyAttack01)
                {
                    enemyManager.GetRightWeapon().oh_Tap_RT_Action.PerformAction(enemyManager);
                }
            }
        }

        private void PerformLeftHandedMeleeAction(EnemyManager enemyManager)
        {
            if (enemyManager.isTwoHanding)
            {
                if (attackType == AttackType.lightAttack01)
                {
                    enemyManager.GetLeftWeapon().th_Tap_RB_Action.PerformAction(enemyManager);
                }
                else if (attackType == AttackType.heavyAttack01)
                {
                    enemyManager.GetLeftWeapon().th_Tap_RT_Action.PerformAction(enemyManager);
                }
            }
            else
            {
                if (attackType == AttackType.lightAttack01)
                {
                    enemyManager.GetLeftWeapon().oh_Tap_RB_Action.PerformAction(enemyManager);
                }
                else if (attackType == AttackType.heavyAttack01)
                {
                    enemyManager.GetLeftWeapon().oh_Tap_RT_Action.PerformAction(enemyManager);
                }
            }
        }
    }

}

