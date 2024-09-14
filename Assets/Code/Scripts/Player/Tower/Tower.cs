using UnityEngine;

namespace epoHless
{
    /// <summary>
    /// The Tower class is a MonoBehaviour that represents a tower in the game.
    /// </summary>
    public class Tower : MonoBehaviour
    {
        [SerializeField] private TowerData data;
        [SerializeField] private Transform target;
        
        private HealthComponent _healthComponent;
        
        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
        }
        
        private void Start()
        {
            _healthComponent.SetHealth(data.GetHealth());
        }
        
        public HealthComponent GetHealthComponent() => _healthComponent;
        
        public Transform GetTarget() => target;
    }
}