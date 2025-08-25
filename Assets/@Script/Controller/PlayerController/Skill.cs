using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : BaseController
{
    public SkillValue[] _data;
    protected Dictionary<Define.SkillType, Action> _skillDic = new Dictionary<Define.SkillType, Action>();
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

                    break;
                case Define.SkillType.Skill3:

                    break;
                case Define.SkillType.Skill4:

                    break;
                case Define.SkillType.Skill5:

                    break;
            }
        }
    }
    public abstract void Skill1();
    public abstract void Skill2();
    public abstract void Skill3();
    public abstract void Skill4();
    public abstract void Skill5();

}
[Serializable]
public class SkillValue
{
    public Define.SkillType Type;
    public List<SkillData[]> Datas;
}