using UnityEngine;

public class BurstWeapon : Weapon
{
    protected bool attacking;
    public override void StartAttack(Vector2 target)
    {
        attacking = true;
    }
    public override void EndAttack(Vector2 target)
    {
        base.EndAttack(target);
        attacking = false;
    }

    protected void Update()
    {
        if (attacking)
        {
            if (delayOver)
            {
                Fire(GetMousePosition());
                lastFireTime = Time.time;
            }
        }
    }
    
}