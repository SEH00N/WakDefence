using UnityEngine;

namespace ProjectWak.FSM.Turret
{
    using ProjectWak.Structure;

    public class TargetDistanceDecision : FSMDecision
    {
        [SerializeField] float distance = 5f;
        private Turret turret = null;

        public override void Init(FSMBrain brain, FSMState state)
        {
            base.Init(brain, state);
            turret = brain.GetComponent<Turret>();
        }

        public override bool MakeDecision()
        {
            if(turret.Target == null)
                return false;

            float sqrMagnitude = (turret.Target.transform.position - turret.transform.position).sqrMagnitude;
            return sqrMagnitude < (distance * distance);
        }

        #if UNITY_EDITOR
        [Space(15f)]
        [SerializeField] bool gizmo = true;
        private void OnDrawGizmos()
        {
            if(gizmo == false)
                return;

            if(turret == null)
                return;

            if(turret.Target == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(turret.transform.position, turret.Target.transform.position);

            Gizmos.color = Color.green;
            Vector3 dir = turret.Target.transform.position - turret.transform.position;
            Gizmos.DrawLine(turret.transform.position, turret.transform.position + dir.normalized * distance);
        }
        #endif
    }
}
