using App.World.Entity.Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Enemy
{
    public class MeleeEnemy : BaseEnemy
    {
        [SerializeField] private DamagePlayer attack;

        public GameObject Attack => attack.gameObject;

        public override void Awake()
        {
            base.Awake();
            attackState = new MeleeAttackState(this, stateMachine);
        }

        public override void Init(Transform target)
        {
            base.Init(target);
            attack.Init(EnemyData.damage);
        }
    }
}
