using UnityEngine;

namespace ProjectWak.Stat
{
    [CreateAssetMenu(menuName = "SO/Stat/EntityStat")]
    public class EntityStatSO : BaseStatSO
    {
        [Space(10f)]
        public Stat MoveSpeed = new Stat();
        public Stat AttackSpeed = new Stat();
        public Stat WorkSpeed = new Stat();
        public Stat CiriticalChance = new Stat();
    }
}