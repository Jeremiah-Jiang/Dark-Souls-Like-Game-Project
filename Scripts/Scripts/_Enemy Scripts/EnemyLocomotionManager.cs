using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JJ
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;

        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockerCollider;
 
        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();

        }
        private void Start()
        {
            Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true); //so character wont fly up
        }

    }
}

