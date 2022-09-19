using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class WeaponHolderSlot : MonoBehaviour
    {
        public Transform parentOverride;
        public WeaponItem currentWeapon;
        public bool isLeftHandSlot;
        public bool isRightHandSlot;
        public bool isBackSlot;
        public bool isShieldSlot;
        public bool isBowSlot;

        public GameObject currentWeaponModel;
        public GameObject sheathModel;

        public void UnloadWeapon()
        {
            if(currentWeaponModel != null)
            {
                WeaponModel weaponModel = currentWeaponModel.GetComponent<WeaponModel>();
                if(weaponModel != null)
                {
                    weaponModel.SetSheathGameModelActive(false);
                }
                currentWeaponModel.SetActive(false);
            }
        }

        public void UnloadWeaponAndDestroy()
        {
            if(currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }

        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            UnloadWeaponAndDestroy();
            if(weaponItem == null)
            {
                UnloadWeapon();
                return;
            }
            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;
            
            if(model != null)
            {
                if (parentOverride != null)
                {
                    model.transform.parent = parentOverride;
                }
                else
                {
                    model.transform.parent = transform;
                }
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }
            else
            {
                Debug.LogError("Weapon model is NULL");
            }
            currentWeaponModel = model;
        }

        public void LoadWeaponModelWithSheath(WeaponItem weaponItem)
        {
            UnloadWeaponAndDestroy();
            if (weaponItem == null)
            {
                UnloadWeapon();
                return;
            }
            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;
            WeaponModel weaponModel = model.GetComponent<WeaponModel>();
            if(weaponModel != null)
            {
                weaponModel.SetSheathGameModelActive(true);
            }
            if (model != null)
            {
                if (parentOverride != null)
                {
                    model.transform.parent = parentOverride;
                }
                else
                {
                    model.transform.parent = transform;
                }
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }
            else
            {
                Debug.LogError("Weapon model is NULL");
            }
            currentWeaponModel = model;
        }
    }
}

