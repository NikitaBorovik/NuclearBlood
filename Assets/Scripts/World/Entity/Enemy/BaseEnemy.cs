using App.World.Entity.Enemy.States;
using App.World.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Enemy
{
    public class BaseEnemy : MonoBehaviour, IKillable, IObjectPoolItem
    {
        private Transform target;
        private Rigidbody2D rigidBody;
        private Animator animator;
        private Health health;
        private AudioSource audioSource;
        private IWaveSystem waveSystem;

        protected ObjectPool objectPool;
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

        public string PoolObjectType => enemyData.type;

        public AudioSource AudioSource => audioSource;

        public virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();

            stateMachine = new StateMachine();
            spawningState = new SpawningState(this, stateMachine);
            dieState = new DieState(this, stateMachine);
            followState = new FollowState(this, stateMachine);
            
        }

        public virtual void Init(Vector3 position, Transform target, IWaveSystem waveSystem, float hpMultiplier)
        {
            this.waveSystem = waveSystem;
            this.target = target;
            transform.position = position;
            health.MaxHealth = enemyData.maxHealth;
            health.HealToMax();
            if(stateMachine.CurrentState == null)
                stateMachine.Initialize(spawningState);
            else
                stateMachine.ChangeState(spawningState);

            if (enemyData.gruntSounds.Count > 0)
                StartCoroutine(Grunt());
        }

        private IEnumerator Grunt()
        {
            float time = Random.Range(0, enemyData.maxTimeBetweenGrunts);
            yield return new WaitForSeconds(time);
            int index = Random.Range(0, enemyData.gruntSounds.Count);
            audioSource.PlayOneShot(enemyData.gruntSounds[index]);
            while (true)
            {
                time = Random.Range(enemyData.minTimeBetweenGrunts, enemyData.maxTimeBetweenGrunts);
                yield return new WaitForSeconds(time);
                index = Random.Range(0, enemyData.gruntSounds.Count);
                audioSource.PlayOneShot(enemyData.gruntSounds[index]);
            }

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
                DropMoney();
                DropHealing();
                //OnDied?.CallDieEvent();
            }
        }

        private void DropMoney()
        {
            if (Random.value <= enemyData.moneyDropChance)
            {
                int count = Random.Range(enemyData.minMoneyDrop, enemyData.maxMoneyDrop + 1);
                for (int i = 0; i < count; i++)
                {
                    GameObject money = objectPool.GetObjectFromPool(enemyData.moneyPrefab.PoolObjectType, enemyData.moneyPrefab.gameObject, transform.position).GetGameObject();
                    money.GetComponent<MoneyDropItem>().Init(transform.position);
                }
            }
        }
        private void DropHealing()
        {
            if (DropChanceManager.ShouldDropHealing(enemyData.healingDropChance))
            {
                GameObject healing = objectPool.GetObjectFromPool(enemyData.healingPrefab.PoolObjectType, enemyData.healingPrefab.gameObject, transform.position).GetGameObject();
                healing.GetComponent<HealingDropItem>().Init(transform.position);
            }
        }

        public void DyingSequence()
        {
            waveSystem.ReportKilled(EnemyData.type);
            objectPool.ReturnToPool(this);
        }

        public void GetFromPool(ObjectPool pool)
        {
            objectPool = pool;
            gameObject.SetActive(true);
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
