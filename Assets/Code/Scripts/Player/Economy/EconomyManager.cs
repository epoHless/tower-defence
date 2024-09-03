using System;
using UnityEngine;

namespace epoHless
{
    public class EconomyManager : MonoBehaviour
    {
        [SerializeField] private float score;
        [SerializeField] private float energy;
        [SerializeField] private EconomyUI economyUI;

        public float GetScore() => score;
        
        public float GetEnergy() => energy;

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
    }
}