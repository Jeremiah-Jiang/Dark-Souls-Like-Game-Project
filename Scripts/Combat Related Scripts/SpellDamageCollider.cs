using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class SpellDamageCollider : DamageCollider
    {
        public GameObject impactParticles;
        public GameObject projectileParticles;
        public GameObject muzzleParticles;

        bool hasCollided = false;

        EnemyManager spellTarget;
        Rigidbody rigidBody;
        Vector3 impactNormal; //Used to rotate impact particles

        protected override void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            projectileParticles = Instantiate(projectileParticles, transform.position, transform.rotation);
            projectileParticles.transform.parent = transform;

            if(muzzleParticles)
            {
                muzzleParticles = Instantiate(muzzleParticles, transform.position, transform.rotation);
                Destroy(muzzleParticles, 2f);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(!hasCollided)
            {
                spellTarget = collision.transform.GetComponent<EnemyManager>();//was characterStatsManager
                if(spellTarget != null && spellTarget.enemyStatsManager.teamID != teamID)
                {
                    if (spellTarget.isBoss)
                    {
                        spellTarget.enemyStatsManager.TakeDamageNoAnimation(characterManager, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage);
                    }
                    else
                    {
                        spellTarget.enemyStatsManager.TakeDamage(characterManager, physicalDamage, magicDamage, fireDamage, lightningDamage, umbraDamage, holyDamage, currentDamageAnimation);
                    }
                }
                hasCollided = true;
                impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
                Destroy(projectileParticles);
                Destroy(impactParticles, 3.1f);
                Destroy(gameObject, 3.1f);
            }
        }
    }

}
