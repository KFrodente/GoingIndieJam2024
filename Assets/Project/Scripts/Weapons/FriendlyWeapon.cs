using UnityEngine;

public class FriendlyWeapon : Weapon
{
    [SerializeField] protected float friendSearchRadius;

    protected override void Fire(Target target)
    {
        target = GetFriendly();
        if(target == null) return;
        lastFireTime = Time.time;
        float angle = InputUtils.GetAngle(target.GetDirection());
        Instantiate(weaponData.projectile, transform.position, Quaternion.Euler(0, 0, angle)).GetComponent<Projectile>().Initialize(target, (int)bc.GetStats().Damage);
        
    }

    protected virtual Target GetFriendly()
    {
        Transform enemy = GetAnEnemyPos();
        if (enemy != null)
        {
            Target t = new Target(TargetType.Position, enemy.position, null, transform.position, true);
            t.uniqueCaseID = TargetCaseID.Friendly;
            return t;
        }
        
        return null;
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
                if (d.GetHealthPercent() < lowestHealthValue)
                {
                    lowestHealthValue = d.GetHealthPercent();
                    lowestHealth = d.transform;
                }
            }
        }

        return lowestHealth;
    }
}