using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        CharacterManager characterManager;
        AudioSource audioSource;
        //Attack grunts

        //Taking Damage Grunts
        [Header("Taking Damage Sounds")]
        public AudioClip[] takingDamageSounds;
        private List<AudioClip> potentialDamageSounds;
        private AudioClip lastDamageSoundPlayed;

        [Header("Weapon Wooshes")]
        private List<AudioClip> potentialWeaponWhooshes;
        private AudioClip lastWeaponWhoosh;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
            audioSource = GetComponent<AudioSource>();
        }
        public virtual void PlayRandomDamageSoundFX()
        {
            potentialDamageSounds = new List<AudioClip>();
            foreach(var damageSound in takingDamageSounds)
            {
                if(damageSound != lastDamageSoundPlayed)
                {
                    potentialDamageSounds.Add(damageSound);
                }
            }

            int randomValue = Random.Range(0, potentialDamageSounds.Count);
            lastDamageSoundPlayed = takingDamageSounds[randomValue];
            audioSource.PlayOneShot(takingDamageSounds[randomValue]);
        }

        public virtual void PlayRandomWeaponWhoosh()
        {
            potentialWeaponWhooshes = new List<AudioClip>();
            if(characterManager.isUsingRightHand)
            {
                WeaponItem rightWeapon = characterManager.GetRightWeapon();
                foreach(var whooshSound in rightWeapon.weaponWhooshes)
                {
                    if(whooshSound != lastWeaponWhoosh)
                    {
                        potentialWeaponWhooshes.Add(whooshSound);
                    }
                }
                int randomValue = Random.Range(0, potentialWeaponWhooshes.Count);
                lastWeaponWhoosh = rightWeapon.weaponWhooshes[randomValue];
                audioSource.PlayOneShot(lastWeaponWhoosh);
            }
            else if(characterManager.isUsingLeftHand)
            {
                WeaponItem leftWeapon = characterManager.GetLeftWeapon();
                foreach (var whooshSound in leftWeapon.weaponWhooshes)
                {
                    if (whooshSound != lastWeaponWhoosh)
                    {
                        potentialWeaponWhooshes.Add(whooshSound);
                    }
                }
                int randomValue = Random.Range(0, potentialWeaponWhooshes.Count);
                lastWeaponWhoosh = leftWeapon.weaponWhooshes[randomValue];
                audioSource.PlayOneShot(lastWeaponWhoosh);
            }
            
        }
    }
}

