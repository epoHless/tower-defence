using UnityEngine;

namespace epoHless
{
    /// <summary>
    /// Turret class is responsible for shooting enemies. It finds the closest enemy in range and shoots at it.
    /// </summary>
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretData data;

        private float _damage;
        private float _range;
        private float _fireRate;

        private float _fireCountdown;

        private Enemy _target;

        private void Start()
        {
            _damage = data.GetDamage();
            _range = data.GetRange();
            _fireRate = data.GetFireRate();
        }

        private void Update()
        {
            // If there is no target, find one
            if (_target == null)
            {
                FindTarget();
                return;
            }
            
            // If there is a target, check if it is still in range
            TargetUpdate();
            
            // If there is no target, return    
            if (_target == null) return;

            // Rotate towards the target
            RotationUpdate();
            
            // If the turret is ready to shoot, shoot
            ShootUpdate();
        }

        private void TargetUpdate()
        {
            var distance = Vector3.Distance(transform.position, _target.transform.position);

            if (distance > _range)
            {
                _target = null;
            }
        }

        private void FindTarget()
        {
            var results = new Collider[10];
            
            // Find all enemies in range of the turret within a sphere
            var size = Physics.OverlapSphereNonAlloc(transform.position, _range, results);
            
            // Check if there are any enemies in range
            for (var i = 0; i < size; i++)
            {
                // If the object is not an enemy, skip
                if (results[i].TryGetComponent(out Enemy enemy))
                {
                    _target = enemy;
                    return;
                }
            }
        }

        private void RotationUpdate()
        {
            var direction = _target.transform.position - transform.position;
            var rotation = Quaternion.LookRotation(direction);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }

        private void ShootUpdate()
        {
            // If the turret is ready to shoot, shoot   
            if (_fireCountdown <= 0f)
            {
                Shoot();
                _fireCountdown = _fireRate;
            }

            
            // Decrease the fire countdown by the time passed since the last frame
            _fireCountdown -= Time.deltaTime;
        }

        private void Shoot()
        {
            _target.GetHealthComponent().TakeDamage(_damage);
        }

        // Draw the range of the turret in the editor for debugging purposes
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _range);
        }
    }
}