using App.World.Entity.Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Enemy
{
    public class BaseEnemy : MonoBehaviour, IKillable
    {
        private Transform target;
        private Rigidbody2D rigidBody;
        private Animator animator;
        private Health health;

        protected StateMachine stateMachine;

        protected BaseEnemyState attackState;
        private FollowState followState;
        private SpawningState spawningState;
        private DieState dieState;

        [SerializeField] EnemyData enemyData;
        [SerializeField] protected List<Collider2D> myColliders;

        public EnemyData EnemyData => enemyData;
        public List<Collider2D> MyColliders => myColliders;
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
            spawningState = new SpawningState(this, stateMachine);
            dieState = new DieState(this, stateMachine);
            followState = new FollowState(this, stateMachine);
            stateMachine.Initialize(spawningState);
        }

        public virtual void Init(Transform target)
        {
            this.target = target;
            health.MaxHealth = enemyData.maxHealth;
            health.HealToMax();
        }
        
        void Update()
        {
            stateMachine.CurrentState.Update();
        }

        public void Die()
        {
            if (stateMachine.CurrentState != dieState)
            {
                StopAllCoroutines();
                stateMachine.ChangeState(dieState);
                //DropExperience();
                //DropHealing();
                //OnDied?.CallDieEvent();
            }
        }

        public void DyingSequence()
        {
            //waveSystem.ReportKilled(EnemyData.type);
            //objectPool.ReturnToPool(this);
        }
    }
}
