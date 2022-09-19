using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JJ
{
    public class PlayerLocomotionManager : MonoBehaviour
    {
        PlayerManager playerManager;
        public Vector3 moveDirection;
        public new Rigidbody rigidbody;

        [Header("Ground & Air Detection Stats")]
        [SerializeField]//0.25
        float groundDetectionRayStartPoint = 0.5f;
        [SerializeField]//0.5
        float minimumDistanceNeededToBeginFall = 1.5f;
        [SerializeField]//0.05
        float groundDirectionRayDistance = 0.05f;
        public LayerMask groundLayer;
        public float inAirTimer;

        [Header("Movement stats")]
        [SerializeField]
        float movementSpeed = 5.0f;
        [SerializeField]
        float walkingSpeed = 3.0f;
        [SerializeField]
        float sprintSpeed = 7.0f;
        [SerializeField]
        float rotationSpeed = 18.0f;
        [SerializeField]
        float fallingSpeed = 350.0f;
        [SerializeField]
        float fallingAccel = 1;

        [Header("Stamina Costs")]
        [SerializeField]
        int rollStaminaCost = 15;
        int backstepStaminaCost = 10;
        int sprintStaminaCost = 1;

        private float prevInAirTimer;

        Vector3 normalVector;
        Vector3 targetPosition;

        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockerCollider;
        public GameObject colliders;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            rigidbody = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            playerManager.isGrounded = true;
            Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true); //so character wont fly up
        }

        #region Movement

        public void HandleRotation()
        {
            if (playerManager.canRotate)
            {
                if(playerManager.isAiming)
                {
                    Quaternion targetRotation = Quaternion.Euler(0, playerManager.cameraHandler.cameraTransform.eulerAngles.y, 0);
                    Quaternion playerRotation = Quaternion.Slerp(base.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    base.transform.rotation = playerRotation;
                }
                else
                {
                    if (playerManager.GetLockOnFlag() == true)
                    {
                        if (playerManager.GetSprintFlag() == true || playerManager.GetRollFlag() == true)
                        {
                            Vector3 targetDir = Vector3.zero;
                            targetDir = playerManager.cameraHandler.cameraTransform.forward * playerManager.GetVerticalMovementValue();
                            targetDir += playerManager.cameraHandler.cameraTransform.right * playerManager.GetHorizontalMovementValue();
                            targetDir.y = 0;

                            if (targetDir == Vector3.zero)
                            {
                                targetDir = base.transform.forward;
                            }
                            Quaternion tr = Quaternion.LookRotation(targetDir);
                            Quaternion targetRotation = Quaternion.Slerp(base.transform.rotation, tr, rotationSpeed * Time.deltaTime);
                            base.transform.rotation = targetRotation;
                        }
                        else
                        {
                            Vector3 rotationDirection = moveDirection;
                            rotationDirection = playerManager.cameraHandler.currentLockOnTarget.transform.position - base.transform.position;
                            rotationDirection.y = 0;
                            rotationDirection.Normalize();
                            Quaternion tr = Quaternion.LookRotation(rotationDirection);
                            Quaternion targetRotation = Quaternion.Slerp(base.transform.rotation, tr, rotationSpeed * Time.deltaTime);
                            base.transform.rotation = targetRotation;
                        }

                    }
                    else
                    {
                        Vector3 targetDir = Vector3.zero;
                        float moveOverride = playerManager.GetMoveAmount();

                        targetDir = playerManager.cameraHandler.cameraObject.transform.forward * playerManager.GetVerticalMovementValue();
                        targetDir += playerManager.cameraHandler.cameraObject.transform.right * playerManager.GetHorizontalMovementValue();

                        targetDir.Normalize();
                        targetDir.y = 0;

                        if (targetDir == Vector3.zero)
                        {
                            targetDir = playerManager.transform.forward;
                        }

                        float rs = rotationSpeed;

                        Quaternion tr = Quaternion.LookRotation(targetDir);
                        Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, rs * Time.deltaTime);
                        playerManager.transform.rotation = targetRotation;
                    }
                }
            }
        }

        public void HandleMovement()
        {
            if (playerManager.GetRollFlag())
                return;
            if (playerManager.isInteracting)
                return;
            moveDirection = playerManager.cameraHandler.cameraObject.transform.forward * playerManager.GetVerticalMovementValue();
            moveDirection += playerManager.cameraHandler.cameraObject.transform.right * playerManager.GetHorizontalMovementValue();
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = movementSpeed;

            if(playerManager.GetSprintFlag() == true && playerManager.GetMoveAmount() > 0.5f)
            {
                speed = sprintSpeed;
                playerManager.isSprinting = true;
                moveDirection *= speed;
                playerManager.DeductStaminaNoDamageTaken(sprintStaminaCost);
            }
            else
            {
                if (playerManager.GetMoveAmount() <= 0.5f)
                {
                    speed = walkingSpeed;
                    moveDirection *= speed; 
                    playerManager.isSprinting = false;
                }
                else
                {
                    moveDirection *= speed;
                    playerManager.isSprinting = false;
                }

            }

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;
            if(playerManager.GetLockOnFlag() == true && playerManager.GetSprintFlag() == false)
            {
                playerManager.UpdateAnimatorValues(playerManager.GetVerticalMovementValue(), playerManager.GetHorizontalMovementValue(), playerManager.isSprinting);
            }
            else
            {
                playerManager.UpdateAnimatorValues(playerManager.GetMoveAmount(), 0, playerManager.isSprinting);
            }
        }

        public void HandleRollingAndSprinting()
        {
            //If player is interacting, disable rolling and sprinting
            if (playerManager.GetAnimatorBool("isInteracting"))
                return;

            if(playerManager.GetRollFlag())
            {
                playerManager.SetRollFlag(false);
                moveDirection = playerManager.cameraHandler.cameraObject.transform.forward * playerManager.GetVerticalMovementValue();
                moveDirection += playerManager.cameraHandler.cameraObject.transform.right * playerManager.GetHorizontalMovementValue();

                if(playerManager.GetMoveAmount() > 0)
                {
                    if (playerManager.GetCurrentStamina() < rollStaminaCost)
                        return;
                    playerManager.PlayTargetAnimation("Rolling", true);
                    playerManager.EraseHandIKForWeapon();
                    CalculateRotation();
                    playerManager.DeductStamina(rollStaminaCost);

                }
                else
                {
                    if (playerManager.GetCurrentStamina() < backstepStaminaCost)
                        return;
                    playerManager.PlayTargetAnimation("Backstep", true);
                    playerManager.EraseHandIKForWeapon();
                    playerManager.DeductStamina(backstepStaminaCost);
                }
            }
        }

        public void HandleFalling(Vector3 moveDirection)
        {
            playerManager.isGrounded = false;
            RaycastHit hit;
            Vector3 origin = transform.position;
            origin.y += groundDetectionRayStartPoint;
            //If raycast comes out and hits sth directly in front of you, you can't move
            if(Physics.Raycast(origin, transform.forward, out hit, 0.4f))
            {
                moveDirection = Vector3.zero;
            }

            if(playerManager.isInAir)
            {
                if(inAirTimer > 1.0f && inAirTimer > prevInAirTimer * 2 && fallingAccel < 10)
                {
                    fallingAccel += 0.5f;
                    prevInAirTimer = inAirTimer;
                }
                float fallingVelocity = fallingSpeed * fallingAccel;
                rigidbody.AddForce(Vector3.down * fallingVelocity);
                //If u need to walk off an edge, you will hop off so u don't get stuck on an edge
                rigidbody.AddForce(moveDirection * fallingSpeed / 10.0f);
            }

            Vector3 direction = moveDirection;
            direction.Normalize();
            origin = origin + direction * groundDirectionRayDistance;

            targetPosition = playerManager.transform.position;
            Debug.DrawRay(origin, Vector3.down * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
            bool groundDetected = (Physics.Raycast(origin, Vector3.down, out hit, minimumDistanceNeededToBeginFall, groundLayer));

            if(groundDetected)
            {
                normalVector = hit.normal;
                Vector3 tp = hit.point;
                playerManager.isGrounded = true;
                targetPosition.y = tp.y;

                if(playerManager.isInAir)
                {
                    if (inAirTimer > 0.5f)
                    {
                        //Debug.Log("You were in the air for " + inAirTimer);
                        playerManager.PlayTargetAnimation("Landing", true);
                        inAirTimer = 0;
                        fallingAccel = 1;
                    }
                    else
                    {
                        Debug.Log("Play empty state");
                        playerManager.PlayTargetAnimation("Empty", false);
                        inAirTimer = 0;
                        fallingAccel = 1;
                    }
                    playerManager.isInAir = false;
                }
            }
            else
            {
                if (playerManager.isGrounded)
                {
                    playerManager.isGrounded = false;
                }
                if(playerManager.isInAir == false)
                {
                    if (playerManager.isInteracting == false)
                    {
                        playerManager.PlayTargetAnimation("Falling", true);
                    }
                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (movementSpeed / 2);
                    playerManager.isInAir = true;
                }
                
                if(playerManager.isInAir && playerManager.isInteracting == false)
                {
                    playerManager.PlayTargetAnimation("Falling", true);
                }

                
            }
            #region SG's code
            /*
            if (Physics.Raycast(origin, Vector3.down, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
            {
                normalVector = hit.normal;
                Vector3 tp = hit.point;
                playerManager.isGrounded = true;
                targetPosition.y = tp.y;

                if (playerManager.isInAir)
                {
                    // if player was in the air for more than 0.5s when the raycast detected the ground, play a landing animation
                    if (inAirTimer > 0.5f)
                    {
                        Debug.Log("You were in the air for " + inAirTimer);
                        animatorHandler.PlayTargetAnimation("Landing", true);
                        inAirTimer = 0;
                    }
                    else
                    {
                        animatorHandler.PlayTargetAnimation("Empty", false);
                        inAirTimer = 0;
                    }
                    playerManager.isInAir = false;
                }
            }
            else
            {
                if (playerManager.isGrounded)
                {
                    playerManager.isGrounded = false;
                }
                if (playerManager.isInAir == false)
                {
                    if (playerManager.isInteracting == false)
                    {
                        Debug.Log("Player is Falling");
                        animatorHandler.PlayTargetAnimation("Falling", true);

                    }
                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (movementSpeed / 2);
                    playerManager.isInAir = true;
                }
            }
            */
            #endregion
            if (playerManager.isGrounded)
            {
                if(playerManager.isInteracting || playerManager.GetMoveAmount() > 0)
                {
                    playerManager.transform.position = Vector3.Lerp(playerManager.transform.position, targetPosition, Time.deltaTime);
                }
                else
                {
                    playerManager.transform.position = targetPosition;
                }
            }
            
            if(playerManager.isInteracting || playerManager.GetMoveAmount() > 0)
            {
                playerManager.transform.position = Vector3.Lerp(playerManager.transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                playerManager.transform.position = targetPosition;
            }
            
        }

        public void HandleJumping()
        {
            if (playerManager.isInteracting)
                return;
            if (playerManager.GetCurrentStamina() <= 0)
                return;
            if (playerManager.GetJumpInput())
            {
                if(playerManager.GetMoveAmount() > 0)
                {
                    moveDirection = playerManager.cameraHandler.cameraObject.transform.forward * playerManager.GetVerticalMovementValue();
                    moveDirection += playerManager.cameraHandler.cameraObject.transform.right * playerManager.GetHorizontalMovementValue();
                    playerManager.PlayTargetAnimation("Jump Forward", true);
                    playerManager.EraseHandIKForWeapon();
                    CalculateRotation();

                }
            }
        }
        #endregion

        private void CalculateRotation()
        {
            moveDirection.y = 0;
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            playerManager.transform.rotation = rotation;
        }
    }
}
