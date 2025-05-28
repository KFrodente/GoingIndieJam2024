using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField]private List<GameObject> pool = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void CreateBullet(WeaponObjectData weaponData, Vector3 position, Quaternion rotation, Target target, BaseCharacter bc)
    {
        GameObject found = null;
        foreach (GameObject p in pool)
        {
            if (p.GetComponent<Projectile>().available)
            {
                found = p;
                break;
            }
        }
        if (found != null)
        {
            Projectile p = found.GetComponent<Projectile>();
            p.setAvailability(false);
            p.Initialize(target, weaponData.projectile.damage);
            p.transform.position = position;
            p.transform.rotation = rotation;
        }
        else
        {
            Projectile spawned = Instantiate(weaponData.projectile, transform.position, rotation);
            spawned.setAvailability(false);
            spawned.Initialize(target, (int)bc.GetStats().Damage);
            if (weaponData.attackSound && AudioManager.instance) AudioManager.instance.Play(weaponData.attackSound);
            pool.Add(spawned.gameObject);
        }
    }

}
