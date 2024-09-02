using UnityEngine;

namespace epoHless
{
    public class WiggleComponent : MonoBehaviour
    {
        [SerializeField] AnimationCurve curve;
        [SerializeField] float speed = 1f;
        [SerializeField] float amplitude = 1f;
        
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private float _time;
        
        private Transform parent;
        
        private void Awake()
        {
            _startPosition = transform.position;
            _targetPosition = _startPosition + Vector3.up * amplitude;
            
            parent = transform.parent;
        }

        private void Update()
        {
            _time += Time.deltaTime * speed;
            
            var position = Vector3.Lerp(_startPosition, _targetPosition, curve.Evaluate(_time));
            
            var parentPosition = parent.position;
            
            position.x = parentPosition.x;
            position.z = parentPosition.z;
            
            transform.position = position;
            
            if (_time >= 1f)
            {
                (_targetPosition, _startPosition) = (_startPosition, _targetPosition);

                _time = 0f;
            }
        }
    }
}