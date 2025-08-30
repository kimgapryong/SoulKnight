using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSkill : Skill
{
    public override void Skill1()
    {
        if (!_skillDataDic.ContainsKey(Define.SkillType.Skill1))
            return;

        SkillData data = _skillDataDic[Define.SkillType.Skill1];

        if (!Manager.Skill._skillDic.ContainsKey(_hero) || !skill1 || !CheckMp(data))
            return;

        MonsterController target = _player._status.monster;

        if (target == null)
            target = Manager.Monster.SearchMonster(transform.parent, data.SkillArange, data.Target)[0];

        skill1 = false;

        StartCoroutine(Combo(target, data));
    }
    IEnumerator Combo(MonsterController target, SkillData data)
    {
        var t = target;                  // ÇöÀç Å¸±ê Ä¸Ã³
        if (!t) yield break;

        for (int i = 0; i < data.Again; i++)
        {
            var obj = Manager.Resources.Instantiate("Skills/Arrow", transform.position, Quaternion.identity);
            Vector2 dir = (t.transform.position - obj.transform.position).normalized;
            ProjectileController pre = obj.AddComponent<ProjectileController>();
            pre.SetInfo(_player, dir, GetDamage(data.Damage), 17);

            yield return new WaitForSeconds(0.1f);
        }

        if(itemTime)
            yield return new WaitForSeconds(1);
        else
            yield return new WaitForSeconds(data.CoolTime);
        skill1 = true;
    }
    public override void Skill2()
    {

        if (!_skillDataDic.ContainsKey(Define.SkillType.Skill2))
            return;

        SkillData data = _skillDataDic[Define.SkillType.Skill2];
        if (!Manager.Skill._skillDic.ContainsKey(_hero) || !skill2 || !CheckMp(data))
            return;

        List<MonsterController> target = Manager.Monster.SearchMonster(transform.parent, data.SkillArange, data.Target);

        skill2 = false;

        foreach (MonsterController t in target)
        {
            var obj = Manager.Resources.Instantiate("Skills/Arrow", transform.position, Quaternion.identity);
            Vector2 dir = (t.transform.position - obj.transform.position).normalized;
            ProjectileController pre = obj.AddComponent<ProjectileController>();
            pre.SetInfo(_player, dir, GetDamage(data.Damage), 10);
        }
        if(itemTime)
            StartCoroutine(WaitCool(1f, () => { skill2 = true; }));
        else
            StartCoroutine(WaitCool(data.CoolTime, () => { skill2 = true; }));
    }

    public override void Skill3()
    {
        if (!_skillDataDic.ContainsKey(Define.SkillType.Skill3))
            return;

        SkillData data = _skillDataDic[Define.SkillType.Skill3];

        if (!Manager.Skill._skillDic.ContainsKey(_hero) || !skill3 || !CheckMp(data))
            return;

        MonsterController target = _player._status.monster;

        if (target == null)
            target = Manager.Monster.SearchMonster(transform.parent, data.SkillArange, data.Target)[0];

        skill3 = false;

        Vector2 baseDir = (target.transform.position - transform.position).normalized;
        for (int i = 0; i < data.Again; i++)
        {
            float offsetIndex = i - (data.Again - 1) * 0.5f;     
            float angle = offsetIndex * 10;     

            Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, angle) * (Vector3)baseDir);
            
            var obj = Manager.Resources.Instantiate("Skills/Arrow", transform.position, Quaternion.identity);
            var pre = obj.AddComponent<ProjectileController>();
            pre.SetInfo(_player, dir, GetDamage(data.Damage), 12f,true, 6);
        }
        if (itemTime)
            StartCoroutine(WaitCool(1f, () => { skill3 = true; }));
        else
            StartCoroutine(WaitCool(data.CoolTime, () => { skill3 = true; }));
    }

    public override void Skill4()
    {
        if (!_skillDataDic.ContainsKey(Define.SkillType.Skill4))
            return;

        SkillData data = _skillDataDic[Define.SkillType.Skill4];

        if (!Manager.Skill._skillDic.ContainsKey(_hero) || !skill4 || !CheckMp(data))
            return;

        MonsterController target = _player._status.monster;

        if (target == null)
            target = Manager.Monster.SearchMonster(transform.parent, data.SkillArange, data.Target)[0];

        skill4 = false; ;

        Debug.Log("È­»ìºñ");
        var obj = Manager.Resources.Instantiate("Skills/ArrowRain", (Vector2)target.transform.position + (Vector2.up * 5f), Quaternion.identity);
        Vector2 dir = Vector2.zero;
        ProjectileController pre = obj.AddComponent<ProjectileController>();
        pre.SetInfo(_player, dir, GetDamage(data.Damage), 0,true, 0.7f);

        Debug.Log("È­»ìºñ¤Óº÷ºñ");
        if (itemTime)
            StartCoroutine(WaitCool(1f, () => { skill4 = true; }));
        else
            StartCoroutine(WaitCool(data.CoolTime, () => { skill4 = true; }));

    }

}
