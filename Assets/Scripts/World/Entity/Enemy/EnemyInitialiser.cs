using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Enemy
{
    public class EnemyInitialiser : MonoBehaviour
    {
        [SerializeField] private BaseEnemy enemy;
        [SerializeField] private Transform target;

        private void Start()
        {
            //enemy.Init(Vector3.zero, target);
        }
    }
}
