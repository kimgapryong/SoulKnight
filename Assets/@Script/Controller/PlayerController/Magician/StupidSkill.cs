using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidSkill : Skill
{
    protected override void SkillAnim(Define.SkillType type)
    {
        switch (type)
        {
            case Define.SkillType.Skill1:
                {
                    anim.Play("Skill_Pre");
                    float time = GetClipLength(anim, "Skill_Pre");
                    StartCoroutine(WaitCool(time, () => { anim.Play("NoneSkill"); }));
                }
                break;
            case Define.SkillType.Skill2:
                {
                    anim.Play("Skill_Pre");
                    float time = GetClipLength(anim, "Skill_Pre");
                    StartCoroutine(WaitCool(time, () => { anim.Play("NoneSkill"); }));
                }
                break;
            case Define.SkillType.Skill3:
                {
                    anim.Play("Skill_Pre");
                    float time = GetClipLength(anim, "Skill_Pre");
                    StartCoroutine(WaitCool(time, () => { anim.Play("NoneSkill"); }));
                }
                break;
            case Define.SkillType.Skill4:
                {
                    anim.Play("Skill_Pre");
                    float time = GetClipLength(anim, "Skill_Pre");
                    StartCoroutine(WaitCool(time, () => { anim.Play("NoneSkill"); }));
                }
                break;
        }
    }
    public override void Skill1()
    {
        if (!_skillDataDic.ContainsKey(Define.SkillType.Skill1))
            return;

        SkillData data = _skillDataDic[Define.SkillType.Skill1];

        if (!Manager.Skill._skillDic.ContainsKey(_hero) || !skill1 || !CheckMp(data))
            return;
        
        List<MonsterController>  target = Manager.Monster.SearchMonster(transform.parent, data.SkillArange);

        Type = Define.SkillType.Skill1;
        skill1 = false;

        float animTime = GetClipLength(anim, "Skill_Pre");
        StartCoroutine(WaitCool(animTime, () =>
        {
            foreach (MonsterController t in target)
            {
                GameObject obj = Manager.Resources.Instantiate("Skills/ReMagic", t.transform.position, Quaternion.identity);
                Animator anim = obj.GetComponent<Animator>();

                anim.Play("ReMagic");
                float time = GetClipLength(anim, "ReMagic");

                //애니메이션 끝나고 공격
                StartCoroutine(WaitCool(time, () =>
                {
                    t.OnDamage(_player, GetDamage(data.Damage)); // 적 공격함
                    Destroy(obj);
                }));
            }
            StartCoroutine(WaitCool(data.CoolTime, () => { skill1 = true; })); // 플레이어의 스킬 쿨 초기화
        }));

       
    }

    public override void Skill2()
    {
        if (!_skillDataDic.ContainsKey(Define.SkillType.Skill2))
            return;

        SkillData data = _skillDataDic[Define.SkillType.Skill2];

        if (!Manager.Skill._skillDic.ContainsKey(_hero) || !skill2 || !CheckMp(data))
            return;

        MonsterController target = _player._status.monster;

        if (target == null)
            target = Manager.Monster.SearchMonster(transform.parent, data.SkillArange, data.Target)[0];

        Type = Define.SkillType.Skill2;
        skill2 = false;

        GameObject obj = Manager.Resources.Instantiate("Skills/MagicSkill", transform.position, Quaternion.identity);
        Vector2 dir = (target.transform.position - obj.transform.position).normalized;
        ProjectileController pre = obj.AddComponent<ProjectileController>();
        pre.SetInfo(_player, dir, GetDamage(data.Damage), 12);
        pre.ChainAttack(data.Target);

        StartCoroutine(WaitCool(data.CoolTime, () => { skill2 = true; })); // 플레이어의 스킬 쿨 초기화
    }

    public override void Skill3()
    {
        if (!_skillDataDic.ContainsKey(Define.SkillType.Skill3))
            return;

        SkillData data = _skillDataDic[Define.SkillType.Skill3];

        if (!Manager.Skill._skillDic.ContainsKey(_hero) || !skill3 || !CheckMp(data))
            return;

        Type = Define.SkillType.Skill3;
        skill3 = false;

        List<PlayerController> list = Manager.Character.SearchPlayer(transform, data.SkillArange);
        foreach (var player in list)
        {
            player.Heal(GetDamage(data.Damage));
        }
        StartCoroutine(WaitCool(data.CoolTime, () => { skill3 = true; })); // 플레이어의 스킬 쿨 초기화
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

        Type = Define.SkillType.Skill4;
        skill4 = false;

        target.Poision(data.SkillTime, data.DotDamage, data.DotSpeed);

        StartCoroutine(WaitCool(data.CoolTime, () => { skill4 = true; })); // 플레이어의 스킬 쿨 초기화
    }
}
