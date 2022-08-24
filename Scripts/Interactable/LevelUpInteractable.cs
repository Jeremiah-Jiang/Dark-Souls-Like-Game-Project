using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class LevelUpInteractable : Interactable
    {
        Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.Play("Bonfire_Idle");
        }
        public override void Interact(PlayerManager playerManager)
        {
            playerManager.uiManager.levelUpWindow.gameObject.SetActive(true);
        }
    }
}

