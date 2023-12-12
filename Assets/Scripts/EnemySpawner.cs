using System;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private int _countOfEnemiesPerGround;
        [SerializeField] private List<GroundSpawnPoints> _grounds;
        private ObjectPooler<Enemy> _enemyPooler;

        private void Awake()
        {
            _enemyPooler = new ObjectPooler<Enemy>();
            _enemyPooler.Init(_enemyPrefab);
        }

        private void Start()
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            foreach (var ground in _grounds)
            {
                for (int i = 0; i < _countOfEnemiesPerGround; i++)
                {
                    var enemy = _enemyPooler.GetObject();
                    enemy.Init(ground.GetRandomPoint());
                }
            }
        }
    }
}