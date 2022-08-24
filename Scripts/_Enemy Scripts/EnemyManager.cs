using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JJ
{
    [RequireComponent(typeof(EnemyStatsManager))]
    [RequireComponent(typeof(EnemyCombatManager))]
    [RequireComponent(typeof(EnemyEffectsManager))]
    [RequireComponent(typeof(EnemyAnimatorManager))]
    [RequireComponent(typeof(EnemyInventoryManager))]
    [RequireComponent(typeof(EnemyWeaponSlotManager))]
    [RequireComponent(typeof(EnemyLocomotionManager))]
    public class EnemyManager : CharacterManager
    {
        public EnemyBossManager enemyBossManager;
        public EnemyStatsManager enemyStatsManager;
        public EnemyEffectsManager enemyEffectsManager;
        public EnemyAnimatorManager enemyAnimatorManager;
        public EnemyLocomotionManager enemyLocomotionManager;
        public EnemyWeaponSlotManager enemyWeaponSlotManager;
        public NavMeshAgent navmeshAgent;
        public Rigidbody enemyRigidBody;

        public State currentState;
        public CharacterManager currentTarget;

        public bool isPerformingAction;
        public float maximumAggroRadius = 1.5f;
        public float cautionRadius = 5.0f;
        public float rotationSpeed = 25.0f;
        
        [Header("A.I Settings")]
        public float detectionRadius = 20.0f;
        public float enemyAllyAwarenessRadius = 5.0f;
        //Higher magnitudes = higher FOV
        public float minimumDetectionAngle = -50.0f;
        public float maximumDetectionAngle = 50.0f;
        public float currentRecoveryTime = 0;

        [Header("Advanced A.I Settings")]
        public bool allowAIToPerformBlock;
        public int blockLikelihood = 50; //Will work like a percentage, 0 - 100%
        public bool allowAIToPerfromDodge;
        public int dodgeLikelihood = 50; //Will work like a percentage, 0 - 100%
        public bool allowAIToPerformParry;
        public int parryLikelihood = 50; //Will work like a percentage, 0 - 100%

        [Header("A.I Combat Settings")]
        public bool allowAIToPerformCombos;
        public bool isPhaseShifting;
        public float comboLikelihood;
        public AICombatStyle combatStyle;

        [Header("A.I Target Information")]
        public float distanceFromTarget;
        public Vector3 targetDirection;
        public float viewableAngle;

        [SerializeField]
        public bool isBoss;
        protected override void Awake()
        {
            base.Awake();
            enemyBossManager = GetComponent<EnemyBossManager>();
            enemyStatsManager = GetComponent<EnemyStatsManager>();
            enemyEffectsManager = GetComponent<EnemyEffectsManager>();
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyWeaponSlotManager = GetComponent<EnemyWeaponSlotManager>();
            backstabCollider = GetComponentInChildren<CriticalDamageCollider>();
            enemyRigidBody = GetComponent<Rigidbody>();
            isBoss = (enemyBossManager == null ? false : true);
            navmeshAgent = GetComponentInChildren<NavMeshAgent>();
            navmeshAgent.enabled = false;
        }

        private void Start()
        {
            enemyRigidBody.isKinematic = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Gizmos.DrawWireSphere(transform.position, enemyAllyAwarenessRadius);

        }

        protected override void Update()
        {
            HandleRecoveryTimer();
            HandleStateMachine();
            //transform.position = new Vector3(navmeshAgent.transform.position.x, navmeshAgent.transform.position.y, navmeshAgent.transform.position.z);
            //navmeshAgent.transform.localPosition = Vector3.zero;
            //navmeshAgent.transform.localRotation = Quaternion.identity;
            isRotatingWithRootMotion = _animator.GetBool("isRotatingWithRootMotion");
            isInteracting = _animator.GetBool("isInteracting");
            isPhaseShifting = _animator.GetBool("isPhaseShifting");
            isInvulnerable = _animator.GetBool("isInvulnerable");
            canDoCombo = _animator.GetBool("canDoCombo");
            canRotate = _animator.GetBool("canRotate");
            _animator.SetBool("isDead", isDead);

            if(currentTarget != null)
            {
                distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
                targetDirection = currentTarget.transform.position - transform.position;
                viewableAngle = Vector3.Angle(targetDirection, transform.forward);
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            enemyEffectsManager.HandleAllPoisonEffects();
        }
        private void LateUpdate()
        {
            navmeshAgent.transform.localPosition = Vector3.zero;
            navmeshAgent.transform.localRotation = Quaternion.identity;
        }

        private void HandleStateMachine()
        {
            if (isDead)
                return;
            if(currentState != null)
            {
                State nextState = currentState.Tick(this);

                if(nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State nextState)
        {
            currentState = nextState;
        }

        private void HandleRecoveryTimer()
        {
            if(currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if(isPerformingAction)
            {
                if(currentRecoveryTime <= 0)
                {
                    isPerformingAction = false;
                }
            }
        }

        public bool canAIPerformBlock()
        {
            return allowAIToPerformBlock;
        }

        public bool canAIPerformDodge()
        {
            return allowAIToPerfromDodge;
        }

        public bool canAIPerformParry()
        {
            return allowAIToPerformParry;
        }
    }
}

