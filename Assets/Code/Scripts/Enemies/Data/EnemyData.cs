using UnityEngine;
using UnityEngine.Serialization;

namespace epoHless
{
    [CreateAssetMenu(fileName = "Enemy_", menuName = "Tower Defence/Enemies/New Enemy", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [Header("Enemy Stats")]
        [SerializeField] private float health;
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        
        public float GetHealth() => health;
        public float GetSpeed() => speed;
        public float GetDamage() => damage;
    }
}