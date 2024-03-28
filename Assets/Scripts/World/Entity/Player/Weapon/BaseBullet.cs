using UnityEngine;


namespace App.World.Entity.Player.Weapons
{
    public class BaseBullet : MonoBehaviour, IObjectPoolItem
    {
        protected float damage;
        protected float pearcingCount;
        protected float accuracy;
        private const float DefaultAccuracy = 0.5f;
        protected ObjectPool objectPool;
        [SerializeField]
        protected string poolObjectType;
        public string PoolObjectType => poolObjectType;

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (!gameObject.activeSelf)
                return;
            if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            {
                objectPool.ReturnToPool(this);
                return;
            }
            Health targetHealt = collision.GetComponent<Health>();
            if (targetHealt == null)
            {
                return;
            }
            targetHealt.TakeDamage(damage);
            if (pearcingCount > 0)
            {
                pearcingCount--;
            }
            else
            {
                objectPool.ReturnToPool(this);
            }

        }
        public virtual void Init(float damage, int pearcingCount, float accuracy)
        {
            this.damage = damage;
            this.pearcingCount = pearcingCount;
            this.accuracy = accuracy;
            GetComponent<TimeToLive>().Init();
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
            return (gameObject);
        }
    }
}
