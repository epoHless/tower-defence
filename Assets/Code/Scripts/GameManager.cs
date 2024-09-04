using UnityEngine;
using UnityEngine.Serialization;

namespace epoHless
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BaseSelector baseSelector;
        [SerializeField] private TurretBaseUI turretBaseUI;
        [SerializeField] private EconomyManager economyManager;
        [SerializeField] private WaveManager waveManager;

        [Header("Turret Prefabs")]
        [SerializeField] private SellingTurret[] turretPrefabs;
        
        private TurretBase _selectedBase;
        
        private void OnEnable()
        {
            baseSelector.OnBaseSelected += OnBaseSelected;
            turretBaseUI.OnButtonClickedEvent += OnButtonClicked;
            waveManager.OnEnemyDeathEvent += OnEnemyDeath;
            waveManager.OnWaveEndEvent += OnWaveEnd;
        }
        
        private void OnDisable()
        {
            baseSelector.OnBaseSelected -= OnBaseSelected;
            turretBaseUI.OnButtonClickedEvent -= OnButtonClicked;
            waveManager.OnEnemyDeathEvent -= OnEnemyDeath;
            waveManager.OnWaveEndEvent -= OnWaveEnd;
        }
        
        private void OnBaseSelected(TurretBase turretBase)
        {
            _selectedBase = turretBase;
            
            turretBaseUI.TogglePanel(_selectedBase != null);
        }
        
        private void OnButtonClicked(int index)
        {
            if(_selectedBase.HasTurret()) return;
            
            var turret = turretPrefabs[index].Sell(economyManager);
            
            if (turret == null) return;
            
            var turretPrefab = Instantiate(turret);
            _selectedBase.AttachTurret(turretPrefab);
            
            _selectedBase = null;
            turretBaseUI.TogglePanel(false);
        }
        
        private void OnEnemyDeath(float amount)
        {
            economyManager.AddScore(amount);
        }
        
        private void OnWaveEnd(Wave wave)
        {
            economyManager.AddEnergy(wave.GetEnergyAfterWave());
        }
    }
    
    [System.Serializable]
    public class SellingTurret
    {
        [SerializeField] private Turret turret;
        
        [SerializeField] private float sellValue;

        public Turret Sell(EconomyManager economy)
        {
            if (economy.SpendEnergy(sellValue))
            {
                return turret;
            }
            
            return null;    
        }
        
        public float GetSellValue() => sellValue;
    }
}