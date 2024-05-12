using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedNeutralEnemyInput : RangedEnemyInput
{
    [SerializeField] protected int benefitAttackCountPeriod;
    [SerializeField] protected float friendSearchRadius;
    protected int currentAttackNumber = 0;
    public override Target GetInputTarget()
    {
        Target t = null;
        if (currentAttackNumber == benefitAttackCountPeriod)
        {
            Transform enemy = GetAnEnemyPos();
            if (enemy != null)
            {
                t = new Target(TargetType.Position, enemy.position, null, transform.position, false);
                t.uniqueCaseID = TargetCaseID.Friendly;
                currentAttackNumber = -1;
            }
        }
        if (BaseCharacter.playerCharacter.transform == null) return null;
        t = new Target(TargetType.Character, null, BaseCharacter.playerCharacter.transform, transform.position, false);
        currentAttackNumber++;
        return t;
    }
    public override MouseInputData GetMouseInput()
    {
        return new MouseInputData
        {
            leftDown = (GetDistance() < character.GetStats().AttackRange * 3),
            //leftDown = (GetDistance() < preferedRange * 1.5f), // Might want to get actual attack range from stats
            leftUp = (GetDistance() > character.GetStats().AttackRange * 3),
            //leftUp = (GetDistance() > preferedRange * 1.5f),
            rightDown = false,
            rightUp = false,
            middleDown = false,
            middleUp = false
        };
    }
    public virtual Transform GetAnEnemyPos()
    {

        Transform lowestHealth = null;
        float lowestHealthValue = float.MaxValue;
        Collider2D[] characterInRange = Physics2D.OverlapCircleAll(transform.position, friendSearchRadius, LayerMask.GetMask("Character"));
        foreach (Collider2D character in characterInRange)
        {
            if(character.transform.Equals(transform)) continue;
            if (character.TryGetComponent(out Damagable d) && !d.IsPlayer)
            {
                if (d.GetHealthPerent() < lowestHealthValue)
                {
                    lowestHealthValue = d.GetHealthPerent();
                    lowestHealth = d.transform;
                }
            }
        }

        return lowestHealth;
    }
}
