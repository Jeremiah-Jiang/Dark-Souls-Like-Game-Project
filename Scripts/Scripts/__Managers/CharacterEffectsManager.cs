using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        CharacterManager characterManager;

        [Header("Current Range FX")]
        public GameObject currentRangeFX;

        [Header("Damage FX")]
        public GameObject bloodSplatterFX;

        [Header("Block FX")]
        public GameObject blockSparksFX;

        [Header("Weapon FX")]
        public WeaponFX rightWeaponFX;
        public WeaponFX leftWeaponFX;

        [Header("Poison FX")]
        public GameObject defaultPoisonParticleFX;
        public GameObject currentPoisonParticleFX;
        public Transform buildUpTransform; //Location of build up particle FX

        public bool isPoisoned;
        public float poisonBuildUp = 0; //The buildup over time that poisons the player AFTER it reaches 100;
        public float poisonAmount = 100; //The amount of poison the player has to process before they are unpoisoned
        public float defaultPoisonAmount = 100;
        public float poisonDamageTick = 2; //Amount of time between each poison damage tick
        float timer;
        public int poisonDamage = 1;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }
        public virtual void PlayWeaponFX(bool isLeft)
        {
            if(isLeft)
            {
                if(leftWeaponFX != null)
                {
                    leftWeaponFX.PlayWeaponFX(characterManager.isPerformingFullyChargedAttack);
                }
            }
            else
            {
                if(rightWeaponFX != null)
                {
                    rightWeaponFX.PlayWeaponFX(characterManager.isPerformingFullyChargedAttack);
                }
            }
        }

        public virtual void PlayBloodSplatterFX(Vector3 bloodSplatterLocation)
        {
            if(bloodSplatterFX != null)
            {
                GameObject blood = Instantiate(bloodSplatterFX, bloodSplatterLocation, Quaternion.identity);
            }
        }

        public virtual void PlayBlockSparksFX(Vector3 blockSparksLocation)
        {
            if(blockSparksFX != null)
            {
                GameObject sparks = Instantiate(blockSparksFX, blockSparksLocation, Quaternion.identity);

            }
        }

        public virtual void HandleAllPoisonEffects()
        {
            if (characterManager.isDead)
                return;
            HandlePoisonBuildUp();
            HandleIsPoisonedEffect();
        }

        protected virtual void HandlePoisonBuildUp()
        {
            if (isPoisoned)
                return;
            if(poisonBuildUp > 0 && poisonBuildUp < 100)
            {
                poisonBuildUp -= Time.deltaTime;
            }
            else if (poisonBuildUp >= 100)
            {
                isPoisoned = true;
                poisonBuildUp = 0;
                if(buildUpTransform != null)
                {
                    currentPoisonParticleFX = Instantiate(defaultPoisonParticleFX, buildUpTransform.transform);
                }
                else
                {
                    //else statement is just a failsafe
                    currentPoisonParticleFX = Instantiate(defaultPoisonParticleFX, characterManager.transform);
                }
            }
        }

        protected virtual void HandleIsPoisonedEffect()
        { 
            if(isPoisoned)
            {
                if(poisonAmount > 0)
                {
                    timer += Time.deltaTime;
                    if(timer >= poisonDamageTick)
                    {
                        characterManager.TakePoisonDamage(poisonDamage);
                        timer = 0;
                    }
                    poisonAmount -= Time.deltaTime;
                }
                else
                {
                    isPoisoned = false;
                    poisonAmount = defaultPoisonAmount;
                    Destroy(currentPoisonParticleFX);
                }
            }
        }
    }

}
