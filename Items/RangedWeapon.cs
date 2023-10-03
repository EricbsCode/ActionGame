using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Item
{
    public GameObject projectile;

    public override void Use()
    {
        base.Use();
        if(inventory.transform.localScale.x == 1)
            Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, -90));
        if(inventory.transform.localScale.x == -1)
            Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, 90));
    }

    public override void Remove()
    {
        base.Remove();
    }
}
