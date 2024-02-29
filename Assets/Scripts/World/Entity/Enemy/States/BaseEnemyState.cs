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

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }
    }
}
