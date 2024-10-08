using UnityEngine;

namespace epoHless
{
    [System.Serializable]
    public struct Wave
    {
        [SerializeField] private Enemy[] enemies;
        
        [SerializeField] private float spawnRate;
        [SerializeField] private float waveDuration;
        [SerializeField] private float pointsPerEnemy;
        [SerializeField] private float energyAfterWave;
        
        public Enemy[] GetEnemies() => enemies;
        public float GetSpawnRate() => spawnRate;
        public float GetWaveDuration() => waveDuration;
        public float GetPointsPerEnemy() => pointsPerEnemy;
        public float GetEnergyAfterWave() => energyAfterWave;
    }
}