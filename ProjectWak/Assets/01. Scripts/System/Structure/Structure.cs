using ProjectWak.Modules;
using ProjectWak.Research;
using ProjectWak.Stat;
using UnityEngine;

namespace ProjectWak.Structure
{
    public abstract class Structure : MonoBehaviour
    {
        [SerializeField] protected BaseStatSO stat = null;
        [SerializeField] protected ResearchDataSO researchData = null;
        protected Health health = null;
        
        public virtual void OnStructurePlaced() {}
        public virtual void OnStructureDestroyed() {}

        protected abstract void HandleResearched(ResearchEventType type);

        protected virtual void Awake()
        {
            health = GetComponent<Health>();
            health.Init(stat);

            researchData.OnResearchedEvent += HandleResearched;
            health.OnDeadEvent.AddListener(OnStructureDestroyed);
        }

        protected virtual void OnDestroy()
        {
            researchData.OnResearchedEvent -= HandleResearched;
            health.OnDeadEvent.RemoveListener(OnStructureDestroyed);
        }
    }
}
