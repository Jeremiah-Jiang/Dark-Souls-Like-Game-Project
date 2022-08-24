using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Items/Ammo")]
    public class RangedAmmoItem : Item
    {
        [Header("Ammo Type")]
        public AmmoType ammoType;

        [Header("Ammo Velocity")]
        public float forwardVelocity = 1000;
        public float upwardVelocity = 0;
        public float ammoMass = 0;
        public bool useGravity = false;

        [Header("Ammo Capacity")]
        public int carryLimit = 99;
        public int currentAmount = 99;

        [Header("Ammo Base Damage")]
        public int physicalDamage = 50;
        public int fireDamage = 0;

        [Header("Item Models")]
        public GameObject loadedItemModel; //The model is displayed while drawing the bow back
        public GameObject liveAmmoModel;// The live model that can damage characters
        public GameObject contactModel;//The model that instantiates into a collider on contact
    }
}

