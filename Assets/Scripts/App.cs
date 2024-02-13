using App.Systems.Input;
using App.World.Entity.Player.PlayerComponents;
using App.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class App : MonoBehaviour
    {
        [SerializeField]
        private InputSystem inputSystem;
        
        [SerializeField]
        private ObjectsContainer objectsContainer;
        [SerializeField]
        private Camera mainCamera;

        private void Start()
        {
            inputSystem.Init(mainCamera, objectsContainer.Player.GetComponent<Player>());
        }

    }
}
