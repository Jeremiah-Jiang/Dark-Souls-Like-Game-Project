using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class WorldEventManager : MonoBehaviour
    {
        public List<FogWall> fogWalls;
        UIBossHealthBar bossHealthBar;
        EnemyBossManager boss;
        public bool bossFightIsActive; //currently fighting boss
        public bool bossHasBeenAwakened; //Woke the boss/watched cutscene already but died so don't activate cutscene again
        public bool bossHasBeenDefeated;

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
        }

        public void ActivateBossFight()
        {
            bossFightIsActive = true;
            bossHasBeenAwakened = true;
            bossHealthBar.SetUIHealthBarToActive();
            //Activate fog wall
            foreach(var fogWall in fogWalls)
            {
                fogWall.ActivateFogWall();
            }
        }

        public void BossHasBeenDefeated()
        {
            bossHasBeenDefeated = true;
            bossFightIsActive = false;
            foreach (var fogWall in fogWalls)
            {
                fogWall.DeactivateFogWall();
            }
        }
    }
}

