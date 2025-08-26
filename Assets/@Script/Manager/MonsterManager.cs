using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager 
{
    public List<MonsterController> _monList = new List<MonsterController>();

    public List<MonsterController> SearchMonster(Transform pos,float arange, int count)
    {
        List<MonsterController> monList = new List<MonsterController>();
        foreach (MonsterController mon in _monList)
        {
            if(Vector2.Distance(pos.position, mon.transform.position) > arange)
                continue;

            monList.Add(mon);

            if(monList.Count >= count)
                return monList;
        }

        return monList;
    }
}
