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
        private Animator animator;
        private Health health;

        protected StateMachine stateMachine;

        protected BaseEnemyState attackState;
        private FollowState followState;

        [SerializeField] EnemyData enemyData;

        public EnemyData EnemyData => enemyData;
        public Transform Target => target;
        public Rigidbody2D RigidBody => rigidBody;
        public Animator Animator => animator;
        public Health Health => health;

        public BaseEnemyState AttackState => attackState;
        public FollowState FollowState => followState;

        public virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
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
