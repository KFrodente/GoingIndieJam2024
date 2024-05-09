using System;

namespace Stats
{
    public class Stats
    {
        public readonly StatMediator mediator = new StatMediator();
        readonly BaseStats baseStats;

        public float Damage
        {
            get
            {
                var q = new Query(StatType.Damage, baseStats.damage);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public float ChargeSpeed
        {
            get
            {
                var q = new Query(StatType.ChargeSpeed, baseStats.chargeSpeed);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public float Range
        {
            get
            {
                var q = new Query(StatType.Range, baseStats.range);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public float Defense
        {
            get
            {
                var q = new Query(StatType.Defence, baseStats.defense);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public float MoveSpeed
        {
            get
            {
                var q = new Query(StatType.MoveSpeed, baseStats.moveSpeed);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
		public float MaxMoveSpeed
		{
			get
			{
				var q = new Query(StatType.MaxMoveSpeed, baseStats.moveSpeed);
				mediator.PerformQuery(this, q);
				return q.value;
			}
		}
		public float TurnSpeed
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