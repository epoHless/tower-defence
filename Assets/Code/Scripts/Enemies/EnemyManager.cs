using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace epoHless
{
    public class EnemyManager : MonoBehaviour
    {
        [Header("Enemy Settings")]
        [SerializeField] private Enemy enemyPrefab;
        
        [Header("Spawn Settings")]
        [SerializeField] private float spawnRate;
        [SerializeField] private List<Tower> targets;
        [SerializeField] private Transform[] spawnPoints;

        private float _spawnTimer;
        
        private void Update()
        {
            if (!AreTargetsAlive()) return;
            
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer >= spawnRate)
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
            
            var enemy = Instantiate(enemyPrefab, point.position, Quaternion.identity);
            
            Tower tower = null;

            while (tower == null)
            {
                tower = targets[Random.Range(0, targets.Count)];
            }
            
            enemy.GetMovement().MoveTo(tower.GetTarget().position);
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