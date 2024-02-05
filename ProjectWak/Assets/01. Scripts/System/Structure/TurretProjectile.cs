using ProjectWak.Modules;
using ProjectWak.Stat;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectWak.Structure
{
    public class TurretProjectile : MonoBehaviour
    {
        [SerializeField] LayerMask targetLayer = 0;
        [SerializeField] bool onShot = true;

        public UnityEvent OnHitEvent = null;

        private BaseStatSO stat = null;
        private GameObject owner = null;

        public void Init(BaseStatSO stat, GameObject owner)
        {
            this.stat = stat;
            this.owner = owner;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            LayerMask otherLayer = 1 << other.gameObject.layer;
            if ((otherLayer & targetLayer) > 0)
            {
                if (other.TryGetComponent<Health>(out Health health))
                {
                    health?.TakeDamage(stat.AttackDamage.CurrentValue, owner);
                    OnHit(other);
                    OnHitEvent?.Invoke();

                    if(onShot)
                        Destroy(gameObject);
                }
            }
        }

        public virtual void OnHit(Collider other) { }
    }
}