using UnityEngine;

namespace epoHless
{
    public class TurretBase : MonoBehaviour
    {
        [SerializeField] private Transform attachmentPoint;
        
        private Turret _turret;
        
        public void AttachTurret(Turret turret)
        {
            _turret = turret;
            _turret.transform.SetParent(attachmentPoint);
            _turret.transform.localPosition = Vector3.zero;
        }
        
        public bool HasTurret() => _turret != null;
    }
}