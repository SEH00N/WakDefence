using UnityEngine;

namespace ProjectWak.FSM.Turret
{
    using System;
    using System.Collections;
    using ProjectWak.Modules;
    using ProjectWak.Structure;

    public class TurretDetectAction : FSMAction
    {
        [SerializeField] int containerSize = 30;
        [SerializeField] float radius = 10f;
        [SerializeField] LayerMask targetLayer = 0;

        private Turret turret = null;
        private Collider[] container = null;
        
        public override void Init(FSMBrain brain, FSMState state)
        {
            base.Init(brain, state);
            turret = brain.GetComponent<Turret>();

            container = new Collider[containerSize];
        }

        public override void UpdateState()
        {
            base.UpdateState();
            
            turret.Target = null;

            int detected = Physics.OverlapSphereNonAlloc(turret.transform.position, radius, container, targetLayer);
            turret.Target = SelectTarget(detected);
        }

        private Health SelectTarget(int detected)
        {
            if(detected <= 0)
                return null;

            if(detected > 1)
            {
                Array.Sort(container, (a, b) => {
                    float distanceA = a == null ? float.MaxValue : (transform.position - a.transform.position).sqrMagnitude;
                    float distanceB = b == null ? float.MaxValue : (transform.position - b.transform.position).sqrMagnitude;

                    if(distanceA == distanceB)
                        return 0;
                    else if(distanceA > distanceB)
                        return 1;
                    else return -1;
                });
            }
            
            for(int i = 0; i < detected; i++)
            {
                Health result = container[i].GetComponent<Health>();
                if(result != null && result.IsDead == false)
                    return result;
            }

            return null;
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
