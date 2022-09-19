using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class BombDamageCollider : DamageCollider
    {
        [Header("Explosive Damage & Radius")]
        public int explosiveRadius = 1;
        public int impactDamage;
        public int explosionSplashDamage;
        public Rigidbody bombRigidbody;

        private bool hasCollided = false;
        public GameObject impactParticles;
        protected override void Awake()
        {
            damageCollider = GetComponent<Collider>();
            bombRigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Firebomb collided with " + collision.transform);
            if(collision.transform.parent!= null)
            {
                Debug.Log("Firebomb collided with " + collision.transform.parent);

            }
            if (!hasCollided)
            {
                hasCollided = true;
                impactParticles = Instantiate(impactParticles, transform.position, Quaternion.identity);
                Explode();
                EnemyManager character = collision.transform.GetComponent<EnemyManager>(); //was characterStatsManager

                if(character != null && character.enemyStatsManager.teamID != teamID)
                {
                    if(character.isBoss)
                    {
                        character.enemyStatsManager.TakeDamageNoAnimation(characterManager, impactDamage, 0, 0, 0, 0, 0);
                    }
                    else
                    {
                        character.enemyStatsManager.TakeDamage(characterManager, impactDamage, 0, 0, 0, 0, 0, currentDamageAnimation);
                    }
                }

                Destroy(impactParticles, 5f);
                Destroy(transform.parent.parent.gameObject);
            }
        }

        private void Explode()
        {
            Collider[] characters = Physics.OverlapSphere(transform.position, explosiveRadius);
            foreach(Collider charactersInExplosion in characters)
            {
                EnemyManager character = charactersInExplosion.GetComponent<EnemyManager>();
                if(character != null && character.GetTeamID() != teamID)
                {
                    if(character.isBoss)
                    {
                        character.enemyStatsManager.TakeDamageNoAnimation(characterManager, 0, 0 ,explosionSplashDamage, 0, 0, 0);
                    }
                    else
                    {
                        character.enemyStatsManager.TakeDamage(characterManager, 0, 0, explosionSplashDamage, 0, 0, 0, currentDamageAnimation);
                    }
                }
            }
        }

    }
}

