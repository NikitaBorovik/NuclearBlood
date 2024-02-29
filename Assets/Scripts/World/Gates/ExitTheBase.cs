using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Items.Gates
{
    public class ExitTheBase : MonoBehaviour
    {
        
        public void Init()
        {
            
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Exit();
        }
        private void Exit()
        {
            gameObject.SetActive(false);
            
        }
    }
}

