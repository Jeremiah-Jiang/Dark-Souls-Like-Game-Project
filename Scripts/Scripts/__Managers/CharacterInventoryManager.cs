using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class CharacterInventoryManager : MonoBehaviour
    {
        protected CharacterManager characterManager;
        [Header("Current Item Being Used")]
        public Item currentItemBeingUsed;

        [Header("Quickslots")]
        public SpellItem currentSpell;
        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;
        public ConsumableItem currentConsumable;
        public RangedAmmoItem currentAmmo;

        [Header("Current Equipment")]
        public HelmetEquipment currentHelmetEquipment;
        public TorsoEquipment currentTorsoEquipment;
        public LegEquipment currentLegEquipment;
        public HandEquipment currentHandEquipment;
        public BackEquipment currentBackEquipment;

        public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[2];
        public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[2];
        public ConsumableItem[] consumableItemsEquipped = new ConsumableItem[maxConsumableItemSlots];

        public int currRightWeaponIdx = 0;
        public int currLeftWeaponIdx = 0;
        public int currConsumableIdx = 0;
        public static int maxConsumableItemSlots = 2;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        private void Start()
        {
            characterManager.LoadBothWeaponsOnSlots();
        }
    }
}

