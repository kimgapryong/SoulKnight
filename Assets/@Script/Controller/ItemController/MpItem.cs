using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpItem : Item_Base
{
    public override void ItemAbilty()
    {
        Manager.Player._status.CurMp += Manager.Player._status.Mp * (precent / 100);
    }
}
