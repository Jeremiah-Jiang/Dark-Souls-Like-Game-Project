using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class WeaponModel : MonoBehaviour
    {
        Sheath sheath;

        private void Awake()
        {
            sheath = GetComponentInChildren<Sheath>(true);
        }

        public void SetSheathGameModelActive(bool value)
        {
            if(sheath != null)
            {
                GameObject sheathGameObject = sheath.gameObject;
                if (sheathGameObject != null)
                {
                    sheathGameObject.SetActive(value);
                }
                else
                {
                    Debug.Log("sheath game object is null");
                }
            }
            else
            {
                Debug.Log("sheath is NULL");
            }
        }
        //Just a reference script
    }
}

