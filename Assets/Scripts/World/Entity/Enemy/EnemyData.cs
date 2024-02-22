using UnityEngine;

namespace App.World.Entity.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Enemies/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public float maxHealth;
        public float speed;
        public float attackRange;
        public float damage;
        public float timeBetweenAttacks;
        public float spawnAnimationDuration;
    }
}
