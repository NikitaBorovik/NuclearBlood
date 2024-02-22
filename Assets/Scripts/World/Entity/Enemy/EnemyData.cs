using UnityEngine;

namespace App.World.Entity.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Enemies/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public float speed;
        public float attackRange;
    }
}
