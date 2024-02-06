using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectWak.FSM
{
    public class FSMBrain : MonoBehaviour
    {
        // <prev, new>
        public UnityEvent<FSMState, FSMState> OnStateChangedEvent = null;

        [SerializeField] FSMState currentState = null;

        private void Awake()
        {
            Transform statesTrm = transform.Find("States");
            
            List<FSMState> states = new List<FSMState>();
            statesTrm.GetComponentsInChildren<FSMState>(states);
            states.ForEach(i => i.Init(this));
        }

        private void Update()
        {
            currentState?.UpdateState();
        }

        private void OnValidate()
        {
            if(transform.Find("States") == null)
                new GameObject("States").transform.SetParent(transform);
        }

        public void ChangeState(FSMState targetState)
        {
            OnStateChangedEvent?.Invoke(currentState, targetState);

            currentState?.ExitState();
            currentState = targetState;
            currentState?.EnterState();
        }
    }
}

