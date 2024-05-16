using UnityEngine;

public class BossCharacter : BaseCharacter
{
    public virtual Stats.Stats GetStats()
    {
        return characterStats.baseStats;
    }
    protected override void Awake()
    {
        weapon.InitializeCharacter(this);
    }

    
    protected override void Reposition(Vector2 normalizedDirection)
    {
        
    }

    protected override void Update()
    {
        Attack(new Target(TargetType.Character, null, playerCharacter.transform, transform, false));
        Debug.Log("Attacking");
    }

    protected override void xFlipping()
    {
        
    }


    protected override void FixedUpdate()
    {
        
    }

    
}
