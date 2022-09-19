using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class DisableAllChildrenOfSelectedGameObject : MonoBehaviour
    {
        public GameObject parentGameObject;

        public void DisableAllChildren()
        {
            for(int i = 0; i < parentGameObject.transform.childCount; i++)
            {
                var child = parentGameObject.transform.GetChild(i);
                if(child != null)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
}

