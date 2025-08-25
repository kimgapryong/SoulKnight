using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayerController : PlayerController
{

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        State = Define.State.Idle;
        return true;
    }
   
    protected override void UpdateMethod()
    {
        base.UpdateMethod();
        SearchMonster();
    }
    private void SearchMonster()
    {
        if(State != Define.State.Attack && monster != null)
            return;

        float minValue = float.MaxValue;
        foreach (var m in Manager.Character._monList)
        {
            float curValue = (m.transform.position - transform.position).magnitude;
            if(minValue > curValue)
            {
                minValue = curValue;
                monster = m;
            }
        }
    }

    public void AutoReset(MonsterController monster)
    {
        Manager.Character._monList.Remove(monster);
        this.monster = null;
    }
}
