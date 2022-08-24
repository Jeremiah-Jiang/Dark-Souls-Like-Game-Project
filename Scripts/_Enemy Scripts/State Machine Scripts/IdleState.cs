using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class IdleState : State
    {
        public PursueTargetState pursueTargetState;
        public LayerMask targetDetectionLayer;
        public LayerMask allyDetectionLayer;
        public LayerMask obstacleLayer;
        private float _detectionDelay = 0.5f;
        private float _inFOVDetectionDelay = 0.3f;

        public override State Tick(EnemyManager enemyManager)
        {
            #region Handle Enemy Target Detection
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, targetDetectionLayer);
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, enemyManager.enemyAllyAwarenessRadius, allyDetectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager characterManager = colliders[i].transform.GetComponent<CharacterManager>();

                if (characterManager != null)
                {
                    Vector3 targetsDirection = characterManager.transform.position - enemyManager.transform.position;
                    float viewableAngle = Vector3.Angle(targetsDirection, enemyManager.transform.forward);

                    if (characterManager.GetTeamID() != enemyManager.GetTeamID())
                    {
                        if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                        {
                            if (Physics.Linecast(enemyManager.lockOnTransform.position, characterManager.lockOnTransform.position, obstacleLayer))
                            {
                                return this;
                            }
                            else
                            {
                                enemyManager.currentTarget = characterManager;
                            }
                        }
                    }
                }
            }
            //Debug.Log("Finished checking its own detection radius");
            //Make enemies aware if an ally near them is pursuing a target
            for (int i = 0; i < nearbyColliders.Length; i++)
            {
                CharacterManager characterManager = nearbyColliders[i].transform.GetComponent<CharacterManager>();
                if (characterManager != null)
                {
                    if (characterManager.GetTeamID() == enemyManager.GetTeamID())
                    {
                        if (characterManager is EnemyManager)
                        {
                            EnemyManager allyManager = (EnemyManager)characterManager;
                            if (allyManager.currentTarget != null)
                            {
                                Vector3 targetsDirection = allyManager.transform.position - enemyManager.transform.position;
                                float viewableAngle = Vector3.Angle(targetsDirection, enemyManager.transform.forward);
                                if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                                {
                                    StartCoroutine(DetectionDelay(enemyManager, allyManager, _inFOVDetectionDelay));
                                }
                                else
                                {
                                    StartCoroutine(DetectionDelay(enemyManager, allyManager, _detectionDelay));
                                }

                                //enemy.currentTarget = allyManager.currentTarget;
                            }
                        }
                    }
                }
            }
            #endregion

            #region Handle Switching to Next State
            if (enemyManager.currentTarget != null)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }
            #endregion

            IEnumerator DetectionDelay(EnemyManager enemyManager, EnemyManager allyManager, float delayTime)
            {
                yield return new WaitForSeconds(delayTime);
                enemyManager.currentTarget = allyManager.currentTarget;
            }
        }
    }
}

