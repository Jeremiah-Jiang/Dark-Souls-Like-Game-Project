using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [RequireComponent(typeof(InputManager))]
    //[RequireComponent(typeof(PlayerModelManager))]
    [RequireComponent(typeof(PlayerStatsManager))]
    [RequireComponent(typeof(PlayerCombatManager))]
    [RequireComponent(typeof(PlayerEffectsManager))]
    [RequireComponent(typeof(PlayerAnimatorManager))]
    [RequireComponent(typeof(PlayerEquipmentManager))]
    [RequireComponent(typeof(PlayerInventoryManager))]
    [RequireComponent(typeof(PlayerWeaponSlotManager))]
    [RequireComponent(typeof(PlayerLocomotionManager))]
    public class PlayerManager : CharacterManager
    {
        [Header("Input Manager")]
        private InputManager _inputManager;

        [Header("Camera")]
        public CameraHandler cameraHandler;

        [Header("Player UI")]
        public UIManager uiManager;

        [Header("Player")]
        private PlayerStatsManager _playerStatsManager;
        //private PlayerModelManager _playerModelManager;
        private PlayerCombatManager _playerCombatManager;
        private PlayerEffectsManager _playerEffectsManager;
        private PlayerAnimatorManager _playerAnimatorManager;
        private PlayerEquipmentManager _playerEquipmentManager;
        private PlayerInventoryManager _playerInventoryManager;
        private PlayerWeaponSlotManager _playerWeaponSlotManager;
        private PlayerLocomotionManager _playerLocomotionManager;

        public bool isResting;

        [Header("Interactable")]
        [SerializeField]
        private InteractableUI _interactableUI;
        [SerializeField]
        private InteractionPopUp _interactionPopUp;
        [SerializeField]
        private ItemPopUp _itemPopUp;

        private float _interactableSphereCastRadius = 0.4f;

        protected override void Awake()
        {
            base.Awake();
            uiManager = FindObjectOfType<UIManager>();
            _inputManager = GetComponent<InputManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            _interactableUI = uiManager.GetComponentInChildren<InteractableUI>();
            _interactionPopUp = _interactableUI.GetComponentInChildren<InteractionPopUp>(true);
            _itemPopUp = _interactableUI.GetComponentInChildren<ItemPopUp>(true);
            //_playerModelManager = GetComponent<PlayerModelManager>();
            _playerStatsManager = GetComponent<PlayerStatsManager>();
            _playerCombatManager = GetComponent<PlayerCombatManager>();
            backstabCollider = GetComponentInChildren<CriticalDamageCollider>();
            _playerEffectsManager = GetComponent<PlayerEffectsManager>();
            _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            _playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            _playerInventoryManager = GetComponent<PlayerInventoryManager>();
            _playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
            _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        }

        protected override void Update()
        {
            base.Update();
            isInteracting = _animator.GetBool("isInteracting");
            isHoldingArrow = _animator.GetBool("isHoldingArrow");
            isPerformingFullyChargedAttack = _animator.GetBool("isPerformingFullyChargedAttack");
            isUsingConsumable = _animator.GetBool("isUsingConsumable");
            canRotate = _animator.GetBool("canRotate");
            canDoCombo = _animator.GetBool("canDoCombo");
            _animator.SetBool("isInAir", isInAir);
            _animator.SetBool("isDead", isDead);
            _animator.SetBool("isBlocking", isBlocking);
            _animator.SetBool("isTwoHanding", isTwoHanding);
            isFiringSpell = _animator.GetBool("isFiringSpell");
            isInvulnerable = _animator.GetBool("isInvulnerable");
            _inputManager.TickInput(); //make sure this line is above playerLocomotion functions, we want to read input and act on input after it has been read

            _playerLocomotionManager.HandleRollingAndSprinting();
            _playerLocomotionManager.HandleJumping();
            _playerStatsManager.RegenerateStamina();

            if(cameraHandler != null)
            {
                CheckForInteractableObject();
            }

            if (isResting)
            {
                RegenerateHealthOverTime(Mathf.RoundToInt(GetMaxHealth() * 0.2f));
                RegenerateStaminaOverTime(GetMaxStamina() * 0.2f);
                RegenerateFocusPointsOverTime(GetMaxFocusPoints() * 0.2f);
            }

            //Might need to change this so we don't call it every frame
            uiManager.SetMoralAlignmentIcon();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (cameraHandler != null)
            {
                _playerLocomotionManager.HandleMovement();
                _playerLocomotionManager.HandleFalling(_playerLocomotionManager.moveDirection);
                _playerLocomotionManager.HandleRotation();
                _playerEffectsManager.HandleAllPoisonEffects();
                cameraHandler.HandleCameraRotation();
            }
        }

        private void LateUpdate()

        {
            _inputManager.SetDPadUpInput(false);
            _inputManager.SetDPadDownInput(false);
            _inputManager.SetDPadRightInput(false);
            _inputManager.SetDPadLeftInput(false);
            _inputManager.SetInteractInput(false);
            _inputManager.SetJumpInput(false);
            _inputManager.SetInventoryInput(false);
            if(cameraHandler != null)
            {
                cameraHandler.FollowTarget();
                cameraHandler.HandleCameraRotation();

            }

            if (isInAir)
            {
                _playerLocomotionManager.inAirTimer += Time.deltaTime;
            }
        }

        /// <summary>
        /// Function checks for an Interactable Object in front of this instance of PlayerManager
        /// </summary>
        /* Implementation Logic:
         * Send out a sphere cast in the forward direction, ignoring the [Default, Character, Environment, Player] layers
         * If the spherecast hits an object tagged with "Interactable":
         *     Assign the text field as the interactable object's [interactableText] field and set [this.interactableUIGameObject] as active
         *     If player enters interact input, interact with the interactable object
         *     
         * If the spherecast hits an object tagged with "Untagged" && [this._itemPopUp.gameObject] is active && player enters the interact input,
         *     Set this._itemPopUp.gameObject as inactive.
         *     This logic is here because the chest's tag of "Interactable" is replaced with "Untagged" after player interacts with it.
         *     We want to allow the player to disable the item pop up without turning away from the object.
         * Otherwise, keep [this.interactableUIGameObject] inactive
         * 
         * If spherecast does not hit anything and [this.interactableUIGameObject] is active, set it as inactive
         * If spherecast does not hit anything and [this._itemPopUp.gameObject] is active and player enters the interact input, set it as inactive
         */
        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            bool interactableDetected = Physics.SphereCast(transform.position, _interactableSphereCastRadius, transform.forward, out hit, 1f, cameraHandler.ignoreLayers);

            if (interactableDetected)
            {
                if(hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if(interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        _interactableUI.interactableText.text = interactableText;
                        _interactionPopUp.gameObject.SetActive(true);

                        if(_inputManager.GetInteractInput() == true)
                        {
                            interactableObject.Interact(this);
                        }
                    }
                }
                else if(hit.collider.tag == "Untagged" && _itemPopUp.gameObject != null && _inputManager.GetInteractInput() == true)
                {
                    _itemPopUp.gameObject.SetActive(false);
                }
                else
                {
                    _interactionPopUp.gameObject.SetActive(false);
                }
            }
            else
            {
                if(_interactionPopUp.gameObject != null)
                {
                    _interactionPopUp.gameObject.SetActive(false);
                }
                if(_itemPopUp.gameObject != null)
                {
                    _itemPopUp.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Function handles the interaction of opening a chest
        /// </summary>
        /// <param name="playerInteractionPosition">The transform to set the player at when they interact with the chest</param>
        /* Implementation Logic:
         * Set the player's rigidbody velocity to 0 to stop the player's movement
         * Set the player's transform position to the parameter [playerInteractionPosition]'s position
         * Play the Open Chest Animation (currently commented as the animation is not synced properly with the chest's opening animation
         */
        public void OpenChestInteraction(Transform playerInteractionPosition)
        {
            _playerLocomotionManager.rigidbody.velocity = Vector3.zero;//Stop player from skating if they interact while running
            transform.position = playerInteractionPosition.transform.position;
            //playerAnimatorManager.PlayTargetAnimation("Open Chest", true);
        }

        /// <summary>
        /// Function handles the interaction of walking through the fog wall. <br />
        /// </summary>
        /// <param name="fogWallEntrance">The transform of the entrance to the fog wall</param>
        /* Implementation Logic:
         * Set the player's rigidbody velocity to 0 to stop the player's movement
         * Rotate the player to look at the fogWallEntrance's front.
         * Essentially, the transform.forward of the fogwall in the game should be pointing in the direction you want the player to face.
         * If you want the player to face into the fogwall, the transform.forward should be the same direction as where the player is facing
         * Play the passing through fog animation
         */
        public void PassThroughFogWallInteraction(Transform fogWallEntrance)
        {
            _playerLocomotionManager.rigidbody.velocity = Vector3.zero;//Stop player from skating if they interact while running
            Vector3 rotationDirection = fogWallEntrance.transform.forward;
            Quaternion turnRotation = Quaternion.LookRotation(rotationDirection);
            transform.rotation = turnRotation;
            _playerAnimatorManager.PlayTargetAnimation("Pass Through Fog", true);
        }

    //Getter methods for PlayerManager fields
        #region Getter methods for PlayerManager fields
        public InteractionPopUp GetInteractionPopUp()
        {
            return _interactionPopUp;
        }

        public ItemPopUp GetItemPopUp()
        {
            return _itemPopUp;
        }

        public InteractableUI GetInteractableUI()
        {
            return _interactableUI;
        }
        #endregion

    //Methods for InputManager
        #region Methods for InputManager

        //Enable InputManager
        public void EnableInputManager(bool value)
        {
            _inputManager.enabled = value;
        }
        //Getter methods for InputManager Transform Fields
        public Transform GetCriticalAttackRayCastStartPoint()
        {
            return _inputManager.GetCriticalAttackRayCastStartPoint();
        }
        //Getter methods for Movement Values
        #region Getter methods for Movement Values
        public float GetMoveAmount()
        {
            return _inputManager.GetMoveAmount();
        }

        public float GetHorizontalMovementValue()
        {
            return _inputManager.GetHorizontalMovementValue();
        }

        public float GetVerticalMovementValue()
        {
            return _inputManager.GetVerticalMovementValue();
        }

        public float GetMouseXValue()
        {
            return _inputManager.GetMouseXValue();
        }

        public float GetMouseYValue()
        {
            return _inputManager.GetMouseYValue();
        }
        #endregion

        //Setter methods for Movement Values
        #region Setter methods for Movement Values
        public void SetMoveAmount(float value)
        {
            _inputManager.SetMoveAmount(value);
        }

        public void SetHorizontalMovementValue(float value)
        {
            _inputManager.SetHorizontalMovementValue(value);
        }

        public void SetVerticalMovementValue(float value)
        {
            _inputManager.SetVerticalMovementValue(value);
        }
        #endregion

        //Getter methods for Input Action Fields
        #region Getter methods for Input Action Fields
        public bool GetInteractInput()
        {
            return _inputManager.GetInteractInput();
        }

        public bool GetRollInput()
        {
            return _inputManager.GetRollInput();
        }

        public bool GetUseConsumableInput()
        {
            return _inputManager.GetUseConsumableInput();
        }

        public bool GetUseTwoHandInput()
        {
            return _inputManager.GetUseTwoHandInput();
        }

        public bool GetJumpInput()
        {
            return _inputManager.GetJumpInput();
        }

        public bool GetInventoryInput()
        {
            return _inputManager.GetInventoryInput();
        }

        public bool GetDPadUpInput()
        {
            return _inputManager.GetDPadUpInput();
        }

        public bool GetDPadDownInput()
        {
            return _inputManager.GetDPadDownInput();
        }

        public bool GetDPadLeftInput()
        {
            return _inputManager.GetDPadLeftInput();
        }

        public bool GetDPadRightInput()
        {
            return _inputManager.GetDPadRightInput();
        }

        public bool GetLockOnInput()
        {
            return _inputManager.GetLockOnInput();
        }
        #endregion

        //Setter methods for Input Action Fields
        #region Setter methods for Input Action Fields
        public void SetInteractInput(bool value)
        {
            _inputManager.SetInteractInput(value);
        }

        public void SetRollInput(bool value)
        {
            _inputManager.SetRollInput(value);
        }

        public void SetUseConsumeableInput(bool value)
        {
            _inputManager.SetUseConsumeableInput(value);
        }

        public void SetUseTwoHandInput(bool value)
        {
            _inputManager.SetUseTwoHandInput(value);
        }

        public void SetJumpInput(bool value)
        {
            _inputManager.SetJumpInput(value);
        }

        public void SetInventoryInput(bool value)
        {
            _inputManager.SetInventoryInput(value);
        }

        public void SetDPadUpInput(bool value)
        {
            _inputManager.SetDPadUpInput(value);
        }

        public void SetDPadDownInput(bool value)
        {
            _inputManager.SetDPadDownInput(value);
        }

        public void SetDPadRightInput(bool value)
        {
            _inputManager.SetDPadRightInput(value);
        }

        public void SetDPadLeftInput(bool value)
        {
            _inputManager.SetDPadLeftInput(value);
        }
        #endregion

        //Getter methods for Player Flags
        #region Getter methods for Player Flags
        public bool GetRollFlag()
        {
            return _inputManager.GetRollFlag();
        }

        public bool GetSprintFlag()
        {
            return _inputManager.GetSprintFlag();
        }

        public bool GetComboFlag()
        {
            return _inputManager.GetComboFlag();
        }

        public bool GetLockOnFlag()
        {
            return _inputManager.GetLockOnFlag();
        }

        public bool GetInventoryFlag()
        {
            return _inputManager.GetInventoryFlag();
        }

        public bool GetTwoHandFlag()
        {
            return _inputManager.GetTwoHandFlag();
        }
        #endregion

        //Setter methods for Player Flags
        #region Setter methods for Player Flags
        public void SetRollFlag(bool value)
        {
            _inputManager.SetRollFlag(value);
        }

        public void SetSprintFlag(bool value)
        {
            _inputManager.SetSprintFlag(value);
        }

        public void SetComboFlag(bool value)
        {
            _inputManager.SetComboFlag(value);
        }

        public void SetLockOnFlag(bool value)
        {
            _inputManager.SetLockOnFlag(value);
        }

        public void SetInventoryFlag(bool value)
        {
            _inputManager.SetInventoryFlag(value);
        }

        public void SetTwoHandFlag(bool value)
        {
            _inputManager.SetTwoHandFlag(value);
        }
        #endregion

        #endregion

        /*
        #region Methods for PlayerModelManager
        public void SetDefaultHeadActive(bool value)
        {
            _playerModelManager.SetHeadPrefabActive(value);
        }

        public void SetDefaultHeadPrefab(GameObject head)
        {
            _playerModelManager.SetDefaultHeadPrefab(head);
        }
        #endregion
        */

    //Methods for PlayerStatsManager
        #region Methods for PlayerStatsManager
        public PlayerStatsManager GetPlayerStatsManager()
        {
            return _playerStatsManager;
        }

        public void SetIsDark()
        {
            _playerStatsManager.isDark = true;
            _playerStatsManager.isHoly = false;
            _playerStatsManager.isNeutral = false;
        }

        public void SetIsHoly()
        {
            _playerStatsManager.isDark = false;
            _playerStatsManager.isHoly = true;
            _playerStatsManager.isNeutral = false;
        }

        public void SetIsNeutral()
        {
            _playerStatsManager.isDark = false;
            _playerStatsManager.isHoly = false;
            _playerStatsManager.isNeutral = true;
        }

        public void HealPlayer(int healAmount)
        {
            _playerStatsManager.HealPlayer(healAmount);
        }

        public void RegenerateHealthOverTime(int healthRegenRate)
        {
            _playerStatsManager.RegenerateHealthOverTime(healthRegenRate);
        }

        /// <summary>
        /// Function to deduct Stamina and update Stamina Bar on UI
        /// </summary>
        /// <param name="staminaToDeduct"></param>
        public override void DeductStamina(float staminaToDeduct)
        {
            _playerStatsManager.DeductStamina(staminaToDeduct);
            uiManager.SetCurrentStaminaOnSlider(GetCurrentStamina());
        }

        public void DeductStaminaNoDamageTaken(float staminaToDeduct)
        {
            _playerStatsManager.DeductStamina(staminaToDeduct);
            uiManager.SetCurrentStaminaOnSliderNoDamageTaken(GetCurrentStamina());
        }

        public void RegenerateStaminaOverTime(float staminaRegenRate)
        {
            _playerStatsManager.RegenerateStaminaOverTime(staminaRegenRate);
        }

        /// <summary>
        /// Function to deduct Focus Points and update Focus Points Bar on UI
        /// </summary>
        /// <param name="focusPointsToDeduct"></param>
        public override void DeductFocusPoints(float focusPointsToDeduct)
        {
            _playerStatsManager.DeductFocusPoints(focusPointsToDeduct);
            uiManager.SetCurrentFocusPointsOnSlider(GetCurrentFocusPoints());
        }

        public void RegenerateFocusPointsOverTime(float focusPointsRegenRate)
        {
            _playerStatsManager.RegenerateFocusPointsOverTime(focusPointsRegenRate);
        }
        public void DeductSouls(int soulsToDeduct)
        {
            _playerStatsManager.DeductSouls(soulsToDeduct);
        }

        public override void SetHealthLevelAndMaxHealth(int healthLevel)
        {
            _playerStatsManager.SetHealthLevelAndMaxHealth(healthLevel);
        }

        public override void SetStaminaLevelAndMaxStamina(int staminaLevel)
        {
            _playerStatsManager.SetStaminaLevelAndMaxStamina(staminaLevel);
        }

        public override void SetFocusLevelAndMaxFocusPoints(int focusLevel)
        {
            _playerStatsManager.SetFocusLevelAndMaxFocusPoints(focusLevel);
        }


        #endregion

    //Methods for PlayerCombatManager
        #region Methods for PlayerCombatManager
        public PlayerCombatManager GetPlayerCombatManager()
        {
            return _playerCombatManager;
        }

        public void AttemptBackstabOrRiposte()
        {
            _playerCombatManager.AttemptBackstabOrRiposte();
        }
        #endregion

    //Methods for PlayerEffectsManager
        #region Methods for PlayerEffectsManager

        public PlayerEffectsManager GetPlayerEffectsManager()
        {
            return _playerEffectsManager;
        }
        /// <summary>
        /// Getter method to retrieve the GameObject of _playerEffectsManager.instantiatedFXModel
        /// </summary>
        /// <returns></returns>
        public GameObject GetInstantiatedFXModel()
        {
            return _playerEffectsManager.instantiatedFXModel;
        }

        /// <summary>
        /// Setter method to set _playerEffectsManager.currentParticleFX as particleFX
        /// </summary>
        /// <param name="particleFX"></param>
        public void SetCurrentParticleFX(GameObject particleFX)
        {
            _playerEffectsManager.currentParticleFX = particleFX;
        }

        /// <summary>
        /// Setter method to set _playerEffectsManager.amountToHeal as healAmount
        /// </summary>
        /// <param name="healAmount"></param>
        public void SetAmountToHeal(int healAmount)
        {
            _playerEffectsManager.amountToHeal = healAmount;
        }

        /// <summary>
        /// Setter method to set _playerEffectsManager.instantiatedFXModel as fxModel
        /// </summary>
        /// <param name="fxModel"></param>
        public void SetInstantiatedFXModel(GameObject fxModel)
        {
            _playerEffectsManager.instantiatedFXModel = fxModel;
        }

        public void ApplyConsumableEffectToPlayer() //This is an animation event
        {
            _playerEffectsManager.ApplyConsumableEffect();
        }
        #endregion

    //Methods for PlayerAnimatorManager
        #region Methods for PlayerAnimatorManager

        /// <summary>
        /// Method to invoke _playerAnimatorManager.UpdateAnimatorValues(...)
        /// </summary>
        /// <param name="verticalMovement"></param>
        /// <param name="horizontalMovement"></param>
        /// <param name="isSprinting"></param>
        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            _playerAnimatorManager.UpdateAnimatorValues(verticalMovement, horizontalMovement, isSprinting); 
        }
        #endregion

    //Methods for PlayerEquipmentManager
        #region Methods for PlayerEquipmentManager
        public void EquipAllEquipmentModels()
        {
            _playerEquipmentManager.EquipAllEquipmentModels();
        }

        public void SetNakedHeadModelPrefab(GameObject newNakedHead)
        {
            _playerEquipmentManager.SetNakedHeadModelPrefab(newNakedHead);
        }

        public void SetDefaultHairModelPrefab(GameObject newHair)
        {
            _playerEquipmentManager.SetDefaultHairModelPrefab(newHair);
        }

        public void SetDefaultEyebrowModelPrefab(GameObject newEyebrows)
        {
            _playerEquipmentManager.SetDefaultEyebrowModelPrefab(newEyebrows);
        }

        public void SetDefaultFacialHairModelPrefab(GameObject newFacialHair)
        {
            _playerEquipmentManager.SetDefaultFacialHairModelPrefab(newFacialHair);
        }
        #endregion

    //Methods for PlayerInventoryManager
        #region Methods for PlayerInventoryManger
        /// <summary>
        /// Getter method to retrieve the instance of _playerInventoryManager that is attached to this PlayerManager
        /// </summary>
        /// <returns></returns>
        public PlayerInventoryManager GetPlayerInventoryManager()
        {
            return _playerInventoryManager;
        }

        /// <summary>
        /// Method to invoke _playerInventoryManager.ChangeRightWeapon()
        /// </summary>
        public void ChangeRightWeapon()
        {
            _playerInventoryManager.ChangeRightWeapon();
            if (GetRightWeapon().weaponEffect == WeaponEffect.Darkness && GetLeftWeapon().weaponEffect == WeaponEffect.Darkness)
            {
                Debug.Log("This is where the temporary logic for setting Darkness Moral Alignment Icon is");
                _playerStatsManager.isDark = true;
                _playerStatsManager.isNeutral = false;
                _playerStatsManager.isHoly = false;
            }
            else if (GetRightWeapon().weaponEffect == WeaponEffect.Holy && GetLeftWeapon().weaponEffect == WeaponEffect.Holy)
            {
                Debug.Log("This is where the temporary logic for setting Holy Moral Alignment Icon is");
                _playerStatsManager.isHoly = true;
                _playerStatsManager.isDark = false;
                _playerStatsManager.isNeutral = false;
            }
            else
            {
                Debug.Log("This is where the temporary logic for setting Neutral Moral Alignment Icon is");
                _playerStatsManager.isNeutral = true;
                _playerStatsManager.isDark = false;
                _playerStatsManager.isHoly = false;
            }
        }

        /// <summary>
        /// Method to invoke _playerInventoryManager.ChangeLeftWeapon()
        /// </summary>
        public void ChangeLeftWeapon()
        {
            _playerInventoryManager.ChangeLeftWeapon();
            if (GetRightWeapon().weaponEffect == WeaponEffect.Darkness && GetLeftWeapon().weaponEffect == WeaponEffect.Darkness)
            {
                Debug.Log("This is where the temporary logic for setting Darkness Moral Alignment Icon is");
                _playerStatsManager.isDark = true;
                _playerStatsManager.isNeutral = false;
                _playerStatsManager.isHoly = false;
            }
            else if (GetRightWeapon().weaponEffect == WeaponEffect.Holy && GetLeftWeapon().weaponEffect == WeaponEffect.Holy)
            {
                Debug.Log("This is where the temporary logic for setting Holy Moral Alignment Icon is");
                _playerStatsManager.isHoly = true;
                _playerStatsManager.isDark = false;
                _playerStatsManager.isNeutral = false;
            }
            else
            {
                Debug.Log("This is where the temporary logic for setting Neutral Moral Alignment Icon is");
                _playerStatsManager.isNeutral = true;
                _playerStatsManager.isDark = false;
                _playerStatsManager.isHoly = false;
            }
        }

        /// <summary>
        /// Method to invoke _playerInventoryManager.ChangeConsumableItem()
        /// </summary>
        public void ChnageConsumableItem()
        {
            _playerInventoryManager.ChangeConsumableItem();
        }

        /// <summary>
        /// Getter method to retrieve the list of weapons in player Inventory
        /// </summary>
        /// <returns>_playerInventory.weaponsInventory</returns>
        public List<WeaponItem> GetWeaponsInventory()
        {
            return _playerInventoryManager.weaponsInventory;
        }

        /// <summary>
        /// Getter method to retrieve the list of consumable in player Inventory
        /// </summary>
        /// <returns>_playerInventoryManager.consumablesInventory</returns>
        public List<ConsumableItem> GetConsumablesInventory()
        {
            return _playerInventoryManager.consumablesInventory;
        }

        /// <summary>
        /// Getter method to retrieve the list of head equipment in player Inventory
        /// </summary>
        /// <returns>_playerInventory.headEquipmentInventory</returns>
        public List<HelmetEquipment> GetHeadEquipmentInventory()
        {
            return _playerInventoryManager.headEquipmentInventory;
        }

        /// <summary>
        /// Getter method to retrieve the list of torso equipment in player Inventory
        /// </summary>
        /// <returns>_playerInventory.torsoEquipmentInventory</returns>
        public List<TorsoEquipment> GetTorsoEquipmentInventory()
        {
            return _playerInventoryManager.torsoEquipmentInventory;
        }

        /// <summary>
        /// Getter method to retrieve the list of back equipment in player Inventory
        /// </summary>
        /// <returns>_playerInventory.backEquipmentInventory</returns>
        public List<BackEquipment> GetBackEquipmentInventory()
        {
            return _playerInventoryManager.backEquipmentInventory;
        }

        /// <summary>
        /// Getter method to retrieve the list of hand equipment in player Inventory
        /// </summary>
        /// <returns>_playerInventory.handEquipmentInventory</returns>
        public List<HandEquipment> GetHandEquipmentInventory()
        {
            return _playerInventoryManager.handEquipmentInventory;
        }

        /// <summary>
        /// Getter method to retrieve the list of leg equipment in player Inventory
        /// </summary>
        /// <returns>_playerInventory.legEquipmentInventory</returns>
        public List<LegEquipment> GetLegEquipmentInventory()
        {
            return _playerInventoryManager.legEquipmentInventory;
        }
        #endregion

    //Methods for PlayerWeaponSlotManager
        #region Methods for PlayerWeaponSlotManager

        /// <summary>
        /// Method to invoke _playerWeaponSlotManager.LoadWeaponOnSlot(..)
        /// </summary>
        /// <param name="weaponItem"></param>
        /// <param name="isLeft"></param>
        public override void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            _playerWeaponSlotManager.LoadWeaponOnSlot(weaponItem, isLeft);
        }
        #endregion

    //Methods for PlayerLocomotionManager
        #region Methods for PlayerLocomotionManager

        /// <summary>
        /// Returns the instance of PlayerLocomotionManager attached to this PlayerManager
        /// </summary>
        /// <returns></returns>
        public PlayerLocomotionManager GetPlayerLocomotionManager()
        {
            return _playerLocomotionManager;
        }
        #endregion
    }
}

