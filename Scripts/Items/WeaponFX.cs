using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JJ
{
    public class WeaponFX : MonoBehaviour
    {
        [Header("Weapon FX")]
        public ParticleSystem normalWeaponTrail;
        public ParticleSystem fullyChargedAttackWeaponTrail;
        //public ParticleSystem fireWeaponTrail;
        //Fire Weapon Trail
        //Dark Weapon Trail
        //lightning weapon Trail

        private void Awake()
        {
            //normalWeaponTrail = GetComponentInChildren<ParticleSystem>(); //ok for now since we only have normal weapons
        }
        public void PlayWeaponFX(bool isFullyChargedAttack = false)
        {
            normalWeaponTrail.Stop(); //Ensure particle system behaves correctly
            fullyChargedAttackWeaponTrail.Stop();
            //Try not to use !trail.isPlaying or trail.isStopped, a particle system is only considered to be stopped when all the particles are dead, which will not always be the case on trail.Stop()
            if (!normalWeaponTrail.isEmitting && !fullyChargedAttackWeaponTrail.isEmitting) 
            {
                if (isFullyChargedAttack)
                {
                    StopAllTrailsExcept(fullyChargedAttackWeaponTrail);
                    fullyChargedAttackWeaponTrail.Play();
                }
                else
                {
                    normalWeaponTrail.Play();
                }
            }
        }

        private void StopAllTrailsExcept(ParticleSystem particleSystem)
        {
            if(normalWeaponTrail != particleSystem)
            {
                //Debug.Log("Stopping normal Weapon Trail");
                normalWeaponTrail.Stop();
            }
            if(fullyChargedAttackWeaponTrail != particleSystem)
            {
                //Debug.Log("Stopping Fully Charged Weapon Trail");
                fullyChargedAttackWeaponTrail.Stop();
            }
        }

    }
}

