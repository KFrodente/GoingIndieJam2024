using UnityEngine;

public class ToPlayerWeapon : Weapon
{

    protected override void Fire(Target target)
    {
        if(weaponData.attackSound && AudioManager.instance) AudioManager.instance.Play(weaponData.attackSound);
        target = new Target(TargetType.Character, null, BaseCharacter.playerCharacter.transform, transform, false);
        lastFireTime = Time.time;
        float angle = InputUtils.GetAngle(target.GetDirection());
        Instantiate(weaponData.projectile, transform.position, Quaternion.Euler(0, 0, angle)).GetComponent<Projectile>().Initialize(target, (int)bc.GetStats().Damage);
        
    }

    
}