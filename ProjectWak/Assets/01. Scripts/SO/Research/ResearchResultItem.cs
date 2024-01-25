using UnityEngine;

namespace ProjectWak.Research
{
    public partial class ResearchResultTableSO : ScriptableObject
    {
        [System.Serializable]
        public struct ResultItem
        {
            public ResearchEventType ResearchEvent;
            public float Rate;
        }
    }
}
