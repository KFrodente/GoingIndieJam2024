using System;
using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

namespace Stats
{
    public class StatManipulator : MonoBehaviour
    {
        [SerializeField] private List<StatEffect> effects = new List<StatEffect>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out StatHandler handler))
            {
                Debug.Log("Added");
                foreach (var e in effects)
                {
                    handler.AddStatModifier(e.GetModifier());
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
        public StatModifier GetModifier()
        {
            StatModifier modifier = operation switch
            {
                OperatorType.Add => new BasicStatModifier(type, (x) => x + value, duration),
                OperatorType.Multiply => new BasicStatModifier(type, (x) => x * value, duration),
                _ => throw new ArgumentOutOfRangeException()
            };
            return modifier;
        }
    }
    public enum OperatorType {Add, Multiply}
}