using System.Collections.Generic;
using UnityEngine;

namespace ProjectWak.FSM
{
    public class FSMState : MonoBehaviour
    {
        [SerializeField] bool autoTransitioning = true;

        private FSMBrain brain;

        private List<FSMAction> actions;
        private List<FSMTransition> transitions;

        public void Init(FSMBrain brain)
        {
            this.brain = brain;

            actions = new List<FSMAction>();
            GetComponents<FSMAction>(actions);
            actions.ForEach(i => i.Init(brain, this));

            transitions = new List<FSMTransition>();
            GetComponentsInChildren<FSMTransition>(transitions);
            transitions.ForEach(i => i.Init(brain, this));
        }

        public void EnterState()
        {
            actions.ForEach(i => i.EnterState());

            if(autoTransitioning)
                transitions.ForEach(i => i.EnterState());
        }

        public void UpdateState()
        {
            actions.ForEach(i => i.UpdateState());

            if(autoTransitioning)
            {
                foreach(FSMTransition transition in transitions)
                {
                    if(transition.CheckDecisions())
                    {
                        brain.ChangeState(transition.TargetState);
                        break;
                    }
                }
            }
        }

        public void ExitState()
        {
            actions.ForEach(i => i.ExitState());

            if(autoTransitioning)
                transitions.ForEach(i => i.ExitState());
        }
    }
}
