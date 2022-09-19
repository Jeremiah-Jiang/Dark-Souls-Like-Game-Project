using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class PlayerModelManager : MonoBehaviour
    {
        GameObject defaultHeadPrefab;
        GameObject defaultTorsoPrefab;
        GameObject defaultUpperLeftArmPrefab;
        GameObject defaultUpperRightArmPrefab;
        GameObject defaultLowerLeftArmPrefab;
        GameObject defaultLowerRightArmPrefab;
        GameObject defaultLeftHandPrefab;
        GameObject defaultRightHandPrefab;
        GameObject defaultHipPrefab;
        GameObject defaultLowerLeftLegPrefab;
        GameObject defaultLowerRightLegPrefab;
        PlayerManager playerManager;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            SetHeadPrefabActive(true);
        }

        public void SetDefaultHeadPrefab(GameObject head)
        {
            defaultHeadPrefab = head;
        }

        public void SetHeadPrefabActive(bool value)
        {
            defaultHeadPrefab.SetActive(value);
        }
    }
}

