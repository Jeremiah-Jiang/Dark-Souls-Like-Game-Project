using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class RotatePlayerPreview : MonoBehaviour
    {
        PlayerControls playerControls;
        PlayerPreview playerPreview;
        
        public float rotationAmount = 1;
        public float rotationSpeed = 5;

        Vector2 cameraInput;

        Vector3 currentRotation;
        Vector3 targetRotation;

        bool pointerEntered;

        private void Awake()
        {
            playerPreview = FindObjectOfType<PlayerPreview>();
        }
        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();
                playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            playerControls.Enable();
        }

        private void Start()
        {
            currentRotation = playerPreview.transform.eulerAngles;
            targetRotation = playerPreview.transform.eulerAngles;
        }

        private void Update()
        {
            if(pointerEntered == true)
            {
                RotatePlayer();
            }
            
        }

        public void PointerEntered()
        {
            pointerEntered = true;
        }

        public void PointerExit()
        {
            pointerEntered=false;
        }

        public void RotatePlayer()
        {
            if (cameraInput.x > 0)
            {
                targetRotation.y -= rotationAmount;
            }
            else if (cameraInput.x < 0)
            {
                targetRotation.y += rotationAmount;
            }

            currentRotation = Vector3.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            playerPreview.transform.eulerAngles = currentRotation;
        }
    }
}

