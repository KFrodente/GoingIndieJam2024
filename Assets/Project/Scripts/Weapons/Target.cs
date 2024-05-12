using System;
using UnityEngine;

public class Target
{
    private TargetType type;
    private Vector2? lockedTarget;
    private Transform? characterTarget;
    private Vector2 fireLocation;
    public bool shotByPlayer { get; private set; }
    public TargetCaseID uniqueCaseID = TargetCaseID.None;

    public Target(TargetType type, Vector2? lockedPos, Transform? target, Vector2 fireSpot, bool shotByPlayer)
    {
        this.type = type;
        this.lockedTarget = lockedPos;
        this.characterTarget = target;
        this.fireLocation = fireSpot;
        this.shotByPlayer = shotByPlayer;
    }

    public Vector2 GetDirection()
    {
        return (GetTargetPosition() - fireLocation).normalized;
        
    }

    public Vector2 GetTargetPosition()
    {
        switch (type)
        {
            case TargetType.Mouse:
            {
                return InputUtils.GetMousePosition();
            }
            case TargetType.Position:
            {
                if (lockedTarget == null) throw new NullReferenceException("Target missing a locked position");
                return lockedTarget.Value;
            }
            case TargetType.Character:
            {
                if(characterTarget == null) throw new NullReferenceException("Target missing a character target");
                return characterTarget.position;
            }
            default:
            {
                throw new NullReferenceException("Target missing type");
            }
        }
    }
}


public enum TargetType
{
    Mouse,
    Position,
    Character
}

public enum TargetCaseID
{
    None,
    Neutral,
    Friendly,
    Enemy
}