using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class PursueTargetState : State
    {
        public CombatStanceState combatStanceState;

        public override State Tick(EnemyManager enemy)
        {
            HandleRotateTowardsTarget(enemy);
            //enemy.navmeshAgent.transform.localPosition = Vector3.zero;  
            //enemy.navmeshAgent.transform.localRotation = Quaternion.identity;
            //Debug.Log(enemy.transform.position);

            if (enemy.isInteracting)
                return this;

            if (enemy.isPerformingAction)
            {
                enemy.SetAnimatorFloat("Vertical", 0, 0.1f, Time.deltaTime);
                return this;
            }

            if (enemy.distanceFromTarget > enemy.maximumAggroRadius)
            {
                enemy.transform.position = new Vector3(enemy.navmeshAgent.transform.position.x, enemy.navmeshAgent.transform.position.y, enemy.navmeshAgent.transform.position.z);
                enemy.SetAnimatorFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }

            if (enemy.distanceFromTarget <= enemy.cautionRadius)
            {
                return combatStanceState;
            }
            else
            {
                enemy.transform.position = new Vector3(enemy.navmeshAgent.transform.position.x, enemy.navmeshAgent.transform.position.y, enemy.navmeshAgent.transform.position.z);
                return this;
            }

        }
        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            //If attacking enemy, rotate manually
            if (enemyManager.isPerformingAction)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
            //rotate using navmesh agent (pathfinding)
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navmeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.enemyRigidBody.velocity;

                enemyManager.navmeshAgent.enabled = true;
                enemyManager.navmeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidBody.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navmeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
        }
    }

}

