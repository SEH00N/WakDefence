using ProjectWak.FSM;
using ProjectWak.Modules;
using ProjectWak.Stat;
using UnityEngine;

namespace ProjectWak.Structure
{
    public abstract class Turret : Structure
    {
        [SerializeField] TurretProjectile projectile = null;
        protected FSMBrain brain = null;
        
        public Health Target = null;
        public EntityStatSO TurrentStat => stat as EntityStatSO;

        protected override void Awake()
        {
            base.Awake();

            brain = GetComponent<FSMBrain>();
        }

        public virtual TurretProjectile Fire(Vector3 position, Quaternion rotation)
        {
            TurretProjectile instance = Instantiate(projectile, position, rotation);
            instance.Init(stat, gameObject);

            return instance;
        }
    }
}