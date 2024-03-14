using App.Systems.Input;
using App.World.Entity.Player.PlayerComponents;
using App.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Systems.EnemySpawning;
using App.World.Entity;
using App.Systems.GameStates;
using App.World.Gates;

namespace App
{
    public class App : MonoBehaviour
    {
        [SerializeField]
        private InputSystem inputSystem;
        [SerializeField]
        private EnemySpawningSystem enemySpawningSystem;
        [SerializeField]
        private WaveSystem waveSystem;
        [SerializeField]
        private GameStatesSystem gameStatesSystem;
        [SerializeField]
        private ObjectsContainer objectsContainer;
        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private ObjectPool objectPool;

        private void Start()
        {
            inputSystem.Init(mainCamera, objectsContainer.Player.GetComponent<Player>());
            enemySpawningSystem.Init(waveSystem, objectPool, objectsContainer.Player.transform);
            waveSystem.Init(enemySpawningSystem, gameStatesSystem);
            gameStatesSystem.Init(waveSystem, objectsContainer.Gates.GetComponent<Gates>(), objectsContainer.GlobalLight);
        }

    }
}
