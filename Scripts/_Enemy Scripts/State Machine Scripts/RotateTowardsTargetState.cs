using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class RotateTowardsTargetState : State
    {
        public CombatStanceState combatStanceState;
        public override State Tick(EnemyManager enemy)
        {
            /*
            enemy.SetAnimatorFloat("Vertical", 0);
            enemy.SetAnimatorFloat("Horizontal", 0);
            */
            /*
            if (enemy.isInteracting)
                return this;
            */
            // when we enter the state, we will still be interacting from the attack animation so we pause here until it has finished
            /*
            if(enemy.viewableAngle >= 100 && enemy.viewableAngle <= 180 && !enemy.isInteracting)
            {
                enemy.enemyAnimatorManager.PlayTargetAnimationWithRootRotation("Turn_Behind", true);
                return combatStanceState;
            }
            else if(enemy.viewableAngle <= -101 && enemy.viewableAngle >= -180 && !enemy.isInteracting)
            {
                enemy.enemyAnimatorManager.PlayTargetAnimationWithRootRotation("Turn_Behind", true);
                return combatStanceState;
            }
            else if(enemy.viewableAngle <= -45 && enemy.viewableAngle >= -100 && !enemy.isInteracting)
            {
                enemy.enemyAnimatorManager.PlayTargetAnimationWithRootRotation("Turn_Right", true);
                return combatStanceState;
            }
            else if(enemy.viewableAngle >= 45 && enemy.viewableAngle <= 100 && !enemy.isInteracting)
            {
                enemy.enemyAnimatorManager.PlayTargetAnimationWithRootRotation("Turn_Left", true);
                return this;
            }
            */
            return combatStanceState;
        }
    }
}

