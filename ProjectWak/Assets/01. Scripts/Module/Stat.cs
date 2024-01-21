using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWak.Stat
{
    [Serializable]
    public partial class Stat
    {
        [SerializeField] float baseValue = 10f;

        private float currentValue = 10f;
        public float CurrentValue => currentValue;

        public event Action<float> OnValueChangedEvent = null;

        private List<float> modifiers = new List<float>();
        private Dictionary<string, float> preMultipliers = new Dictionary<string, float>();
        private Dictionary<string, float> postMultipliers = new Dictionary<string, float>();

        public void Clear()
        {
            modifiers.Clear();
            preMultipliers.Clear();
            postMultipliers.Clear();

            currentValue = baseValue;
        }

        private void CalculateValue()
        {
            currentValue = baseValue;

            modifiers.ForEach(i => currentValue += i);

            foreach(var pair in preMultipliers)
                currentValue += baseValue * pair.Value;

            foreach (var pair in postMultipliers)
                currentValue += currentValue * pair.Value;

            OnValueChangedEvent?.Invoke(currentValue);
        }

        public void SetBaseValue(float value)
        {
            baseValue = value;
            CalculateValue();
        }

        public void AddModifier(float modifier)
        {
            modifiers.Add(modifier);
            CalculateValue();
        }

        public void RemoveModifier(float modifier)
        {
            modifiers.Remove(modifier);
            CalculateValue();
        } 

        public void AddPreMultiplier(string key, float multiplier) => AddMultiplier(preMultipliers, key, multiplier);
        public void AddPostMultiplier(string key, float multiplier) => AddMultiplier(postMultipliers, key, multiplier);
        public void RemovePreMultiplier(string key) => RemoveMultiplier(preMultipliers, key);
        public void RemovePostMultiplier(string key) => RemoveMultiplier(postMultipliers, key);

        private void AddMultiplier(Dictionary<string, float> multipliers, string key, float multiplier) 
        {
            multipliers.TryAdd(key, multiplier);
            CalculateValue();
        }

        public void RemoveMultiplier(Dictionary<string, float> multipliers, string key)
        {
            multipliers.Remove(key);
            CalculateValue();
        } 
    }
}
