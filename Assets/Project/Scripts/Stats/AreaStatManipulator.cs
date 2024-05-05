using System;
using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

namespace Stats
{
    public class AreaStatManipulator : MonoBehaviour
    {
        [SerializeField] private List<StatEffect> effects = new List<StatEffect>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out StatHandler handler))
            {
                foreach (var e in effects)
                {
                    handler.AddStatModifier(e.GetModifier());
                }
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out StatHandler handler))
            {
                foreach (var e in effects)
                {
                    handler.RemoveStatModifier(e.GetModifier());
                }
            }
        }
    }

    [System.Serializable]
    public class StatEffect
    {
        [SerializeField] private StatType type;
        [SerializeField] private OperatorType operation;
        [SerializeField] private int value;
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
    public enum OperatorType {Add, Multiply}
}