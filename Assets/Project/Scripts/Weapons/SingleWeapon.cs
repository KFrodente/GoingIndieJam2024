using UnityEngine;

public class SingleWeapon : Weapon
{
    public override void StartAttack(Vector2 target)
    {
        base.StartAttack(target);
        if(delayOver) Fire(target);
    }
}