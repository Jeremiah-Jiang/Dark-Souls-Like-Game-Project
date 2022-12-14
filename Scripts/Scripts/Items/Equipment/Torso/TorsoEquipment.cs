using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Items/Equipment/Torso Equipment")]
    public class TorsoEquipment : EquipmentItem
    {
        public string torsoModelName;
        public string upperLeftArmModelName;
        public string upperRightArmModelName;
        public string leftShoulderModelName;
        public string rightShoulderModelName;
    }
}

