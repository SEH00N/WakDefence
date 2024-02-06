using UnityEngine;

namespace ProjectWak.FSM.Turret
{
    using ProjectWak.Structure;

    public class TargetDetectDecision : FSMDecision
    {
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

            if(turret.Target.IsDead)
                return false;

            return true;
        }
    }
}
