using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AutoPlayerController : PlayerController
{
    public Coroutine _cor;
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        State = Define.State.Idle;
        return true;
    }
   
    protected override void UpdateMethod()
    {
        Debug.LogWarning(State);
        base.UpdateMethod();
        SearchMonster();
    }
    private void SearchMonster()
    {
        if(State != Define.State.Attack && monster != null)
            return;

        float minValue = float.MaxValue;
        foreach (var m in Manager.Monster._monList)
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
        Manager.Monster._monList.Remove(monster);
        this.monster = null;
    }

    public IEnumerator AutoSkill()
    {
        var types = new Define.SkillType[4] { Define.SkillType.Skill1, Define.SkillType.Skill2, Define.SkillType.Skill3, Define.SkillType.Skill4 };
        while (true)
        {
            while (State != Define.State.Attack)
                yield return null;

            
            yield return new WaitForSeconds(4f);
            Debug.Log("여기로와");
            int randValue = UnityEngine.Random.Range(0, 4);
            int test = 0;
            Debug.Log(randValue);
            _skill._skillDic[types[test]]?.Invoke();
        }
    }

    private void OnEnable()
    {
        if (_skill == null)
            _skill = transform.Find("SkillAnim").GetComponent<Skill>();

        _skill.SetPlayer(this);
    }
    public void StartAutoSkill()
    {
        _cor = StartCoroutine(AutoSkill());
    }
    public void StopAutoSkill()
    {
        if(_cor == null)
            return;
        StopCoroutine(_cor);
    }
}
