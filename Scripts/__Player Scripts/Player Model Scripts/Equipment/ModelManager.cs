using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ModelManager : MonoBehaviour
    {
        public List<GameObject> models;

        protected virtual void Awake()
        {
            GetAllModels();
        }

        public virtual void GetAllModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                models.Add(transform.GetChild(i).gameObject);
            }
        }

        public virtual void UnequipAllModels()
        {
            foreach (GameObject model in models)
            {
                model.SetActive(false);
            }
        }

        public virtual void EquipModelByName(string name)
        {
            if (name.Length == 0)
            {
                return;
            }
            int targetID = System.Int16.Parse(name.Substring(name.Length - 2)); //Int15 since the numbers are very small
            models[targetID].SetActive(true);
        }
    }
}

