using UnityEngine;

namespace ProjectWak.FSM.Turret
{
    using ProjectWak.Stat;
    using ProjectWak.Structure;

    public class TurretFireAction : FSMAction
    {
        [SerializeField] Transform head = null;
        [SerializeField] Transform firePos = null;

        [Space(15f)]
        [SerializeField] float rotateSpeed = 15f;

        private Turret turret = null;
        private EntityStatSO turretStat = null;

        private float lastFiredTime = 0f;
        private float attackDelay = 1f;

        public override void Init(FSMBrain brain, FSMState state)
        {
            base.Init(brain, state);
            turret = brain.GetComponent<Turret>();

            turretStat = turret.TurrentStat;

            attackDelay = 1f / turretStat.AttackSpeed.CurrentValue;
            turretStat.AttackSpeed.OnValueChangedEvent += (currentValue) => {
                attackDelay = 1f / currentValue;
            };
        }

        public override void EnterState()
        {
            base.EnterState();
            lastFiredTime = Time.time;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if(turret.Target == null)
                return;

            
            Quaternion targetRotation = Quaternion.LookRotation(turret.Target.transform.position - head.position, Vector3.up);
            Quaternion current = Quaternion.Lerp(head.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            head.rotation = current;

            if(lastFiredTime + attackDelay < Time.time)
            {
                turret.Fire(firePos.position, head.rotation);
                lastFiredTime = Time.time;
            }
        }
    }
}
