using System;
using UnityEngine;
using UnityEngine.UI;

namespace epoHless
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image _healthBar;

        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            UpdateHealth(1);
        }

        public void UpdateHealth(float percentage)
        {
            _healthBar.fillAmount = percentage;
            _healthBar.color = gradient.Evaluate(percentage);
        }
    }
}