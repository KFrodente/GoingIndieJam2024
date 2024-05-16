using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetheredDamagable : Damagable
{
    [SerializeField] private GameObject tetheredDeadBody;
    public override void Die(bool suicide = false)
    {
        if (!suicide && dispenser != null) dispenser.Dispense();
        if (IsPlayer && baseCharacter.possessingSpirit != null)
        {
            baseCharacter.possessingSpirit.Reliquish();
            if (baseCharacter.possessingSpirit.TryGetComponent(out Damagable d))
            {
                d.GainImmunity(1f);
            }
            if (baseCharacter.possessingSpirit.TryGetComponent(out Rigidbody2D rb))
            {
                rb.AddForce(baseCharacter.possessingSpirit.transform.up * 1000, ForceMode2D.Force);
            }
        }
        tetheredDeadBody.transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, 0);
        tetheredDeadBody.SetActive(true);
        gameObject.SetActive(false);
    }
}
