using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    public class ConsumableItem : Item
    {
        [Header("Item Quantity")]
        public int maxItemAmount;
        public int currentItemAmount;

        [Header("Item Model")]
        public GameObject itemModel;

        [Header("Animations")]
        public string consumeAnimation;
        public string failToConsumeAnimation;
        public bool isInteracting;

        [Header("Item Description")]
        public string itemDescription = "-";

        public virtual void AttemptToConsumeItem(PlayerManager player)
        {
            if (currentItemAmount > 0)
            {
                player.PlayTargetAnimation(consumeAnimation, isInteracting, true);
            }
            else
            {
                player.PlayTargetAnimation(failToConsumeAnimation, true);
            }
        }
    }
}

