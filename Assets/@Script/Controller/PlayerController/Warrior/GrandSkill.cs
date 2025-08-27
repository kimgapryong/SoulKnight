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

        //���� ���� Ž��
        Debug.Log(_player);
        Debug.Log(_player._status.monster);
        
        GameObject obj = Manager.Resources.Instantiate("Skills/Slash", target.transform.position, Quaternion.identity);
        Animator anim = obj.GetComponent<Animator>();

        anim.Play("Slash");
        float time = GetClipLength(anim, "Slash");

        //�ִϸ��̼� ������ ����
        StartCoroutine(WaitCool(time, () =>
        {
            target.OnDamage(_player,GetDamage(data.Damage)); // �� ������
            StartCoroutine(WaitCool(data.CoolTime, () => { skill1 = true; })); // �÷��̾��� ��ų �� �ʱ�ȭ
            Destroy(obj);
        }));
        
    }

    public override void Skill2()
    {
        Debug.Log("��ų2");
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
        var t = target;                  // ���� Ÿ�� ĸó
        if (!t) yield break;

        for (int i = 0; i < data.Again; i++)
        {
            var obj = Manager.Resources.Instantiate("Skills/Slash2", t.transform.position, Quaternion.identity);
            // obj.transform.SetParent(t.transform, true); // Ÿ�� �����Ϸ��� Ȱ��ȭ
            var anim = obj.GetComponent<Animator>();
            if (anim) anim.Play("Slash2");

            float time = anim ? GetClipLength(anim, "Slash2") : 0f; // �ʿ� �� / anim.speed

            // ����Ʈ ��� ���
            yield return new WaitForSeconds(time);

            if (t) t.OnDamage(_player, GetDamage(data.Damage));
            if (obj) Destroy(obj);
        }

        // �޺� ���� �� ��ٿ�
        yield return new WaitForSeconds(data.CoolTime);
        skill2 = true;
    }
    public override void Skill3()
    {
        Debug.Log("��ų3");
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

        //�ִϸ��̼� ������ ����
        Vector2 dir = (target.transform.position - obj.transform.position).normalized;
        ProjectileController pre = obj.AddComponent<ProjectileController>();
        pre.SetInfo(_player, dir, data.Damage, 10, true, time);

        StartCoroutine(WaitCool(data.CoolTime, () => { skill3 = true; })); // �÷��̾��� ��ų �� �ʱ�ȭ
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
