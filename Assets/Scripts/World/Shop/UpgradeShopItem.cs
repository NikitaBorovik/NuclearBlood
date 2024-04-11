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
        private int? currentIndex = null;

        public void SetRandomUpgrade()
        {
            if (upgrades.Count <= 0)
                return;
            currentIndex = Random.Range(0, upgrades.Count);
            currentUpgrade = upgrades[currentIndex.Value];
            spriteRenderer.sprite = currentUpgrade.Image;
        }

        public void RemoveUpgradeAt(int index)
        {
            if (index < upgrades.Count)
            {
                if (currentIndex == index)
                {
                    currentUpgrade = null;
                    currentIndex = null;
                    spriteRenderer.sprite = null;
                }

                upgrades.RemoveAt(index);
                SetRandomUpgrade();
            }
            else
            {
                throw new System.InvalidOperationException("Cannot remove a non-existent upgrade.");
            }
        }
        
        protected override void Awake()
        {
            base.Awake();
        }

        void Start()
        {
            SetRandomUpgrade();
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if (currentUpgrade == null)
                return;
            merchantInfoField.GetComponentInChildren<TextMeshPro>().text = 
                $"Press E to buy for {currentUpgrade.Cost} bones :\n{currentUpgrade.Description}";

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
                    merchantInfoField.GetComponentInChildren<TextMeshPro>().text = currentUpgrade != null
                            ? $"Press E to buy for {currentUpgrade.Cost} bones :{currentUpgrade.Description}"
                            : "No upgrades left.";
                    timeFromBuy = 0;

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
            var upgradeManager = player.GetComponent<UpgradeManager>();

            var addedUpgrade = upgradeManager.FindUpgrade(upgrade);

            if (addedUpgrade == null)
            {
                upgradeManager.AddUpgrade(upgrade);
            }
            else if (addedUpgrade.IsComplete)
            {
                RemoveUpgradeAt(currentIndex.Value);
                return;
            }
            else
            {
                upgradeManager.LevelUpUpgrade(upgrade);
            }

            SetRandomUpgrade();
        }
    }
}

