using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item_Base
{
    public override void ItemAbilty()
    {
        Manager.Player._status.CurHp += Manager.Player._status.Hp * (precent / 100);
    }
}
