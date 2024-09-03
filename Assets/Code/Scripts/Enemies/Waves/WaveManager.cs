using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        
        [Header("UI Settings")]
        [SerializeField] private Button startWaveButton;

        private float _waveTimer;
        private float _spawnTimer;
        
        private bool _isWaveActive;
        
        private int _currentWaveIndex;
        
        public event Action<Wave> OnWaveEndEvent; 
        public event Action<float> OnEnemyDeathEvent; 
        public event Action OnGameEndEvent;
        
        private void OnEnable() => startWaveButton.onClick.AddListener(StartWave);
        private void OnDisable() => startWaveButton.onClick.RemoveListener(StartWave);

        private void Start() => _currentWave = waves[_currentWaveIndex];

        private void Update()
        {
            if (!_isWaveActive) return;   
            
            if (!AreTargetsAlive())
            {
                OnGameEndEvent?.Invoke();
                
                return;
            }

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
                OnWaveEndEvent?.Invoke(_currentWave);
                
                _isWaveActive = false;
                
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
                    startWaveButton.gameObject.SetActive(true);
                    
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
            OnEnemyDeathEvent?.Invoke(_currentWave.GetPointsPerEnemy());
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
        
        private void StartWave()
        {
            _waveTimer = 0;
            _spawnTimer = 0;
            
            _currentWaveIndex = Array.IndexOf(waves, _currentWave) + 1;
            
            if (_currentWaveIndex >= waves.Length)
            {
                OnGameEndEvent?.Invoke();
                
                return;
            }
            
            startWaveButton.gameObject.SetActive(false);
            
            _isWaveActive = true;
        }
    }
}