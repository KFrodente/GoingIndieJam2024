using System;
using UnityEngine;

namespace Stats
{
    public abstract class StatModifier : IDisposable
    {
        public bool MarkedForRemoval { get; set; }
        public event Action<StatModifier> OnDispose = delegate { };
        public readonly CountdownTimer timer;

        protected StatModifier(float duration)
        {
            if(duration <= 0) return;
            timer = new CountdownTimer(duration);
            timer.OnTimerStop += () => MarkedForRemoval = true;
            timer.Start();
        }

        public void Update(float dt)
        {
            if(timer != null) timer.Tick(dt);
        }

        public abstract void Handle(object sender, Query query);
        public void Dispose()
        {
            OnDispose?.Invoke(this);
        }
    }
    
    public class BasicStatModifier : StatModifier
    {
        readonly StatType type;
        readonly Func<float, float> operation;

        public BasicStatModifier(StatType _type, Func<float, float> _operation, float _duration) : base(_duration)
        {
            type = _type;
            operation = _operation;
        }
        public override void Handle(object sender, Query query)
        {
            if (query.type == type)
            {
                query.value = operation(query.value);
            }
        }
    }
}