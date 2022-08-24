using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "A.I/Enemy Actions/Attack Action")]
    
    public class EnemyAttackAction : EnemyActions 
    {
        public bool canCombo;
        public EnemyAttackAction comboAction;

        public int attackScore = 3;
        public float recoveryTime = 2;

        public float maximumAttackAngle = 35.0f;
        public float minimumAttackAngle = -35.0f;

        public float minimumDistanceNeededToAttack = 0.0f;
        public float maximumDistanceNeededToAttack = 3.0f;
    }
}

