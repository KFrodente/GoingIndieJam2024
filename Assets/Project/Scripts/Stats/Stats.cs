using System;
using Unity.VisualScripting;

namespace Stats
{
    public class Stats
    {
        public readonly StatMediator mediator = new StatMediator();
        public BaseStats baseStats;

        public Stats MultiplyModifier(Stats other)
        {
            return new Stats(this.Damage * other.Mult_Damage, this.Defense * other.Mult_Defence, this.MoveSpeed * other.Mult_MoveSpeed, this.ChargeSpeed * other.Mult_ChargeSpeed, this.MaxMoveSpeed * other.Mult_MaxMoveSpeed, this.TurnSpeed * other.Mult_TurnSpeed, this.Range * other.Mult_Range, this.AttackSpeed * other.Mult_AttackSpeed);
        }

        public float Damage
        {
            get
            {
                var q = new Query(StatType.Damage, baseStats.damage);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public float AttackSpeed
        {
            get
            {
                var q = new Query(StatType.AttackSpeed, baseStats.attackSpeed);
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
        public float Mult_Damage
        {
            get
            {
                var q = new Query(StatType.Mult_Damage, baseStats.damageMult);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public float Mult_Defence
        {
            get
            {
                var q = new Query(StatType.Mult_Defence, baseStats.defenceMult);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public float Mult_MoveSpeed
        {
            get
            {
                var q = new Query(StatType.Mult_MoveSpeed, baseStats.moveSpeedMult);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public float Mult_ChargeSpeed
        {
            get
            {
                var q = new Query(StatType.Mult_ChargeSpeed, baseStats.chargeSpeedMult);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public float Mult_MaxMoveSpeed
        {
            get
            {
                var q = new Query(StatType.Mult_MaxMoveSpeed, baseStats.maxMoveSpeedMult);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public float Mult_TurnSpeed
        {
            get
            {
                var q = new Query(StatType.Mult_TurnSpeed, baseStats.turnSpeedMult);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }

        public float Mult_Range
        {
            get
            {
                var q = new Query(StatType.Mult_Range, baseStats.rangeMult);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }
        public float Mult_AttackSpeed
        {
            get
            {
                var q = new Query(StatType.Mult_AttackSpeed, baseStats.attackSpeedMult);
                mediator.PerformQuery(this, q);
                return q.value;
            }
        }

        public Stats(StatMediator _mediator, BaseStats _baseStats)
        {
            mediator = _mediator;
            baseStats = _baseStats;
        }

        public Stats(float v1, float v2, float v3, float v4, float v5, float v6, float v7, float v8)
        {
            baseStats = new BaseStats();
            baseStats.damage = v1;
            baseStats.defense = v2;
            baseStats.moveSpeed = v3;
            baseStats.chargeSpeed = v4;
            baseStats.maxMoveSpeed = v5;
            baseStats.turnSpeed = v6;
            baseStats.range = v7;
            baseStats.attackSpeed = v8;
        }
    }
    
    
    
}