using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GrandSkill : Skill
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

        //단일 몬스터 탐색
        Debug.Log(_player);
        Debug.Log(_player._status.monster);
        
        GameObject obj = Manager.Resources.Instantiate("Skills/Slash", target.transform.position, Quaternion.identity);
        Animator anim = obj.GetComponent<Animator>();

        anim.Play("Slash");
        float time = GetClipLength(anim, "Slash");

        //애니메이션 끝나고 공격
        StartCoroutine(WaitCool(time, () =>
        {
            target.OnDamage(_player,GetDamage(data.Damage)); // 적 공격함
            StartCoroutine(WaitCool(data.CoolTime, () => { skill1 = true; })); // 플레이어의 스킬 쿨 초기화
            Destroy(obj);
        }));
        
    }

    public override void Skill2()
    {
        Debug.Log("스킬2");
        if (!_skillDataDic.ContainsKey(Define.SkillType.Skill2))
            return;
        SkillData data = _skillDataDic[Define.SkillType.Skill2];
        if (!Manager.Skill._skillDic.ContainsKey(_hero) || !skill2 || !CheckMp(data))
            return;

        MonsterController target = _player._status.monster;

        if (target == null)
            target = Manager.Monster.SearchMonster(transform.parent, data.SkillArange, data.Target)[0];

        skill2 = false;

        StartCoroutine(Combo(target, data));
    }
    IEnumerator Combo(MonsterController target, SkillData data)
    {
        var t = target;                  // 현재 타깃 캡처
        if (!t) yield break;

        for (int i = 0; i < data.Again; i++)
        {
            var obj = Manager.Resources.Instantiate("Skills/Slash2", t.transform.position, Quaternion.identity);
            // obj.transform.SetParent(t.transform, true); // 타깃 추적하려면 활성화
            var anim = obj.GetComponent<Animator>();
            if (anim) anim.Play("Slash2");

            float time = anim ? GetClipLength(anim, "Slash2") : 0f; // 필요 시 / anim.speed

            // 이펙트 재생 대기
            yield return new WaitForSeconds(time);

            if (t) t.OnDamage(_player, GetDamage(data.Damage));
            if (obj) Destroy(obj);
        }

        // 콤보 끝난 후 쿨다운
        yield return new WaitForSeconds(data.CoolTime);
        skill2 = true;
    }
    public override void Skill3()
    {
        Debug.Log("스킬3");
        if (!_skillDataDic.ContainsKey(Define.SkillType.Skill3))
            return;
        SkillData data = _skillDataDic[Define.SkillType.Skill3];
        if (!Manager.Skill._skillDic.ContainsKey(_hero) || !skill3 || !CheckMp(data))
            return;

        MonsterController target = _player._status.monster;

        if (target == null)
            target = Manager.Monster.SearchMonster(transform.parent, data.SkillArange, data.Target)[0];


        skill3 = false;

        GameObject obj = Manager.Resources.Instantiate("Skills/Slash3", transform.position, Quaternion.identity);
        Animator anim = obj.GetComponent<Animator>();
        anim.Play("Slash3");
        float time = GetClipLength(anim, "Slash3");

        //애니메이션 끝나고 공격
        Vector2 dir = (target.transform.position - obj.transform.position).normalized;
        ProjectileController pre = obj.AddComponent<ProjectileController>();
        pre.SetInfo(_player, dir, data.Damage, 10, true, time);

        StartCoroutine(WaitCool(data.CoolTime, () => { skill3 = true; })); // 플레이어의 스킬 쿨 초기화
    }

    public override void Skill4()
    {
        if (!_skillDataDic.ContainsKey(Define.SkillType.Skill4))
            return;

        SkillData data = _skillDataDic[Define.SkillType.Skill4];
        if (!Manager.Skill._skillDic.ContainsKey(_hero) || !skill4 || !CheckMp(data))
            return;

        skill4 = false;

        Debug.Log("skill4");
    }
 
    
}
