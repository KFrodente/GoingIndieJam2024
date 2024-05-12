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
            }
        }
        if (BaseCharacter.playerCharacter.transform == null) return null;
        t = new Target(TargetType.Character, null, BaseCharacter.playerCharacter.transform, transform.position, false);
        
        return t;
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
