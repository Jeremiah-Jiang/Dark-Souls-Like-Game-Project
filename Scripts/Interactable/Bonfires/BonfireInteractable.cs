using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class BonfireInteractable : Interactable
    {
        [Header("Bonfire Teleport Location")]
        public Transform bonfireTeleportLocation;

        [Header("Activation Status")]
        public bool hasBeenActivated;

        [Header("Bonfire Effects")]
        public ParticleSystem activationFX;
        public ParticleSystem fireFX;
        public AudioClip bonfireCracklingSoundFX;
        public AudioClip bonfireActivationSoundFX;

        AudioSource audioSource;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if(hasBeenActivated)
            {
                fireFX.gameObject.SetActive(true);
                fireFX.Play();
                audioSource.PlayOneShot(bonfireCracklingSoundFX);
                audioSource.loop = true;
                interactableText = "Rest";
            }
            else
            {
                interactableText = "Light Bonfire";
            }
        }

        private void Update()
        {
            if(hasBeenActivated && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(bonfireCracklingSoundFX);
                audioSource.loop = true;
            }
        }
        public override void Interact(PlayerManager playerManager)
        {
            Debug.Log("Bonfire Interacted With");

            if(hasBeenActivated)
            {
                if(interactableText != "Leave")
                {
                    playerManager.PlayTargetAnimation("Bonfire_Start", true);
                    playerManager.isResting = true;
                    interactableText = "Leave";
                }
                else
                {
                    playerManager.PlayTargetAnimation("Bonfire_End", true);
                    playerManager.isResting = false;
                    interactableText = "Rest";
                }

            }
            else
            {
                playerManager.PlayTargetAnimation("Bonfire_Activate", true);
                playerManager.uiManager.ActivateBonFirePopUp();
                hasBeenActivated = true;
                interactableText = "Rest";
                activationFX.gameObject.SetActive(true);
                activationFX.Play();
                fireFX.gameObject.SetActive(true);
                fireFX.Play();
                audioSource.PlayOneShot(bonfireActivationSoundFX);
                
            }
        }
    }
}

