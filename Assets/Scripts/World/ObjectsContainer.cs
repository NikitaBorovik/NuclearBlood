using App.World.UI;
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
        [SerializeField]
        private Pauser pauser;
        [SerializeField]
        private GameObject shop;

        public GameObject Player { get => player; }
        public GameObject Gates { get => gates; }
        public GameObject Shop { get => shop; }
        public Light2D GlobalLight { get => globalLight; }
        public Pauser Pauser { get => pauser; }
    }
}