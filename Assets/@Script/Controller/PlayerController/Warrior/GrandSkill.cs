using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (!_skillDataDic.ContainsKey(Define.SkillType.Skill2))
            return;
        SkillData data = _skillDataDic[Define.SkillType.Skill2];
        if (!Manager.Skill._skillDic.ContainsKey(_hero) || !skill2 || !CheckMp(data))
            return;

        skill2 = false;

        Debug.Log("skill2");
    }

    public override void Skill3()
    {
        if (!_skillDataDic.ContainsKey(Define.SkillType.Skill3))
            return;
        SkillData data = _skillDataDic[Define.SkillType.Skill3];
        if (!Manager.Skill._skillDic.ContainsKey(_hero) || !skill3 || !CheckMp(data))
            return;

        skill3 = false;

        Debug.Log("skill3");
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
