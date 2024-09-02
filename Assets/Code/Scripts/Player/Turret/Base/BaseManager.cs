using UnityEngine;

namespace epoHless
{
    public class BaseManager : MonoBehaviour
    {
        [SerializeField] private BaseSelector baseSelector;
        [SerializeField] private TurretBaseUI turretBaseUI;

        [SerializeField] private Turret[] turretPrefabs;
        
        private TurretBase _selectedBase;
        
        private void OnEnable()
        {
            baseSelector.OnBaseSelected += OnBaseSelected;
            turretBaseUI.OnButtonClickedEvent += OnButtonClicked;
        }
        
        private void OnDisable()
        {
            baseSelector.OnBaseSelected -= OnBaseSelected;
            turretBaseUI.OnButtonClickedEvent -= OnButtonClicked;
        }

        private void OnBaseSelected(TurretBase turretBase)
        {
            _selectedBase = turretBase;
            
            turretBaseUI.TogglePanel(_selectedBase != null);
        }
        
        private void OnButtonClicked(int index)
        {
            var turretPrefab = Instantiate(turretPrefabs[index]);
            _selectedBase.AttachTurret(turretPrefab);
            
            _selectedBase = null;
            turretBaseUI.TogglePanel(false);
        }
    }
}