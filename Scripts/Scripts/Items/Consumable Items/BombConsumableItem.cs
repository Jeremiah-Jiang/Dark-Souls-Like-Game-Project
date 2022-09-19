using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJ
{
    [CreateAssetMenu(menuName = "Items/Consumables/Bomb Item")]
    public class BombConsumableItem : ConsumableItem
    {
        [Header("Velocity")]
        public int upwardsVelocity = 300;
        public int forwardVelocity = 1000;
        public int bombMass = 1;

        [Header("Live Active Model")]
        public GameObject liveBombModel;

        [Header("Base Damage")]
        public int baseDamage = 50;
        public int explosiveDamage = 50;

        public override void AttemptToConsumeItem(PlayerManager player)
        {
            if(!player.isInteracting)
            {
                if (currentItemAmount > 0)
                {
                    player.GetRightHandSlot().UnloadWeapon();
                    player.PlayTargetAnimation(consumeAnimation, true);
                    GameObject bombModel = Instantiate(itemModel, player.GetRightHandSlot().transform.position, Quaternion.identity, player.GetRightHandSlot().transform);
                    player.SetInstantiatedFXModel(bombModel);
                }
                else
                {
                    player.PlayTargetAnimation("Frustrated", true);
                }
            }

        }
    }

}
