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
        public float MaxHP => maxHP;

        private bool isDead = false;
        public bool IsDead => isDead;

        public void SetMaxHP(float maxHP)
        {
            this.maxHP = maxHP;
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

        private void OnDie()
        {
            isDead = true;
            OnDeadEvent?.Invoke();
        }
    }
}
