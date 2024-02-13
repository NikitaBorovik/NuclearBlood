using App.World.Entity.Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Enemy
{
    public class BaseEnemy : MonoBehaviour
    {
        private Transform target;
        private Rigidbody2D rigidBody;
        protected StateMachine stateMachine;

        protected BaseEnemyState attackState;
        private FollowState followState;

        [SerializeField] EnemyData enemyData;

        public EnemyData EnemyData => enemyData;
        public Transform Target => target;
        public Rigidbody2D RigidBody => rigidBody;

        public BaseEnemyState AttackState => attackState;
        public FollowState FollowState => followState;

        public virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            stateMachine = new StateMachine();
            followState = new FollowState(this, stateMachine);
            stateMachine.Initialize(followState); //TODO: SpawningState
        }

        public virtual void Init(Transform target)
        {
            this.target = target;
        }
        
        void Update()
        {
            stateMachine.CurrentState.Update();
        }
    }
}
