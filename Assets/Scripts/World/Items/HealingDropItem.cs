using App.World.Entity.Player.PlayerComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Items
{
    public class HealingDropItem : BaseDropItem
    {
        [SerializeField] private float healing;

        public override string PoolObjectType => "HealingDrop";

        public override void Init(Vector3 position)
        {
            base.Init(position);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log($"Player healed {healing}hp");
            //collision.gameObject.GetComponent<Player>().Health.Heal(healing);
            objectPool.ReturnToPool(this);
        }
    }
}