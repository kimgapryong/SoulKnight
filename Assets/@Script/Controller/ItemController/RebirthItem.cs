using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebirthItem : Item_Base
{
    public override void ItemAbilty()
    {
        foreach(var pla in Manager.Character.playerList)
        {
            if(pla._status.CurHp <= 0)
            {
                pla._status.Death = false;
                pla._status.CurHp = pla._status.Hp;
                return;
            }
        }
    }
}
