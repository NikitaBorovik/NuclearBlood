using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Enemy.States
{
    public abstract class BaseEnemyState : IState
    {
        protected BaseEnemy baseEnemy;
        protected StateMachine stateMachine;

        public BaseEnemyState(BaseEnemy baseEnemy, StateMachine stateMachine)
        {
            this.baseEnemy = baseEnemy;
            this.stateMachine = stateMachine;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();
    }
}
