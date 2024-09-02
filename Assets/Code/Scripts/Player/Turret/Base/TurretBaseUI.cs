using System;
using UnityEngine;
using UnityEngine.UI;

namespace epoHless
{
    public class TurretBaseUI : MonoBehaviour
    {
        [SerializeField] private RectTransform panel;
        
        [SerializeField] private Button[] buttons;
        
        public event Action<int> OnButtonClickedEvent;
        
        private void Start()
        {
            Initialize();
            
            TogglePanel(false);
        }

        private void Initialize()
        {
            int i = 0;
            
            foreach (var button in buttons)
            {
                var index = i;
                
                button.onClick.AddListener(() => OnButtonClicked(index));
                
                i++;
            }
        }

        private void OnButtonClicked(int index)
        {
            OnButtonClickedEvent?.Invoke(index);
        }
        
        public void TogglePanel(bool state)
        {
            panel.gameObject.SetActive(state);
        }
    }
}