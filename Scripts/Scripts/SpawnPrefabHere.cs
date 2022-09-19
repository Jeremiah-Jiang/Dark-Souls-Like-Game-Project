using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class SpawnPrefabHere : MonoBehaviour
    {
        public GameObject prefabToSpawn;
        CharacterCreationUIManager characterCreationUIManager;
        
        private void Awake()
        {
            characterCreationUIManager = FindObjectOfType<CharacterCreationUIManager>();
            
        }
        
        /*
        private void Start()
        {
            Instantiate(prefabToSpawn, transform);
            characterCreationUIManager.Initialize();

        }
        */
    }
}

