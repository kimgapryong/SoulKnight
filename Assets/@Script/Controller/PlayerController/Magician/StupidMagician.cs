using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidMagician : PlayerController
{
    public override void NormalAttack()
    {
        GameObject obj = Manager.Resources.Instantiate("Projectile/StupidMagic", transform.position, Quaternion.identity);
        Vector2 dir = (_status.monster.transform.position - obj.transform.position).normalized;
        ProjectileController pre = obj.AddComponent<ProjectileController>();
        pre.SetInfo(this, dir, _status.Damage , 5);
    }
}
