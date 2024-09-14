using UnityEngine;

namespace epoHless
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        
        [SerializeField, Tooltip("The component responsible of the selection of the turrets attachments")] 
        private BaseSelector baseSelector;
        
        [SerializeField, Tooltip("The UI where we will be buying our turrets")] 
        private TurretBaseUI turretBaseUI;
        
        [SerializeField, Tooltip("The manager responsible of handling score and points")] 
        private EconomyManager economyManager;
        
        [SerializeField, Tooltip("The manager responsible of enemy wave management")] 
        private WaveManager waveManager;

        [Header("Turret Prefabs")]
        [SerializeField] private SellingTurret[] turretPrefabs;
        
        private TurretBase _selectedBase;
        
        /// <summary>
        /// Subscribe to the events of the different components
        /// </summary>
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
        
        /// <summary>
        /// When a base is selected, we store it and show the UI
        /// </summary>
        /// <param name="turretBase"></param>
        private void OnBaseSelected(TurretBase turretBase)
        {
            _selectedBase = turretBase;
            
            turretBaseUI.TogglePanel(_selectedBase != null);
        }
        
        /// <summary>
        /// When a button is clicked, we check if the base has a turret, if not we sell the turret and attach it to the base
        /// </summary>
        /// <param name="index"></param>
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
        
        /// <summary>
        /// When an enemy dies, we add the score to the economy manager 
        /// </summary>
        /// <param name="amount"></param>
        private void OnEnemyDeath(float amount)
        {
            economyManager.AddScore(amount);
        }
        
        /// <summary>
        /// When a wave ends, we add the energy to the economy manager
        /// </summary>
        /// <param name="wave"></param>
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