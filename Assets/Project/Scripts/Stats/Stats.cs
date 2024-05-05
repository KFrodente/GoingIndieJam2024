using System;

namespace Stats
{
    public class Stats
    {
        public readonly StatMediator mediator = new StatMediator();
        readonly BaseStats baseStats;

        public int Damage
        {
            get
            {
                var q = new Query(StatType.Damage, baseStats.damage);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public int Health
        {
            get
            {
                var q = new Query(StatType.Health, baseStats.health);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public int Defence
        {
            get
            {
                var q = new Query(StatType.Defence, baseStats.defence);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public int MoveSpeed
        {
            get
            {
                var q = new Query(StatType.MoveSpeed, baseStats.moveSpeed);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public int TurnSpeed
        {
            get
            {
                var q = new Query(StatType.TurnSpeed, baseStats.turnSpeed);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        

        public Stats(StatMediator _mediator, BaseStats _baseStats)
        {
            mediator = _mediator;
            baseStats = _baseStats;
        }
        
    }
    
    
    
}