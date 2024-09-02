using System;
using UnityEngine;

namespace epoHless
{
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
            if (_target == null)
            {
                FindTarget();
                return;
            }
            
            TargetUpdate();
            
            if (_target == null) return;

            RotationUpdate();
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
            var size = Physics.OverlapSphereNonAlloc(transform.position, _range, results);

            for (var i = 0; i < size; i++)
            {
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
            if (_fireCountdown <= 0f)
            {
                Shoot();
                _fireCountdown = _fireRate;
            }

            _fireCountdown -= Time.deltaTime;
        }

        private void Shoot()
        {
            _target.GetHealthComponent().TakeDamage(_damage);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _range);
        }
    }
}