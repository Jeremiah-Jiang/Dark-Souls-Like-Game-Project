using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Items/Equipment/Helmet Equipment")]
    public class HelmetEquipment : EquipmentItem
    {
        public string helmetModelName;
        public string helmetAttachmentModelName;
        public bool hideFace;
        public bool hideHair;
        public bool hideFacialHair;
        public bool hideEyebrow;
    }
}


