using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWak.Research
{
    [CreateAssetMenu(menuName = "SO/Research/ResearchData")]
    public class ResearchDataSO : ScriptableObject
    {
        [SerializeField] ResearchResultTableSO resultTable = null;
        public ResearchResultTableSO ResultTable => resultTable;

        public Action<ResearchEventType> OnResearchedEvent = null;

        private List<ResearchEventType> researchResults = null;
        public List<ResearchEventType> ResearchResults => researchResults;

        public int ResearchedCount => researchResults.Count;
    }
}