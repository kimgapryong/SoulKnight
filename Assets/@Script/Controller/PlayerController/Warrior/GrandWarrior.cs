using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandWarrior : PlayerController
{
    public override void NormalAttack()
    {
        _status.monster.OnDamage(this, 1);
    }
}
