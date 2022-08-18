using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class PlayerAnimatorManager : CharacterAnimatorManager
    {
        PlayerManager playerManager;

        int vertical;
        int horizontal;

        protected override void Awake()
        {
            base.Awake();
            playerManager = GetComponent<PlayerManager>();
            vertical = (int)Animator.StringToHash("Vertical");
            horizontal = (int)Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            #region Vertical
            float v = 0;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.55f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.55f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion

            if (isSprinting && v != 0)
            {
                v = 2;
                h = horizontalMovement;
            }
            playerManager.SetAnimatorFloat(vertical, v, 0.1f, Time.deltaTime);
            playerManager.SetAnimatorFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        private void OnAnimatorMove()
        {
            if (character.isInteracting == false)
                return;
            float delta = Time.deltaTime;
            playerManager.GetPlayerLocomotionManager().rigidbody.drag = 0;
            Vector3 deltaPosition = playerManager.GetAnimator().deltaPosition;
            Vector3 velocity = deltaPosition / delta;
            playerManager.GetPlayerLocomotionManager().rigidbody.velocity = velocity;
        }

        /// <summary>
        /// <para>This is an animation event to disable collision so that player can pass through colliders (such as fog wall)</para>
        /// <para>DisableCollision() should be paired with EnableCollision() to prevent game breaking</para>
        /// </summary>
        public void DisableCollision()
        {
            playerManager.GetPlayerLocomotionManager().colliders.SetActive(false);
            playerManager.GetPlayerLocomotionManager().characterCollider.enabled = false;
            playerManager.GetPlayerLocomotionManager().characterCollisionBlockerCollider.enabled = false;
        }

        /// <summary>
        /// <para>This is an animation event to enable collision so that player can interact with colliders again</para>
        /// <para>EnableCollision() should be paired with DisableCollision() to prevent game breaking</para>
        /// </summary>
        public void EnableCollision()
        {
            playerManager.GetPlayerLocomotionManager().colliders.SetActive(true);
            playerManager.GetPlayerLocomotionManager().characterCollider.enabled = true;
            playerManager.GetPlayerLocomotionManager().characterCollisionBlockerCollider.enabled = true;
        }
    }
}
