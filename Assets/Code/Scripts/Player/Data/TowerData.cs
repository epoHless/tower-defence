using UnityEngine;

namespace epoHless
{
    [CreateAssetMenu(fileName = "Tower_", menuName = "Tower Defence/Towers/New Tower", order = 0)]
    public class TowerData : ScriptableObject
    {
        [SerializeField] private string towerName;  
        [SerializeField] private float health;
        
        public string GetTowerName() => towerName;
        public float GetHealth() => health;
    }
}