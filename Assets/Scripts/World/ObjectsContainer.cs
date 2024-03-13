using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace App.World
{
    public class ObjectsContainer : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private GameObject gates;
        [SerializeField]
        private Light2D globalLight;

        public GameObject Player { get => player; }
        public GameObject Gates { get => gates;}
        public Light2D GlobalLight { get => globalLight; }
    }
}