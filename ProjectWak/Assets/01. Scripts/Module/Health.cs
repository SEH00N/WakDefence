using ProjectWak.Stat;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectWak.Modules
{
    public class Health : MonoBehaviour
    {
        // <this, performer, amount>
        public UnityEvent<Health, GameObject, float> OnHPModifiedEvent = null;
        public UnityEvent OnDeadEvent = null;

        private float currentHP = 0f;
        public float CurrentHP => currentHP;

        private float maxHP = 50f;
        private float armor = 10f;

        private bool isDead = false;
        public bool IsDead => isDead;

        public void Init(BaseStatSO stat)
        {
            maxHP = stat.MaxHP.CurrentValue;
            armor = stat.Armor.CurrentValue;

            stat.MaxHP.OnValueChangedEvent += value => maxHP = value;
            stat.Armor.OnValueChangedEvent += value => armor = value;
        }

        public void Reset()
        {
            isDead = false;
            currentHP = maxHP;
        }

        public void TakeDamage(float damage, GameObject performer)
        {
            if(isDead)
                return;

            damage = CalculateArmor(damage);
            ModifyHP(-damage, false);
            OnHPModifiedEvent?.Invoke(this, performer, -damage);

            if(currentHP <= 0f)
                OnDie();
        }

        public void HealHP(float amount, GameObject performer, bool overflow = false)
        {
            float modifiedAmount = ModifyHP(amount, overflow);
            OnHPModifiedEvent?.Invoke(this, performer, modifiedAmount);
        }

        private float ModifyHP(float amount, bool overflow)
        {
            float prevHP = currentHP;
            currentHP += amount;
            if(overflow == false)
                currentHP = Mathf.Clamp(currentHP, 0f, maxHP);
            
            return currentHP - prevHP;
        }

        private float CalculateArmor(float damage)
        {
            float factor = 100 / (100 + armor);
            return damage * factor;
        }

        private void OnDie()
        {
            isDead = true;
            OnDeadEvent?.Invoke();
        }
    }
}
