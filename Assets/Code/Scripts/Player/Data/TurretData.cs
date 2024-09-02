using UnityEngine;

namespace epoHless
{
    [CreateAssetMenu(fileName = "Turret_", menuName = "Tower Defence/Turrets/New Turret", order = 0)]
    public class TurretData : ScriptableObject
    {
        [SerializeField] private float damage;
        [SerializeField] private float range;
        [SerializeField] private float fireRate;
        
        public float GetDamage() => damage;
        public float GetRange() => range;
        public float GetFireRate() => fireRate;
    }
}