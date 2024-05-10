using System;
using UnityEngine;

namespace Stats
{
    public enum OperatorType {Add, Multiply}
    
    [System.Serializable]
    public class StatEffect
    {
        [SerializeField] private StatType type;
        [SerializeField] private OperatorType operation;
        [SerializeField] private float value;
        [SerializeField] private float duration;

        private StatModifier statMod = null;
        public StatModifier GetModifier()
        {
            if (statMod != null) return statMod;
            StatModifier modifier = operation switch
            {
                OperatorType.Add => new BasicStatModifier(type, (x) => x + value, duration),
                OperatorType.Multiply => new BasicStatModifier(type, (x) => x * value, duration),
                _ => throw new ArgumentOutOfRangeException()
            };
            statMod = modifier;
            return modifier;
        }
    }
}