﻿using UnityEngine;
using App.World.Entity.Player.PlayerComponents;

namespace App.Upgrades.ConcreteUpgrades.StandardStrategy.PlayerUpgrades
{
    [System.Serializable]
    public struct HealingDropChanceUpgradeLevel
    {
        [Range(0f, 1f)] public float additionalDropChance;
    }

    [CreateAssetMenu(fileName = "HealingChanceUpgrade", menuName = "Scriptable Objects/Upgrades/HealingChanceUpgrade")]
    public class HealingChanceUpgrade : StandardUpgradeScriptableObject<Player, HealingDropChanceUpgradeLevel>
    {
        public HealingChanceUpgrade() : base(new PlayerHealingDropChanceUpgradeStrategy()) { }
    }

    public class PlayerHealingDropChanceUpgradeStrategy : IStrategy<Player, HealingDropChanceUpgradeLevel>
    {
        private float? initialDropChance = null;

        public void Initialize(Player player)
        {
            initialDropChance = DropChanceManager.AdditionalHealingDropChance;
        }

        public void Destroy()
        {
            initialDropChance = null;
        }

        public void Reset(Player player)
        {
            DropChanceManager.AdditionalHealingDropChance = initialDropChance ?? 0f;
        }

        public void SwitchToLevel(Player player, HealingDropChanceUpgradeLevel level)
        {
            Reset(player);
            DropChanceManager.AdditionalHealingDropChance += level.additionalDropChance;
        }
    }
}
