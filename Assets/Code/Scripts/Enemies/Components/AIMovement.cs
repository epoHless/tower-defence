using System;
using UnityEngine;
using UnityEngine.AI;

namespace epoHless
{
    public class AIMovement : MonoBehaviour
    {
        public event Action OnDestinationReached;
        
        NavMeshAgent _agent;
        
        private void Awake() => _agent = GetComponent<NavMeshAgent>();
        
        public void MoveTo(Vector3 position) => _agent.SetDestination(position);
        
        public void SetSpeed(float speed) => _agent.speed = speed;

        private void Update()
        {
            if (Vector3.Distance(transform.position, _agent.destination) < 0.1f)
            {
                OnDestinationReached?.Invoke();
                
                enabled = false;
            }
        }
    }
}