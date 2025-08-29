using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileMonster : MonsterController
{
    protected override void Attack()
    {
        atkCool = true;
        State = Define.State.Idle;

        GameObject obj = Manager.Resources.Instantiate($"Projectile/{data.Path}", transform.position, Quaternion.identity);

        if(target == null)
        {
            StartCoroutine(WaitTime(_status.AtkSpeed, () => atkCool = false));
            return;
        }
            

        Vector2 dir = (target.transform.position - obj.transform.position).normalized;
        ProjectileController pre = obj.AddComponent<ProjectileController>();
        pre.SetInfo(this, dir, _status.Damage, 10);

        StartCoroutine(WaitTime(_status.AtkSpeed, () => atkCool = false));
    }
}
