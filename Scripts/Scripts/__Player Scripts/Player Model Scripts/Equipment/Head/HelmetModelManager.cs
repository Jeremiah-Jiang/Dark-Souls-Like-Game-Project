using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class HelmetModelManager : ModelManager
    {
        protected override void Awake()
        {
            base.Awake();
            
        }
        //This is a reference script attached to the respective gameobject part of the Character Model,
        //all children objects under this gameObject will be retrieved as available equipment items
    }
}

