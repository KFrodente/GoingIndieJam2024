using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Stats
{
    public class StatMediator
    {
        private LinkedList<StatModifier> modifiers = new LinkedList<StatModifier>();
        public event EventHandler<Query> Queries;

        public void PerformQuery(object sender, Query query)
        {
            Queries?.Invoke(sender, query);
        }
        public void AddModifier(StatModifier mod)
        {
            modifiers.AddLast(mod);
            Queries += mod.Handle;
            
            mod.OnDispose += (x) =>
            {
                modifiers.Remove(mod);
                Queries -= x.Handle;
            };
        }

        public void Update(float dt)
        {
            foreach (var mod in modifiers)
            {
                mod.Update(dt);
            }

            var node = modifiers.First;
            while (node != null)
            {
                var next = node.Next;
                if (node.Value.MarkedForRemoval)
                {
                    node.Value.Dispose();
                }
                node = next;
            }
        }
    }

    public class Query
    {
        public readonly StatType type;
        public float value;

        public Query(StatType _type, float _value)
        {
            type = _type;
            value = _value;
        }
    }
    
}