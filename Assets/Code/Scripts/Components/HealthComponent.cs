using UnityEngine;

namespace epoHless
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private HealthUI healthUI;
        [SerializeField] private ParticleSystem deathEffect;
        public event System.Action OnDeath;
        
        private float _maxHealth;
        private float _health;
        
        public void TakeDamage(float damage)
        {
            _health -= damage;
            
            if(healthUI) healthUI.UpdateHealth(_health / _maxHealth);
            
            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
            
            OnDeath?.Invoke();
            
            Destroy(gameObject);
        }
        
        public bool IsAlive()
        {
            return _health > 0;
        }
        
        public void SetHealth(float health)
        {
            _maxHealth = health;
            _health = _maxHealth;
        }
    }
}