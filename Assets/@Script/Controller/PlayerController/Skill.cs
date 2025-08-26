using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : BaseController
{
    public Define.HeroName _hero;
    public SkillValue[] _data;

    public Dictionary<Define.SkillType, Action> _skillDic = new Dictionary<Define.SkillType, Action>(); //스킬 타입의 매핑
    public Dictionary<Define.SkillType, SkillData> _skillDataDic = new Dictionary<Define.SkillType, SkillData>();

    public PlayerController _player;
    private Animator anim;

    protected bool skill1 = true;  // 스킬 쿨 확인
    protected bool skill2 = true;
    protected bool skill3 = true;
    protected bool skill4 = true;

    private Define.SkillType _type;
    public Define.SkillType Type
    {
        get { return _type; }
        set
        {
            _type = value;
            SkillAnim(value);
        }
    }

    protected override bool Init()
    {
        if(base.Init() == false) 
            return false;

        anim = GetComponent<Animator>();
        Manager.Skill.SetSkillDic(_hero, this); //히어로의 스킬 등록
        SetData();

        _skillDataDic[Define.SkillType.Skill1] = _data[0].Datas[0];

        //처음에는 그냥 플레이어
        _player = transform.parent.GetComponent<PlayerController>();

        return true;
    }
    protected virtual void SkillAnim(Define.SkillType type) { }

    protected virtual void SetData()
    {
        foreach (var item in _data)
        {
            switch (item.Type)
            {
                case Define.SkillType.Skill1:
                    _skillDic.Add(item.Type, Skill1);
                    break;
                case Define.SkillType.Skill2:
                    _skillDic.Add(item.Type, Skill2);
                    break;
                case Define.SkillType.Skill3:
                    _skillDic.Add(item.Type, Skill3);
                    break;
                case Define.SkillType.Skill4:
                    _skillDic.Add(item.Type, Skill4);
                    break;
         
            }
        }
    }
    public abstract void Skill1();
    public abstract void Skill2();
    public abstract void Skill3();
    public abstract void Skill4();

    protected virtual void SkillUpgrade(Define.SkillType type, SkillData data)
    {
        _skillDataDic[type] = data;
    }

    public float GetClipLength(Animator anim,string name)
    {
        foreach (var c in anim.runtimeAnimatorController.animationClips)
            if (c != null && c.name == name)
                return c.length;
        return 0f; 
    }

    protected virtual float GetDamage(float damage)
    {
        return _player._status.Damage * damage / 100;
    }
    protected IEnumerator WaitCool(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }
    //  마나 체크
    protected bool CheckMp(SkillData data)
    {
        if (!(_player._status is PlayerStatus ps)) return false;

        if (ps.CurMp <= 0f || ps.CurMp < data.Mp) return false;

        // 소모(속성으로 깎아야 mpAction 이벤트가 정상 동작)
        ps.CurMp = Mathf.Max(0f, ps.CurMp - data.Mp);
        return true;
    }
   
  
    
    public void SetPlayer(PlayerController player)
    {
        _player = player;
    }
}
[Serializable]
public class SkillValue
{
    public Define.SkillType Type;
    public List<SkillData> Datas;
}