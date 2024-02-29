using App.Systems.EnemySpawning;
using App.World.Entity.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour, IWaveSystem
{
    private EnemySpawningSystem enemySpawningSystem;

    [SerializeField] private List<BaseEnemy> enemies;

    public void Init(EnemySpawningSystem enemySpawningSystem)
    {
        this.enemySpawningSystem = enemySpawningSystem;
        StartCoroutine(Wave());
    }

    private IEnumerator Wave()
    {
        enemySpawningSystem.SpawnEnemy(enemies[0].gameObject, 1);
        yield return new WaitForSeconds(3f);
    }

    public void ReportKilled(string enemyType)
    {
        //TODO: implement later
    }
}
