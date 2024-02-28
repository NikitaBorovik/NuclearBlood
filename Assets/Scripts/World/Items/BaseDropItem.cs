using App.World.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Items
{
    public abstract class BaseDropItem : MonoBehaviour, IObjectPoolItem
    {
        private Rigidbody2D rigidBody;
        protected ObjectPool objectPool;

        public abstract string PoolObjectType { get; }
        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        public virtual void Init(Vector3 position)
        {
            transform.position = position;
            Drop();
        }

        public void Drop()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            float xDirection = Random.Range(-1f, 1f);
            float yDirection = Random.Range(-1f, 1f);
            float speed = Random.Range(10f, 30f);
            Vector2 dropDirection = new Vector2(xDirection, yDirection).normalized;
            rigidBody.velocity = new Vector3(dropDirection.x * speed, dropDirection.y * speed, 0f);
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
