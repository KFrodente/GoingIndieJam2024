using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DauntlessCharacter : BaseCharacter
{
    public override void Attack(Vector2 target)
    {
        weapon.StartAttack(target);
    }

    public override void Reposition(Vector2 direction)
    {
        movement.Move(direction);
    }
}
