using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Items/Equipment/Hand Equipment")]
    public class HandEquipment : EquipmentItem
    {
        public string lowerRightArmModelName;
        public string rightHandModelName;
        public string lowerLeftArmModelName;
        public string leftHandModelName;
        public string leftElbowPadName;
        public string rightElbowPadName;
    }
}
