using UnityEngine;

namespace App.World
{
    public class ObjectsContainer : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private GameObject gates;

        public GameObject Player { get => player; }
        public GameObject Gates { get => gates;}
    }
}