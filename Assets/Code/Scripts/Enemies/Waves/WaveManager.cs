using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace epoHless
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Waves Settings")]
        [SerializeField] private Wave[] waves;
        
        private Wave _currentWave;
        
        [Header("Spawn Settings")]
        [SerializeField] private List<Tower> targets;
        [SerializeField] private Transform[] spawnPoints;

        private float _waveTimer;
        private float _spawnTimer;
        
        private void Start() => _currentWave = waves[0];

        private void Update()
        {
            if (!AreTargetsAlive()) return;

            if (IsCurrentWaveActive())
            {
                CalculateSpawn();
            }
        }

        private bool IsCurrentWaveActive()
        {
            _waveTimer += Time.deltaTime;

            if (_waveTimer >= _currentWave.GetWaveDuration())
            {
                return false;
            }
            
            return true;
        }

        private void CalculateSpawn()
        {
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer >= _currentWave.GetSpawnRate())
            {
                _spawnTimer = 0;
                
                if (targets.Count == 0)
                {
                    enabled = false;

                    return;
                }

                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            
            var enemies = _currentWave.GetEnemies();
            
            var enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], point.position, Quaternion.identity);
            
            Tower tower = null;

            while (tower == null)
            {
                tower = targets[Random.Range(0, targets.Count)];
            }
            
            enemy.GetHealthComponent().OnDeath += OnEnemyDeath;
            
            enemy.GetMovement().MoveTo(tower.GetTarget().position);
        }

        private void OnEnemyDeath()
        {
            
        }

        private bool AreTargetsAlive()
        {
            foreach (var target in targets)
            {
                if (target.GetHealthComponent().IsAlive())
                {
                    return true;
                }
            }

            return false;
        }
    }
}