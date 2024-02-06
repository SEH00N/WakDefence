using UnityEngine;

namespace ProjectWak.FSM.Turret
{
    using System;
    using System.Collections;
    using ProjectWak.Modules;
    using ProjectWak.Structure;

    public class TurretDetectAction : FSMAction
    {
        [SerializeField] float radius = 10f;
        [SerializeField] LayerMask targetLayer = 0;

        private Turret turret = null;
        private Collider[] container = new Collider[30];
        
        public override void Init(FSMBrain brain, FSMState state)
        {
            base.Init(brain, state);
            turret = brain.GetComponent<Turret>();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            
            turret.Target = null;

            int detected = Physics.OverlapSphereNonAlloc(turret.transform.position, radius, container, targetLayer);
            if(detected > 1)
            {
                Array.Sort(container, (a, b) => {
                    if(a == null | b == null)
                        return -1;

                    float distanceA = (transform.position - a.transform.position).sqrMagnitude;
                    float distanceB = (transform.position - b.transform.position).sqrMagnitude;
                    if(distanceA == distanceB)
                        return 0;
                    else if(distanceA > distanceB)
                        return 1;
                    else return -1;
                });
            }

            if(detected > 0)
                container[0].transform.TryGetComponent<Health>(out turret.Target);
        }

        #if UNITY_EDITOR
        [Space(15f)]
        [SerializeField] bool gizmo = true;
        private void OnDrawGizmos()
        {
            if (gizmo == false)
                return;

            if (turret == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(turret.transform.position, radius);
        }
        #endif
    }
}
