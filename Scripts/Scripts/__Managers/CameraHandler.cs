using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JJ
{
    public class CameraHandler : MonoBehaviour
    {
        PlayerManager playerManager;

        public Transform targetTransform;            //The transform the camera follows (The player)
        public Transform targetTransformWhileAiming; //The transform the camera follows while aiming
        public Transform cameraTransform;
        public Camera cameraObject;
        public Transform cameraPivotTransform;
        private Vector3 cameraTransformPosition;
        public LayerMask ignoreLayers;
        public LayerMask environmentLayer;
        private Vector3 cameraFollowVelocity = Vector3.zero;

        public static CameraHandler singleton;
        //Change to 0.005f when full screen
        public float horizontalLookSpeed = 250f;
        public float horizontalAimingSpeed = 100.0f;
        public float followSpeed = 0.5f;
        //Change to 0.005f when full screen
        public float verticalLookSpeed = 250f;
        public float verticalAimingSpeed = 100.0f;

        private float targetPosition;
        private float defaultPosition;
        private float horizontalAngle;
        private float verticalAngle;
        public float minimumVerticalAngle = -35.0f;
        public float maximumVerticalAngle = 35.0f;

        public float cameraSphereRadius = 0.2f;
        public float cameraCollisionOffset = 0.2f;
        public float minimumCollisionOffset = 0.2f;
        public float lockedPivotPosition = 2.25f;
        public float unlockedPivotPosition = 1.65f;

        List<CharacterManager> availableTargets = new List<CharacterManager>();
        public CharacterManager currentLockOnTarget;
        public CharacterManager nearestLockOnTarget;
        public CharacterManager leftLockOnTarget;
        public CharacterManager rightLockOnTarget;
        public float maximumLockOnDistance = 30.0f;

        private void Awake()
        {
            singleton = this;
            defaultPosition = cameraTransform.localPosition.z;
            //ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
            //ignoreLayers = ~(1 << 0 | 1 << 8 | 1 << 10);
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            playerManager = FindObjectOfType<PlayerManager>();
            targetTransformWhileAiming = playerManager.GetComponentInChildren<TargetTransformWhileAiming>().transform;
            cameraObject = GetComponentInChildren<Camera>();
        }

        private void Start()
        {
            environmentLayer = LayerMask.NameToLayer("Environment");
        }

        public void FollowTarget()
        {
            if(playerManager.isAiming)
            {
                Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransformWhileAiming.position, ref cameraFollowVelocity, Time.deltaTime * followSpeed);
                transform.position = targetPosition;
            }
            else
            {
                Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, Time.deltaTime * followSpeed);
                //Vector3 targetPosition = Vector3.Lerp(myTransform.position, targetTransform.position, delta / followSpeed);
                transform.position = targetPosition;
            }
            HandleCameraCollisions();
        }

        public void HandleCameraRotation()
        {
            if(playerManager.GetLockOnFlag() && currentLockOnTarget != null)
            {
                HandleLockedCameraRotation();
            }
            else if(playerManager.isAiming)
            {
                HandleAimedCameraRotation();
            }
            else
            {
                HandleStandardCameraRotation();
            }
        }
        public void HandleStandardCameraRotation()
        {
            horizontalAngle += (playerManager.GetMouseXValue() * horizontalLookSpeed) * Time.deltaTime;
            verticalAngle -= (playerManager.GetMouseYValue() * verticalLookSpeed) * Time.deltaTime;
            verticalAngle = Mathf.Clamp(verticalAngle, minimumVerticalAngle, maximumVerticalAngle);

            Vector3 rotation = Vector3.zero;
            rotation.y = horizontalAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = verticalAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandleLockedCameraRotation()
        {
            Vector3 direction = currentLockOnTarget.transform.position - transform.position;
            direction.Normalize();
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction); //rotate in the direction created
            transform.rotation = targetRotation;
            direction = currentLockOnTarget.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            targetRotation = Quaternion.LookRotation(direction);
            Vector3 eulerAngle = targetRotation.eulerAngles;
            eulerAngle.y = 0;
            cameraPivotTransform.localEulerAngles = eulerAngle;
        }

        private void HandleAimedCameraRotation()
        {
            Quaternion resetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = resetRotation;
            cameraPivotTransform.rotation = resetRotation;

            Quaternion targetRotationX, targetRotationY;

            Vector3 cameraRotationX = Vector3.zero;
            Vector3 cameraRotationY = Vector3.zero;

            horizontalAngle += (playerManager.GetMouseXValue() * horizontalAimingSpeed) * Time.deltaTime;
            verticalAngle -= (playerManager.GetMouseYValue() * verticalAimingSpeed) * Time.deltaTime;

            cameraRotationY.y = horizontalAngle;
            targetRotationY = Quaternion.Euler(cameraRotationY);
            targetRotationY = Quaternion.Slerp(transform.rotation, targetRotationY, 1);
            transform.localRotation = targetRotationY;

            cameraRotationX.x = verticalAngle;
            targetRotationX = Quaternion.Euler(cameraRotationX);
            targetRotationX = Quaternion.Slerp(cameraTransform.localRotation, targetRotationX, 1);
            cameraTransform.localRotation = targetRotationX;
        }
        
        public void ResetAimCameraRotations()
        {
            cameraTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        private void HandleCameraCollisions()
        {
            targetPosition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();
            //If sphere collides with any collider, SphereCast returns true
            if(Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPosition), ignoreLayers))
            {
                float distance = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetPosition = -(distance - cameraCollisionOffset);
            }

            if(Mathf.Abs(targetPosition) < minimumCollisionOffset)
            {
                targetPosition = -minimumCollisionOffset;
            }

            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, Time.deltaTime / 0.2f);
            cameraTransform.localPosition = cameraTransformPosition;
            
        }

        public void HandleLockOn()
        {
            float shortestDistance = Mathf.Infinity;
            float shortestDistanceOfLeftTarget = -Mathf.Infinity;
            float shortestDistanceOfRightTarget = Mathf.Infinity;
            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26); //generate an invisible sphere that stretches around the player 26 units in diameter

            for(int i = 0; i < colliders.Length; i++)
            {
                CharacterManager character = colliders[i].GetComponent<CharacterManager>();

                if(character != null && !character.isDead)
                {
                    Vector3 lockTargetDirection = character.transform.position - targetTransform.position;
                    float distanceFromTarget = Vector3.Distance(targetTransform.position, character.transform.position);
                    float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward); //Prevent us from locking onto something off-screen
                    RaycastHit hit;
                    //Prevent us from locking onto ourselves
                    if (character.transform.root != targetTransform.transform.root
                        && viewableAngle > -50.0f && viewableAngle < 50.0f
                        && distanceFromTarget <= maximumLockOnDistance)
                    {
                        bool targetIsInSight = Physics.Linecast(playerManager.lockOnTransform.position, character.lockOnTransform.position, out hit);
                        if(targetIsInSight)
                        {
                            Debug.DrawLine(playerManager.lockOnTransform.position, character.lockOnTransform.position);

                            if(hit.transform.gameObject.layer == environmentLayer)
                            {

                            }
                            else
                            {
                                availableTargets.Add(character);
                            }
                        }
                    }
                }
            }

            for(int j = 0; j < availableTargets.Count; j++)
            {
                float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[j].transform.position);

                if(distanceFromTarget < shortestDistance)
                {
                    shortestDistance = distanceFromTarget;
                    nearestLockOnTarget = availableTargets[j];
                }

                if(playerManager.GetLockOnFlag())
                {
                    Vector3 relativeEnemyPosition = playerManager.transform.InverseTransformPoint(availableTargets[j].transform.position);
                    var distanceFromLeftTarget = relativeEnemyPosition.x;
                    var distanceFromRightTarget = relativeEnemyPosition.x;
                    if (relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget > shortestDistanceOfLeftTarget && availableTargets[j] != currentLockOnTarget)
                    {
                        shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                        leftLockOnTarget = availableTargets[j];
                    }

                    else if(relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget && availableTargets[j] != currentLockOnTarget)
                    {
                        shortestDistanceOfRightTarget = distanceFromRightTarget;
                        rightLockOnTarget = availableTargets[j];
                    }    
                }
            }
        }
        
        public void ClearLockOn()
        {
            currentLockOnTarget = null;
            nearestLockOnTarget = null;
            availableTargets.Clear();
        }

        public void SetCameraHeight()
        {
            Vector3 velocity = Vector3.zero;
            Vector3 newLockedPosition = new Vector3(0, lockedPivotPosition);
            Vector3 newUnlockedPosition = new Vector3(0, unlockedPivotPosition);

            if(currentLockOnTarget != null)
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newLockedPosition, ref velocity, Time.deltaTime);
            }
            else
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnlockedPosition, ref velocity, Time.deltaTime);

            }
        }
    }
}
