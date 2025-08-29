using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterManager 
{
    public List<MonsterController> _monList = new List<MonsterController>();

    public List<MonsterController> SearchMonster(Transform pos,float arange, int count = 0)
    {
        List<MonsterController> monList = new List<MonsterController>();
        foreach (MonsterController mon in _monList)
        {
            if(Vector2.Distance(pos.position, mon.transform.position) > arange)
                continue;

            monList.Add(mon);

            if(count != 0)
                if(monList.Count >= count)
                    return monList;
        }

        return monList;
    }

    public MonsterController ChainMonster(MonsterController curMonster)
    {
        MonsterController curMon = null;
        float minValue = float.MaxValue;

        foreach (MonsterController mon in _monList)
        {
            float length = (curMonster.transform.position - mon.transform.position).magnitude;
            if (minValue > length)
            {
                curMon = mon;
                minValue = length;
            }
        }
        return curMon;
    }
}
