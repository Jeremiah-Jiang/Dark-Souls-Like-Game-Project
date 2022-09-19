using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        PlayerManager playerManager;

        [Header("Equipment Model Changers")]
        //Head Equipment
        HelmetModelManager helmetModelManager;
        HelmetAttachmentModelManager helmetAttachmentModelManager;
        HeadCoveringNoHairModelManager headCoveringNoHairModelManager;
        HeadCoveringNoFacialHairModelManager headCoveringNoFacialHairModelManager;

        //Torso Equipment
        TorsoModelManager torsoModelManager;
        UpperLeftArmModelManager upperLeftArmModelManager;
        UpperRightArmModelManager upperRightArmModelManager;
        RightShoulderModelManager rightShoulderModelManager;
        LeftShoulderModelManager leftShoulderModelManager;

        //Leg Equipment
        HipModelManager hipModelManager;
        LeftLegModelManager leftLegModelManager;
        RightLegModelManager rightLegModelManager;
        LeftKneeModelManager leftKneeModelManager;
        RightKneeModelManager rightKneeModelManager;

        //Hand Equipment
        LowerLeftArmModelManager lowerLeftArmModelManager;
        LowerRightArmModelManager lowerRightArmModelManager;
        LeftHandModelManager leftHandModelManager;
        RightHandModelManager rightHandModelManager;
        RightElbowModelManager rightElbowModelManager;
        LeftElbowModelManager leftElbowModelManager;

        //Back Attachments
        BackModelManager backModelManager;

        [Header("Default Naked Models")]
        public GameObject nakedHeadModel;
        public GameObject defaultHairModel;
        public GameObject defaultEyebrowModel;
        public GameObject defaultFacialHairModel;
        
        public string nakedTorsoModel;
        public string nakedHipModel;
        public string nakedLeftLegModel;
        public string nakedRightLegModel;
        public string nakedUpperLeftArmModel;
        public string nakedUpperRightArmModel;
        public string nakedLowerLeftArmModel;
        public string nakedLowerRightArmModel;
        public string nakedRightHand;
        public string nakedLeftHand;

        [Header("Default Character Look")]
        public string temp;


        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();

            helmetModelManager = GetComponentInChildren<HelmetModelManager>();
            helmetAttachmentModelManager = GetComponentInChildren<HelmetAttachmentModelManager>();
            headCoveringNoHairModelManager = GetComponentInChildren<HeadCoveringNoHairModelManager>();
            headCoveringNoFacialHairModelManager = GetComponentInChildren<HeadCoveringNoFacialHairModelManager>();

            torsoModelManager = GetComponentInChildren<TorsoModelManager>();
            upperRightArmModelManager = GetComponentInChildren<UpperRightArmModelManager>();
            upperLeftArmModelManager = GetComponentInChildren<UpperLeftArmModelManager>();
            leftShoulderModelManager = GetComponentInChildren<LeftShoulderModelManager>();
            rightShoulderModelManager = GetComponentInChildren<RightShoulderModelManager>();

            lowerRightArmModelManager = GetComponentInChildren<LowerRightArmModelManager>();
            lowerLeftArmModelManager = GetComponentInChildren<LowerLeftArmModelManager>();
            leftHandModelManager = GetComponentInChildren<LeftHandModelManager>();
            rightHandModelManager = GetComponentInChildren<RightHandModelManager>();
            rightElbowModelManager = GetComponentInChildren<RightElbowModelManager>();
            leftElbowModelManager = GetComponentInChildren<LeftElbowModelManager>();

            hipModelManager = GetComponentInChildren<HipModelManager>();
            leftLegModelManager = GetComponentInChildren<LeftLegModelManager>();
            rightLegModelManager = GetComponentInChildren<RightLegModelManager>();
            leftKneeModelManager = GetComponentInChildren<LeftKneeModelManager>();
            rightKneeModelManager = GetComponentInChildren<RightKneeModelManager>();

            backModelManager = GetComponentInChildren<BackModelManager>();

        }
        private void Start()
        {
            EquipAllEquipmentModels();
        }

        public void EquipAllEquipmentModels()
        {
            EquipHelmetEquipmentOnStart();
            EquipTorsoEquipmentOnStart();
            EquipLegEquipmentOnStart();
            EquipHandEquipmentOnStart();
            EquipBackEquipmentOnStart();
            playerManager.SetTotalPoise(playerManager.GetTotalPoiseFromEquipment());
        }
        private void EquipHelmetEquipmentOnStart()
        {
            helmetModelManager.UnequipAllModels();
            helmetAttachmentModelManager.UnequipAllModels();
            HelmetEquipment currentHelmetEquipment = playerManager.GetCurrentHelmetEquipment();

            if (currentHelmetEquipment != null)
            {
                string currentHelmetName = currentHelmetEquipment.helmetModelName;
                string currentHelmetModelAttachmentName = currentHelmetEquipment.helmetAttachmentModelName;
                HideSpecifiedHeadModelAspects(currentHelmetEquipment);
                helmetModelManager.EquipModelByName(currentHelmetName);
                helmetAttachmentModelManager.EquipModelByName(currentHelmetModelAttachmentName);
                playerManager.SetDamageNegationHead(currentHelmetEquipment.GetPhysicalDamageNegation(),
                    currentHelmetEquipment.GetMagicDamageNegation(),
                    currentHelmetEquipment.GetFireDamageNegation(), 
                    currentHelmetEquipment.GetLightningDamageNegation(), 
                    currentHelmetEquipment.GetUmbraDamageNegation(), 
                    currentHelmetEquipment.GetHolyDamageNegation(),
                    currentHelmetEquipment.GetPoise());
            }
            else
            {
                SetHeadModelActive(true);
                playerManager.SetDamageNegationHead(0, 0, 0, 0, 0 ,0, 0);
            }
        }

        private void EquipBackEquipmentOnStart()
        {
            backModelManager.UnequipAllModels();
            BackEquipment currentBackEquipment = playerManager.GetCurrentBackEquipment();
            if(currentBackEquipment != null)
            {
                string currentBackName = currentBackEquipment.backModelName;
                backModelManager.EquipModelByName(currentBackName);
                playerManager.SetDamageAbsorptionBack(currentBackEquipment.GetPhysicalDamageNegation(), 
                    currentBackEquipment.GetMagicDamageNegation(), 
                    currentBackEquipment.GetFireDamageNegation(), 
                    currentBackEquipment.GetLightningDamageNegation(), 
                    currentBackEquipment.GetUmbraDamageNegation(),
                    currentBackEquipment.GetHolyDamageNegation(),
                    currentBackEquipment.GetPoise());
            }
            else
            {
                backModelManager.EquipModelByName("");
                playerManager.SetDamageAbsorptionBack(0, 0, 0, 0, 0, 0, 0);
            }
        }

        private void EquipHandEquipmentOnStart()
        {
            lowerLeftArmModelManager.UnequipAllModels();
            lowerRightArmModelManager.UnequipAllModels();
            leftHandModelManager.UnequipAllModels();
            rightHandModelManager.UnequipAllModels();
            leftElbowModelManager.UnequipAllModels();
            rightElbowModelManager.UnequipAllModels();
            HandEquipment currentHandEquipment = playerManager.GetCurrentHandEquipment();
            if(currentHandEquipment != null)
            {
                string currentLowerLeftArmName = currentHandEquipment.lowerLeftArmModelName;
                string currentLowerRightArmName = currentHandEquipment.lowerRightArmModelName;
                string currentLeftHandName = currentHandEquipment.leftHandModelName;
                string currentRightHandName = currentHandEquipment.rightHandModelName;
                string currentLeftElbowPadName = currentHandEquipment.leftElbowPadName;
                string currentRightElbowPadName = currentHandEquipment.rightElbowPadName;
                lowerLeftArmModelManager.EquipModelByName(currentLowerLeftArmName);
                lowerRightArmModelManager.EquipModelByName(currentLowerRightArmName);
                leftHandModelManager.EquipModelByName(currentLeftHandName);
                rightHandModelManager.EquipModelByName(currentRightHandName);
                leftElbowModelManager.EquipModelByName(currentLeftElbowPadName);
                rightElbowModelManager.EquipModelByName(currentRightElbowPadName);
                playerManager.SetDamageAbsorptionHands(currentHandEquipment.GetPhysicalDamageNegation(),
                    currentHandEquipment.GetMagicDamageNegation(),
                    currentHandEquipment.GetFireDamageNegation(),
                    currentHandEquipment.GetLightningDamageNegation(),
                    currentHandEquipment.GetUmbraDamageNegation(),
                    currentHandEquipment.GetHolyDamageNegation(), 
                    currentHandEquipment.GetPoise());
            }
            else
            {
                lowerLeftArmModelManager.EquipModelByName(nakedLowerLeftArmModel);
                lowerRightArmModelManager.EquipModelByName(nakedLowerRightArmModel);
                leftHandModelManager.EquipModelByName(nakedLeftHand);
                rightHandModelManager.EquipModelByName(nakedRightHand);
                leftElbowModelManager.EquipModelByName("");
                rightElbowModelManager.EquipModelByName("");
                playerManager.SetDamageAbsorptionHands(0, 0, 0, 0, 0, 0, 0);
            }
        }

        private void EquipTorsoEquipmentOnStart()
        {
            torsoModelManager.UnequipAllModels();
            upperLeftArmModelManager.UnequipAllModels();
            upperRightArmModelManager.UnequipAllModels();
            leftShoulderModelManager.UnequipAllModels();
            rightShoulderModelManager.UnequipAllModels();
            TorsoEquipment currentTorsoEquipment = playerManager.GetCurrentTorsoEquipment();
            if (currentTorsoEquipment != null)
            {
                string currentTorsoName = currentTorsoEquipment.torsoModelName;
                string currentUpperLeftArmName = currentTorsoEquipment.upperLeftArmModelName;
                string currentUpperRightArmName = currentTorsoEquipment.upperRightArmModelName;
                string currentLeftShoulderName = currentTorsoEquipment.leftShoulderModelName;
                string currentRightShoulderName = currentTorsoEquipment.rightShoulderModelName;
                torsoModelManager.EquipModelByName(currentTorsoName);
                upperLeftArmModelManager.EquipModelByName(currentUpperLeftArmName);
                upperRightArmModelManager.EquipModelByName(currentUpperRightArmName);
                leftShoulderModelManager.EquipModelByName(currentLeftShoulderName);
                rightShoulderModelManager.EquipModelByName(currentRightShoulderName);
                playerManager.SetDamageAbsorptionBody(currentTorsoEquipment.GetPhysicalDamageNegation(),
                    currentTorsoEquipment.GetMagicDamageNegation(),
                    currentTorsoEquipment.GetFireDamageNegation(),
                    currentTorsoEquipment.GetLightningDamageNegation(),
                    currentTorsoEquipment.GetUmbraDamageNegation(),
                    currentTorsoEquipment.GetHolyDamageNegation(), 
                    currentTorsoEquipment.GetPoise());
            }
            else
            {
                torsoModelManager.EquipModelByName(nakedTorsoModel);
                upperLeftArmModelManager.EquipModelByName(nakedUpperLeftArmModel);
                upperRightArmModelManager.EquipModelByName(nakedUpperRightArmModel);
                leftShoulderModelManager.EquipModelByName("");
                rightShoulderModelManager.EquipModelByName("");
                playerManager.SetDamageAbsorptionBody(0, 0, 0, 0, 0, 0, 0);
            }
        }

        private void EquipLegEquipmentOnStart()
        {
            hipModelManager.UnequipAllModels();
            rightLegModelManager.UnequipAllModels();
            leftLegModelManager.UnequipAllModels();
            leftKneeModelManager.UnequipAllModels();
            rightKneeModelManager.UnequipAllModels();
            LegEquipment currentLegEquipment = playerManager.GetCurrentLegEquipment();
            if (currentLegEquipment != null)
            {
                string currentHipName = currentLegEquipment.hipModelName;
                string currentLeftLegName = currentLegEquipment.leftLegName;
                string currentRightLegName = currentLegEquipment.rightLegName;
                string currentLeftKneePadName = currentLegEquipment.leftKneePadName;
                string currentRightKneePadName = currentLegEquipment.rightKneePagName;
                hipModelManager.EquipModelByName(currentHipName);
                leftLegModelManager.EquipModelByName(currentLeftLegName);
                rightLegModelManager.EquipModelByName(currentRightLegName);
                leftKneeModelManager.EquipModelByName(currentLeftKneePadName);
                rightKneeModelManager.EquipModelByName(currentRightKneePadName);
                playerManager.SetDamageAbsorptionLegs(currentLegEquipment.GetPhysicalDamageNegation(),
                    currentLegEquipment.GetMagicDamageNegation(),
                    currentLegEquipment.GetFireDamageNegation(),
                    currentLegEquipment.GetLightningDamageNegation(),
                    currentLegEquipment.GetUmbraDamageNegation(),
                    currentLegEquipment.GetHolyDamageNegation(), 
                    currentLegEquipment.GetPoise());
            }
            else
            {
                hipModelManager.EquipModelByName(nakedHipModel);
                leftLegModelManager.EquipModelByName(nakedLeftLegModel);
                rightLegModelManager.EquipModelByName(nakedRightLegModel);
                leftKneeModelManager.EquipModelByName("");
                rightKneeModelManager.EquipModelByName("");
                playerManager.SetDamageAbsorptionLegs(0, 0, 0, 0, 0, 0, 0);
            }
        }


        public void SetHeadModelActive(bool value)
        {
            if(nakedHeadModel != null)
            {
                nakedHeadModel.SetActive(value);
            }
            if(defaultHairModel != null)
            {
                defaultHairModel.SetActive(value);
            }
            if(defaultEyebrowModel != null)
            {
                defaultEyebrowModel.SetActive(value);
            }
            if(defaultFacialHairModel != null)
            {
                defaultFacialHairModel.SetActive(value);
            }
        }

        public void HideSpecifiedHeadModelAspects(HelmetEquipment currentHelmetEquipment)
        {
            if(currentHelmetEquipment.hideFace)
            {
                SetHeadModelActive(false);
                return;
            }
            else
            {
                SetHeadModelActive(true);
            }
            if(currentHelmetEquipment.hideHair)
            {
                defaultHairModel.SetActive(false);
            }
            else
            {
                defaultHairModel.SetActive(true);
            }
            if (currentHelmetEquipment.hideFacialHair)
            {
                defaultFacialHairModel.SetActive(false);
            }
            else
            {
                defaultFacialHairModel.SetActive(true);
            }
            if (currentHelmetEquipment.hideEyebrow)
            {
                defaultEyebrowModel.SetActive(false);
            }
            else
            {
                defaultEyebrowModel.SetActive(true);
            }
        }

        public void SetNakedHeadModelPrefab(GameObject newNakedHead)
        {
            nakedHeadModel = newNakedHead;
        }

        public void SetDefaultHairModelPrefab(GameObject newHair)
        {
            defaultHairModel = newHair;
        }

        public void SetDefaultEyebrowModelPrefab(GameObject newEyebrow)
        {
            defaultEyebrowModel = newEyebrow;
        }

        public void SetDefaultFacialHairModelPrefab(GameObject newFacialHair)
        {
            defaultFacialHairModel = newFacialHair;
        }

        /*
        private void EquipHeadCoveringNoHairOnStart()
        {
            headCoveringNoHairModelManager.UnequipAllModels();
            HelmetEquipment currentHelmetEquipment = playerManager.GetCurrentHelmetEquipment();

            if(currentHelmetEquipment != null)
            {
                string currentHelmetName = currentHelmetEquipment.helmetModelName;
                string currentHelmetModelName = currentHelmetEquipment.helmetAttachmentModelName;
                headCoveringNoHairModelManager.EquipModelByName(currentHelmetName);
                playerManager.SetDamageAbsorptionHead(currentHelmetEquipment.physicalDefense, currentHelmetEquipment.fireDefense);
            }
            else
            {
                playerManager.SetDamageAbsorptionHead(0, 0);
            }

        }
        */
    }
}

