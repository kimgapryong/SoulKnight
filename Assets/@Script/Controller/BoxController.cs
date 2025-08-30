using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonsterController
{
   public List<GameObject> itemList = new List<GameObject>();
    public float precent;
    protected override bool Init()
    {
        if (!_first)
        {
            _first = true;
            return true;
        }

        State = Define.State.Idle;
        return false;
    }

    public override void OnDamage(CreatureController attker, float damage)
    {
        if(!Manager.Random.RollPercent(precent))
            Destroy(gameObject);

        int rand = Random.Range(0, itemList.Count);
        Object.Instantiate(itemList[rand], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    protected override void UpdateMethod() { }  // 아무 것도 안 함
    protected override void Move() { }          // 아무 것도 안 함
    protected override void Idle() { }          // 아무 것도 안 함
    protected override void Attack() { }        // 아무 것도 안 함
}
