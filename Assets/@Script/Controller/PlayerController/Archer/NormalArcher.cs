using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalArcher : PlayerController
{
    protected override bool Init()
    {
        if(base.Init() == false )
            return false;
        GetComponent<AutoPlayerController>().atkAction -= NormalAttack;
        GetComponent<AutoPlayerController>().atkAction += NormalAttack;
        return true;
    }
    protected override void NormalAttack()
    {
        GameObject obj = Manager.Resources.Instantiate("Projectile/NormalArrow", transform.position, Quaternion.identity);
        Vector2 dir = (monster.transform.position - obj.transform.position).normalized;
        ProjectileController pre =  obj.AddComponent<ProjectileController>();
        pre.SetInfo(this, dir, _status.Damage);
    }
}
