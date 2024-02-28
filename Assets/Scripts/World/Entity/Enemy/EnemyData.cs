using App.World.Items;
using UnityEngine;

namespace App.World.Entity.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Enemies/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public string type;
        public float maxHealth;
        public float speed;
        public float attackRange;
        public float damage;
        public float timeBetweenAttacks;
        public float spawnAnimationDuration;
        public float moneyDropChance;
        public float healingDropChance;
        public int minMoneyDrop;
        public int maxMoneyDrop;
        public HealingDropItem healingPrefab;
        public MoneyDropItem moneyPrefab;
    }
}
