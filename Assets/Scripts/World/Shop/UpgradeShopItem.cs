using System.Collections.Generic;
using App.Upgrades;
using App.World.Entity.Player.PlayerComponents;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.World.Shop
{
    public class UpgradeShopItem : BaseShopItem
    {

        [SerializeField]
        private List<BaseUpgradeScriptableObject<Player>> upgrades;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private BaseUpgradeScriptableObject<Player> currentUpgrade;

        public void SetRandomUpgrade()
        {
            if (upgrades.Count <= 0)
                return;
            currentUpgrade = upgrades[Random.Range(0, upgrades.Count)];
            spriteRenderer.sprite = currentUpgrade.Image;
        }
        
        protected override void Awake()
        {
            base.Awake();
            SetRandomUpgrade();
        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if (currentUpgrade == null)
                return;
            merchantInfoField.GetComponentInChildren<TextMeshPro>().text = $"Press E to buy for {currentUpgrade.Cost} bones :\n{currentUpgrade.Description}";

        }
        protected override void OnTriggerExit2D(Collider2D collision)
        {
            base.OnTriggerExit2D(collision);
            
        }
        public override void TryBuy(SellEvent ev) // on click && overlap
        {
            if (currentUpgrade == null)
                return;
            if (timeFromBuy >= minTimeFromBuy)
            {
                if (player.Money >= currentUpgrade.Cost)
                {
                    Buy();
                    timeFromBuy = 0;
                    merchantInfoField.GetComponentInChildren<TextMeshPro>().text = $"Press E to buy for {currentUpgrade.Cost} bones :{currentUpgrade.Description}";
                }
                else
                {
                    Debug.Log("Not enough money!");
                }
            }
        }

        public override void Buy()
        {
            base.Buy();
            player.Money -= currentUpgrade.Cost;
            var upgrade = Instantiate(currentUpgrade);
            player.GetComponent<UpgradeManager>().AddUpgrade(upgrade);
            SetRandomUpgrade();
        }
    }
}

