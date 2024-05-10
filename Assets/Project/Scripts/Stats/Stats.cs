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
            return new Stats(this.Damage * other.Mult_Damage, this.MoveSpeed * other.Mult_MoveSpeed, this.ChargeSpeed * other.Mult_ChargeSpeed, this.TurnSpeed * other.Mult_TurnSpeed, this.AttackRange * other.Mult_Range, this.AttackSpeed * other.Mult_AttackSpeed);
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
        public float AttackRange
        {
            get
            {
                var q = new Query(StatType.AttackRange, baseStats.attackRange);
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
                var q = new Query(StatType.Mult_AttackRange, baseStats.attackRangeMult);
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

        public Stats(float v1, float v2, float v3, float v4, float v5, float v6)
        {
            baseStats = new BaseStats();
            baseStats.damage = v1;
            baseStats.moveSpeed = v2;
            baseStats.chargeSpeed = v3;
            baseStats.turnSpeed = v4;
            baseStats.attackRange = v5;
            baseStats.attackSpeed = v6;
        }
    }
    
    
    
}