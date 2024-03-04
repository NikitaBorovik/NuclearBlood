using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

namespace App.World.Items
{
    public class MoneyDropItem : BaseDropItem
    {
        [SerializeField] private int price;

        public override string PoolObjectType => "MoneyDrop";

        public override void Init(Vector3 position)
        {
            base.Init(position);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log($"Player picked up {price}$");
            collision.gameObject.GetComponent<Player>().Money += price;
            objectPool.ReturnToPool(this);
        }
    }
}
