using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JJ
{
    public class InputManager : MonoBehaviour
    {
        PlayerControls inputActions;
        PlayerManager playerManager;

        [Header("Movement Values")]
        private float _horizontal;
        private float _vertical;
        private float _moveAmount;

        [Header("Mouse Values")]
        private float _mouseX;
        private float _mouseY;

        [Header("Player Action Input Bools")]
        [SerializeField]
        private bool _interactInput;
        [SerializeField]
        private bool _rollInput;
        [SerializeField]
        private bool _useConsumeableInput;
        [SerializeField]
        private bool _useTwoHandInput;
        [SerializeField]
        private bool _jumpInput;
        [SerializeField]
        private bool _inventoryInput;
        [SerializeField]
        private bool _dPadUpInput;
        [SerializeField]
        private bool _dPadDownInput;
        [SerializeField]
        private bool _dPadLeftInput;
        [SerializeField]
        private bool _dPadRightInput;
        [SerializeField]
        private bool _lockOnInput;
        [SerializeField]
        private bool _rightStickRightInput;
        [SerializeField]
        private bool _rightStickLeftInput;

        [Header("Weapon Action Input Bools")]
        [SerializeField]
        private bool _tapRBInput;
        [SerializeField]
        private bool _holdRBInput;
        [SerializeField]
        private bool _tapLBInput;
        [SerializeField]
        private bool _holdLBInput;
        [SerializeField]
        private bool _tapRTInput;
        [SerializeField]
        private bool _holdRTInput;
        [SerializeField]
        private bool _tapLTInput;

        [Header("Player Flags")]
        [SerializeField]
        private bool _rollFlag;
        [SerializeField]
        private bool _sprintFlag;
        [SerializeField]
        private bool _comboFlag;
        [SerializeField]
        private bool _lockOnFlag;
        [SerializeField]
        private bool _inventoryFlag;
        [SerializeField]
        private bool _twoHandFlag;

        private float _rollInputTimer; //Decide if we roll or sprint

        public Transform _criticalAttackRayCastStartPoint;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            //Cursor.visible = false;
        }
        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

                inputActions.PlayerActions.TapRB.performed += i => _tapRBInput = true;
                inputActions.PlayerActions.HoldRB.performed += i => _holdRBInput = true;
                inputActions.PlayerActions.HoldRB.canceled += i => _holdRBInput = false;

                inputActions.PlayerActions.TapLB.performed += i => _tapLBInput = true;
                inputActions.PlayerActions.HoldLB.performed += i => _holdLBInput = true;
                inputActions.PlayerActions.HoldLB.canceled += i => _holdLBInput = false;

                inputActions.PlayerActions.TapRT.performed += i => _tapRTInput = true;
                inputActions.PlayerActions.HoldRT.performed += i => _holdRTInput = true;
                inputActions.PlayerActions.HoldRT.canceled += i => _holdRTInput = false;

                inputActions.PlayerActions.Parry.performed += i => _tapLTInput = true;
                inputActions.Quickslots.DPadRight.performed += i => _dPadRightInput = true;
                inputActions.Quickslots.DPadLeft.performed += i => _dPadLeftInput = true;
                inputActions.Quickslots.DPadDown.performed += i => _dPadDownInput = true;
                inputActions.Quickslots.DPadUp.performed += i => _dPadUpInput = true;
                inputActions.PlayerActions.Interact.performed += i => _interactInput = true;
                inputActions.PlayerActions.X.performed += i => _useConsumeableInput = true;
                inputActions.PlayerActions.Roll.performed += inputActions => _rollInput = true;
                inputActions.PlayerActions.Roll.canceled += i => _rollInput = false;
                inputActions.PlayerActions.Jump.performed += i => _jumpInput = true;
                inputActions.PlayerActions.Inventory.performed += i => _inventoryInput = true;
                inputActions.PlayerActions.LockOn.performed += i => _lockOnInput = true;
                inputActions.PlayerActions.LockOnTargetRight.performed += i => _rightStickRightInput = true;
                inputActions.PlayerActions.LockOnTargetLeft.performed += i => _rightStickLeftInput = true;
                inputActions.PlayerActions.TwoHand.performed += inputActions => _useTwoHandInput = true;
                //inputActions.PlayerActions.CriticalAttack.performed += i => critical_Attack_Input = true;
                
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput()
        {
            if (playerManager.isDead)
                return;
            HandleMoveInput();
            HandleRollInput();

            HandleTapRBInput();
            HandleTapLBInput();
            HandleTapRTInput();
            HandleTapLTInput();
            HandleHoldRBInput();
            HandleHoldLBInput();
            HandleHoldRTInput();

            if(!_inventoryFlag)
            {
                HandleQuickSlotInput();
            }
            HandleInventoryInput();

            HandleLockOnInput();
            HandleTwoHandInput();
            HandleUseConsumableInput();

        }

        private void HandleMoveInput()
        {
            if(_inventoryFlag)
            {
                _horizontal = 0;
                _vertical = 0;
                _mouseX = 0;
                _mouseY = 0;
                return;
            }
            if(playerManager.isHoldingArrow)
            {
                _horizontal = movementInput.x;
                _vertical = movementInput.y;
                _moveAmount = Mathf.Clamp01((Mathf.Abs(_horizontal) + Mathf.Abs(_vertical)));
                if(_moveAmount > 0.5f)
                {
                    _moveAmount = 0.5f;
                }
                _mouseX = cameraInput.x;
                _mouseY = cameraInput.y;
            }
            else
            {
                _horizontal = movementInput.x;
                _vertical = movementInput.y;
                _moveAmount = Mathf.Clamp01(Mathf.Abs(_horizontal) + Mathf.Abs(_vertical));
                _mouseX = cameraInput.x;
                _mouseY = cameraInput.y;
            }
        }

        private void HandleRollInput()
        {
            if (_rollInput)
            {
                _rollInputTimer += Time.deltaTime;
                if(playerManager.GetCurrentStamina() <= 0)
                {
                    _rollInput = false;
                    _sprintFlag = false;
                }
                if(_moveAmount > 0.5f && playerManager.GetCurrentStamina() > 0)
                {
                    _sprintFlag = true;
                }
            }
            else
            {
                _sprintFlag = false;

                if (_rollInputTimer > 0 && _rollInputTimer < 0.5f)
                {
                    _rollFlag = true;
                }
                _rollInputTimer = 0;
            }
        }

        private void HandleTapRBInput()
        {
            // RB input handles Right Hand Weapon's Light Attack 
            if(_tapRBInput)
            {
                _tapRBInput = false;
                ItemAction rightWeaponTapRBAction;
                if(playerManager.isTwoHanding)
                {
                    rightWeaponTapRBAction = playerManager.GetRightWeapon().th_Tap_RB_Action;
                }
                else
                {
                    rightWeaponTapRBAction = playerManager.GetRightWeapon().oh_Tap_RB_Action;
                }
                if(rightWeaponTapRBAction != null)
                {
                    playerManager.UpdateWhichHandCharacterIsUsing(true);
                    playerManager.SetCurrentItemBeingUsed(playerManager.GetRightWeapon());
                    rightWeaponTapRBAction.PerformAction(playerManager);
                }
            }
        }

        private void HandleHoldRBInput()
        {
            if (_holdRBInput)
            {
                ItemAction rightWeaponHoldRBAction;
                if (playerManager.isTwoHanding)
                {
                     rightWeaponHoldRBAction = playerManager.GetRightWeapon().th_Hold_RB_Action;
                }
                else
                {
                    rightWeaponHoldRBAction = playerManager.GetRightWeapon().oh_Hold_RB_Action;
                }
                if(rightWeaponHoldRBAction != null)
                {
                    playerManager.UpdateWhichHandCharacterIsUsing(true);
                    playerManager.SetCurrentItemBeingUsed(playerManager.GetRightWeapon());
                    rightWeaponHoldRBAction.PerformAction(playerManager);
                }
            }
        }

        private void HandleTapLBInput()
        {
            if (_tapLBInput)
            {
                _tapLBInput = false;
                if (playerManager.isTwoHanding)
                {
                    ItemAction rightWeaponTapLBAction = playerManager.GetRightWeapon().th_Tap_LB_Action;
                    if (rightWeaponTapLBAction != null)
                    {
                        playerManager.UpdateWhichHandCharacterIsUsing(true);
                        playerManager.SetCurrentItemBeingUsed(playerManager.GetRightWeapon());
                        rightWeaponTapLBAction.PerformAction(playerManager);
                    }
                }
                else
                {
                    ItemAction leftWeaponTapLBAction = playerManager.GetLeftWeapon().oh_Tap_LB_Action;
                    if (leftWeaponTapLBAction != null)
                    {
                        playerManager.UpdateWhichHandCharacterIsUsing(false);
                        playerManager.SetCurrentItemBeingUsed(playerManager.GetLeftWeapon());
                        leftWeaponTapLBAction.PerformAction(playerManager);
                    }
                }
            }
        }

        private void HandleHoldLBInput()
        {
            if (playerManager.isInAir || playerManager.isSprinting || playerManager.isFiringSpell)
            {
                _holdLBInput = false;
                return;
            }

            if (_holdLBInput)
            {
                if (playerManager.isTwoHanding)
                {
                    ItemAction rightWeaponHoldLBAction = playerManager.GetRightWeapon().th_Hold_LB_Action;
                    if(rightWeaponHoldLBAction != null)
                    {
                        playerManager.UpdateWhichHandCharacterIsUsing(true);
                        playerManager.SetCurrentItemBeingUsed(playerManager.GetRightWeapon());
                        rightWeaponHoldLBAction.PerformAction(playerManager);
                    }
                }
                else
                {
                    ItemAction leftWeaponHoldLBAction = playerManager.GetLeftWeapon().oh_Hold_LB_Action;
                    if(leftWeaponHoldLBAction != null)
                    {
                        playerManager.UpdateWhichHandCharacterIsUsing(false);
                        playerManager.SetCurrentItemBeingUsed(playerManager.GetLeftWeapon());
                        leftWeaponHoldLBAction.PerformAction(playerManager);
                    }
                }
            }
            else if (_holdLBInput == false)
            {
                if (playerManager.isAiming)
                {
                    playerManager.isAiming = false;
                    playerManager.uiManager.SetCrossHairActive(false);
                    playerManager.cameraHandler.ResetAimCameraRotations();
                }
                if(playerManager.isBlocking)
                {
                    playerManager.isBlocking = false;
                }
            }
        }

        private void HandleTapRTInput()
        {
            if (_tapRTInput)
            {
                _tapRTInput = false;
                ItemAction rightWeaponTapRTAction;
                if (playerManager.isTwoHanding)
                {
                    rightWeaponTapRTAction = playerManager.GetRightWeapon().th_Tap_RT_Action;
                }
                else
                {
                    rightWeaponTapRTAction = playerManager.GetRightWeapon().oh_Tap_RT_Action;
                }
                if (rightWeaponTapRTAction != null)
                {
                    playerManager.UpdateWhichHandCharacterIsUsing(true);
                    playerManager.SetCurrentItemBeingUsed(playerManager.GetRightWeapon());
                    rightWeaponTapRTAction.PerformAction(playerManager);
                }
            }
        }

        private void HandleHoldRTInput()
        {
            playerManager.SetAnimatorBool("isChargingAttack", _holdRTInput);

            if(_holdRTInput)
            {
                playerManager.UpdateWhichHandCharacterIsUsing(true);
                playerManager.SetCurrentItemBeingUsed(playerManager.GetRightWeapon());
                if (playerManager.isTwoHanding)
                {
                    if(playerManager.GetRightWeapon().th_Hold_RT_Action != null)
                    {
                        playerManager.GetRightWeapon().th_Hold_RT_Action.PerformAction(playerManager);
                    }
                }
                else
                {
                    if (playerManager.GetRightWeapon().oh_Hold_RT_Action != null)
                    {
                        playerManager.GetRightWeapon().oh_Hold_RT_Action.PerformAction(playerManager);
                    }
                }
            }
        }

        private void HandleTapLTInput()
        {
            if (_tapLTInput)
            {
                _tapLTInput = false;
                if(playerManager.isTwoHanding)
                {
                    ItemAction rightWeaponTapLBAction = playerManager.GetRightWeapon().oh_Tap_LT_Action;
                    if(rightWeaponTapLBAction != null)
                    {
                        playerManager.UpdateWhichHandCharacterIsUsing(true);
                        playerManager.SetCurrentItemBeingUsed(playerManager.GetRightWeapon());
                        playerManager.GetRightWeapon().oh_Tap_LT_Action.PerformAction(playerManager);
                    }
                }
                else
                {
                    ItemAction leftWeaponTapLBAction = playerManager.GetLeftWeapon().oh_Tap_LT_Action;
                    if(leftWeaponTapLBAction != null)
                    {
                        playerManager.UpdateWhichHandCharacterIsUsing(false);
                        playerManager.SetCurrentItemBeingUsed(playerManager.GetLeftWeapon());
                        playerManager.GetLeftWeapon().oh_Tap_LT_Action.PerformAction(playerManager);
                    }
                }
            }
        }

        private void HandleQuickSlotInput()
        {
            if (_dPadRightInput)
            {
                playerManager.ChangeRightWeapon();
            }
            else if(_dPadLeftInput)
            {
                playerManager.ChangeLeftWeapon();
            }
            else if(_dPadDownInput)
            {
                playerManager.ChnageConsumableItem();
            }
        }

        private void HandleInventoryInput()
        {
            if(_inventoryFlag)
            {
                playerManager.uiManager.UpdateUI();
            }

            if (_inventoryInput)
            {
                _inventoryFlag = !_inventoryFlag;

                if(_inventoryFlag)
                {
                    playerManager.uiManager.OpenSelectWindow(true);
                    playerManager.uiManager.SetHUDWindowActive(false);
                }
                else
                {
                    playerManager.uiManager.OpenSelectWindow(false);
                    playerManager.uiManager.CloseAllInventoryWindows();
                    playerManager.uiManager.SetHUDWindowActive(true);
                }
            }
        }

        private void HandleLockOnInput()
        {
            //Enable Lock On
            if(_lockOnInput && _lockOnFlag == false)
            {
                _lockOnInput = false;
                playerManager.cameraHandler.HandleLockOn();
                //Only lock on if there is a nearest target to lock on to
                if (playerManager.cameraHandler.nearestLockOnTarget != null)
                {
                    playerManager.cameraHandler.currentLockOnTarget = playerManager.cameraHandler.nearestLockOnTarget;
                    _lockOnFlag = true;

                }
            }
            //Disable Lock On
            else if (_lockOnInput && _lockOnFlag)
            {
                _lockOnInput = false;
                _lockOnFlag = false;
                playerManager.cameraHandler.ClearLockOn();
            }

            if(_lockOnFlag && _rightStickLeftInput)
            {
                _rightStickLeftInput = false;
                playerManager.cameraHandler.HandleLockOn();
                if(playerManager.cameraHandler.leftLockOnTarget != null)
                {
                    playerManager.cameraHandler.currentLockOnTarget = playerManager.cameraHandler.leftLockOnTarget;
                }
            }

            else if(_lockOnFlag && _rightStickRightInput)
            {

                _rightStickRightInput = false;
                playerManager.cameraHandler.HandleLockOn();
                if(playerManager.cameraHandler.rightLockOnTarget != null)
                {
                    playerManager.cameraHandler.currentLockOnTarget = playerManager.cameraHandler.rightLockOnTarget;
                }
            }

            if(playerManager.cameraHandler != null)
            {
                playerManager.cameraHandler.SetCameraHeight();

            }
        }

        private void HandleTwoHandInput()
        {
            if(_useTwoHandInput)
            {
                _useTwoHandInput = false;
                _twoHandFlag = !_twoHandFlag; //toggle flag

                if(_twoHandFlag)
                {
                    playerManager.isTwoHanding = true;
                    playerManager.LoadWeaponOnSlot(playerManager.GetRightWeapon(), false);
                    playerManager.LoadWeaponOnSlot(playerManager.GetLeftWeapon(), true);
                    playerManager.LoadTwoHandIKTargets(true);
                }
                else
                {
                    playerManager.isTwoHanding = false;
                    playerManager.LoadWeaponOnSlot(playerManager.GetRightWeapon(), false);
                    playerManager.LoadWeaponOnSlot(playerManager.GetLeftWeapon(), true);
                    playerManager.LoadTwoHandIKTargets(false);
                }
            }
        }
        
        private void HandleUseConsumableInput()
        {
            if(_useConsumeableInput)
            {
                _useConsumeableInput = false;
                if(playerManager.IsUsingConsumable() == false) //so player cannot spam consumable and break the system
                {
                    ConsumableItem currentConsumableItem = playerManager.GetCurrentConsumableItem();
                    if (currentConsumableItem != null)
                    {
                        currentConsumableItem.AttemptToConsumeItem(playerManager);
                    }
                }
            }
        }

        //Geter methods for Transform Fields
        public Transform GetCriticalAttackRayCastStartPoint()
        {
            return _criticalAttackRayCastStartPoint;
        }
        //Getter methods for Movement Values
        #region Getter methods for Movement Values
        public float GetMoveAmount()
        {
            return _moveAmount;
        }

        public float GetHorizontalMovementValue()
        {
            return _horizontal;
        }

        public float GetVerticalMovementValue()
        {
            return _vertical;
        }

        public float GetMouseXValue()
        {
            return _mouseX;
        }

        public float GetMouseYValue()
        {
            return _mouseY;
        }
        #endregion

        //Setter methods for Movement Values
        #region Setter methods for Movement Values
        public void SetMoveAmount(float value)
        {
            _moveAmount = value;
        }

        public void SetHorizontalMovementValue(float value)
        {
            _horizontal = value;
        }

        public void SetVerticalMovementValue(float value)
        {
            _vertical = value;
        }
        #endregion

        //Getter methods for Player Flags
        #region Getter methods for Player Flags
        public bool GetRollFlag()
        {
            return _rollFlag;
        }

        public bool GetSprintFlag()
        {
            return _sprintFlag;
        }

        public bool GetComboFlag()
        {
            return _comboFlag;
        }

        public bool GetLockOnFlag()
        {
            return _lockOnFlag;
        }

        public bool GetInventoryFlag()
        {
            return _inventoryFlag;
        }

        public bool GetTwoHandFlag()
        {
            return _twoHandFlag;
        }
        #endregion

        //Setter methods for Player Flags
        #region Setter methods for Player Flags
        public void SetRollFlag(bool value)
        {
            _rollFlag = value;
        }

        public void SetSprintFlag(bool value)
        {
            _sprintFlag = value;
        }

        public void SetComboFlag(bool value)
        {
            _comboFlag = value;
        }

        public void SetLockOnFlag(bool value)
        {
            _lockOnFlag = value;
        }

        public void SetInventoryFlag(bool value)
        {
            _inventoryFlag = value;
        }

        public void SetTwoHandFlag(bool value)
        {
            _twoHandFlag = value;
        }
        #endregion

        //Getter methods for Input Action Fields
        #region Getter methods for Input Action Fields
        public bool GetInteractInput()
        {
            return _interactInput;
        }

        public bool GetRollInput()
        {
            return _rollInput;
        }

        public bool GetUseConsumableInput()
        {
            return _useConsumeableInput;
        }

        public bool GetUseTwoHandInput()
        {
            return _useTwoHandInput;
        }

        public bool GetJumpInput()
        {
            return _jumpInput;
        }

        public bool GetInventoryInput()
        {
            return _inventoryInput;
        }

        public bool GetDPadUpInput()
        {
            return _dPadUpInput;
        }

        public bool GetDPadDownInput()
        {
            return _dPadDownInput;
        }

        public bool GetDPadLeftInput()
        {
            return _dPadLeftInput;
        }

        public bool GetDPadRightInput()
        {
            return _dPadRightInput;
        }

        public bool GetLockOnInput()
        {
            return _lockOnInput;
        }
        #endregion

        //Setter methods for Input Action Fields
        #region Setter methods for Input Action Fields
        public void SetInteractInput(bool value)
        {
            _interactInput = value;
        }

        public void SetRollInput(bool value)
        {
            _rollInput = value;
        }

        public void SetUseConsumeableInput(bool value)
        {
            _useConsumeableInput = value;
        }

        public void SetUseTwoHandInput(bool value)
        {
            _useTwoHandInput = value;
        }

        public void SetJumpInput(bool value)
        {
            _jumpInput = value;
        }

        public void SetInventoryInput(bool value)
        {
            _inventoryInput = value;
        }

        public void SetDPadUpInput(bool value)
        {
            _dPadUpInput = value;
        }

        public void SetDPadDownInput(bool value)
        {
            _dPadDownInput = value;
        }

        public void SetDPadRightInput(bool value)
        {
            _dPadRightInput = value;
        }

        public void SetDPadLeftInput(bool value)
        {
            _dPadLeftInput = value;
        }
        #endregion
    }
}