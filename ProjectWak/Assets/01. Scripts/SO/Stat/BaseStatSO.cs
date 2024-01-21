using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ProjectWak.Stat
{
    [CreateAssetMenu(menuName = "SO/Stat/BaseStat")]
    public class BaseStatSO : ScriptableObject
    {
        public Stat MaxHP = new Stat();
        public Stat AttackDamage = new Stat();
        public Stat Armor = new Stat();

        protected Dictionary<StatType, Stat> stats = null;

        public Stat this[StatType index] => stats[index];

        private void OnEnable()
        {
            if (stats == null)
                stats = new Dictionary<StatType, Stat>();

            stats.Clear();

            Type characterStatType = GetType();
            foreach(StatType statType in Enum.GetValues(typeof(StatType)))
            {
                FieldInfo statField = characterStatType.GetField(statType.ToString());
                if(statField != null)
                    stats.Add(statType, statField.GetValue(this) as Stat);
            }
        }
    }
}
