using System;
using UnityEngine;

namespace epoHless
{
    public class BaseSelector : MonoBehaviour
    {
        private Camera _camera;
        
        public event Action<TurretBase> OnBaseSelected; 
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                OnBaseSelected?.Invoke(TrySelectBase(out var _base) ? _base : null);
            }
        }

        private bool TrySelectBase(out TurretBase _base)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                _base = hit.collider.GetComponent<TurretBase>();
                
                if (_base != null)
                {
                    Debug.Log($"Selected base: {_base}");
                    
                    return true;
                }
            }
            else
            {
                _base = null;
                
                Debug.Log("No base selected");
                
                return false;
            }
            
            return false;
        }
    }
}