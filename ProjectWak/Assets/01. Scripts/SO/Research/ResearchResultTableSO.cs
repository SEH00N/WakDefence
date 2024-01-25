using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectWak.Research
{
    [CreateAssetMenu(menuName = "SO/Research/ResultTable")]
    public partial class ResearchResultTableSO : ScriptableObject
    {
        [SerializeField] List<ResultItem> table = new List<ResultItem>();

        private float[] weights = null;
        private float[] Weights {
            get {
                if((weights == null) || (weights.Length != table.Count))
                    weights = table.Select(i => i.Rate).ToArray();
                return weights;
            }
        }

        public ResearchEventType GetItemByWeight()
        {
            float[] weights = Weights;
            float sum = weights.Sum();
            float weight = Random.Range(0f, sum);
            float nesting = 0f;

            for(int i = 0; i < weights.Length; i++)
            {
                float delta = nesting + weights[i];
                if(nesting <= weight && weight < delta)
                    return table[i].ResearchEvent;
                else
                    nesting = delta;
            }

            return ResearchEventType.LAST;
        }
    }
}