using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class UIEnemyHealthBar : MonoBehaviour
    {
        Slider slider;
        float timeUntilBarIsHidden = 0;
        public Camera mainCamera;
        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            mainCamera = Camera.main;
            if(slider == null)
            {
                Debug.LogError("Enemy slider is NULL");
            }
        }

        public void SetHealth(int health)
        {
            slider.value = health;
            timeUntilBarIsHidden = 3;
        }

        public void SetMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        private void Update()
        {
            timeUntilBarIsHidden = timeUntilBarIsHidden - Time.deltaTime;

            if (slider != null)
            {
                //transform.LookAt(Camera.main.transform);
                if (timeUntilBarIsHidden <= 0)
                {
                    timeUntilBarIsHidden = 0;
                    slider.gameObject.SetActive(false);
                }
                else
                {
                    if (!slider.gameObject.activeInHierarchy)
                    {
                        slider.gameObject.SetActive(true);
                    }
                }
                if (slider.value <= 0)
                {
                    Destroy(slider.gameObject);
                }
            }
        }
        private void LateUpdate()
        {
            
            if(slider != null)
            {
                transform.forward = Camera.main.transform.forward;
                //transform.LookAt(Camera.main.transform);
            }

        }
    }
}

