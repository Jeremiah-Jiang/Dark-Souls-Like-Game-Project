using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JJ
{
    public class ConsumableStatsMiddlePanelUI : MonoBehaviour
    {
        [Header("Capacity Information")]
        [SerializeField] private Text _currentConsumableItemsHeldText;
        [SerializeField] private Text _maxConsuableItemsCapacityText;

        [Header("Consumable Item Effect")]
        [SerializeField] private Text _itemEffectText;


        public void SetAllConsumableItemStatsTexts(ConsumableItem consumableItem, int currentCapacity = 0)
        {
            SetConsumableItemCapacityText(consumableItem, currentCapacity);
            SetConsumableItemEffectText(consumableItem);
            SetConsumableItemStatsWindowActive(true);
        }    
        private void SetConsumableItemCapacityText(ConsumableItem consumableItem, int currentCapacity = 0)
        {
            _currentConsumableItemsHeldText.text = consumableItem.currentItemAmount.ToString();
            _maxConsuableItemsCapacityText.text = consumableItem.maxItemAmount.ToString();
        }

        private void SetConsumableItemEffectText(ConsumableItem consumableItem)
        {
            _itemEffectText.text = consumableItem.itemDescription;
        }

        public void SetConsumableItemStatsWindowActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}

