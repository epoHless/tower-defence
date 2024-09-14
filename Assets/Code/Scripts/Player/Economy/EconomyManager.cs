using System;
using UnityEngine;

namespace epoHless
{
    /// <summary>
    /// The EconomyManager class is responsible for managing the player's score and energy.
    /// </summary>
    public class EconomyManager : MonoBehaviour
    {
        [SerializeField] private float score;
        [SerializeField] private float energy;
        [SerializeField] private EconomyUI economyUI;

        private void Start()
        {
            economyUI.UpdateScore(score);
            economyUI.UpdateEnergy(energy);
        }

        public void AddScore(float amount)
        {
            score += amount;
            
            economyUI.UpdateScore(score);
        }
        
        public void AddEnergy(float amount)
        {
            energy += amount;
            
            economyUI.UpdateEnergy(energy);
        }
        
        public bool SpendEnergy(float amount)
        {
            if (energy < amount) return false;
            
            energy -= amount;
            
            economyUI.UpdateEnergy(energy);
            
            return true;
        }
    }
}