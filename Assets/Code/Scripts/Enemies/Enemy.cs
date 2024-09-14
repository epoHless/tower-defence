using UnityEngine;

namespace epoHless
{
    /// <summary>
    /// The enemy class is responsible for the enemy's health and movement.
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyData data;
        
        private HealthComponent _healthComponent;
        private AIMovement _movement;
        
        private void Awake()
        {
            _movement = GetComponent<AIMovement>();
            _healthComponent = GetComponent<HealthComponent>();
        }

        private void Start()
        {
            _healthComponent.SetHealth(data.GetHealth());
            _movement.SetSpeed(data.GetSpeed());
        }

        private void OnEnable()
        {
            _movement.OnDestinationReached += OnDestinationReached;
        }
        
        private void OnDisable()
        {
            _movement.OnDestinationReached -= OnDestinationReached;
        }

        private void OnDestinationReached()
        {
            var results = new Collider[10];
            var size = Physics.OverlapSphereNonAlloc(transform.position, 1f, results);
            
            for (var i = 0; i < size; i++)
            {
                if (results[i].TryGetComponent(out Tower tower))
                {
                    tower.GetHealthComponent().TakeDamage(data.GetDamage());
                    
                    _healthComponent.TakeDamage(data.GetDamage());
                }
            }
            
            _healthComponent.TakeDamage(data.GetHealth());
        }

        public EnemyData GetData() => data;
        public AIMovement GetMovement() => _movement;
        public HealthComponent GetHealthComponent() => _healthComponent;
    }
}