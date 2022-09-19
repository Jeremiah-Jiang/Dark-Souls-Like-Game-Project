using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class BonfirePopUpUI : MonoBehaviour
    {
        CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void DisplayBonfireLitTextPopUP()
        {
            StartCoroutine(FadeInPopUp());
        }

        IEnumerator FadeInPopUp()
        {
            for(float fade = 0.05f; fade < 1; fade += 0.05f)
            {
                _canvasGroup.alpha = fade;
                if(fade > 0.9f)
                {
                    StartCoroutine(FadeOutPopUp());
                }
                yield return new WaitForSeconds(0.05f);
            }
        }

        IEnumerator FadeOutPopUp()
        {
            yield return new WaitForSeconds(2);
            for (float fade = 1f; fade > 0; fade -= 0.05f)
            {
                _canvasGroup.alpha = fade;
                if (fade <= 0.05f)
                {
                    gameObject.SetActive(false);
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}

