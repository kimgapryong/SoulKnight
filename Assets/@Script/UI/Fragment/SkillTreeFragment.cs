using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeFragment : UI_Base
{
    private Skill _skill;
    private Define.SkillType _type;
    private SkillData[] _data;

    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        for(int i = 0; i < _data.Length; i++)
        {
            Manager.UI.MakeSubItem<SkillImageFramgnet>(transform, callback: (fa) =>
            {
                fa.SetInfo(_type,i, _data[i], _skill);
            });
        }

        return true;
    }
    public void SetInfo(Define.SkillType type, SkillData[] data, Skill skill)
    {
        _data = data;
        _type = type;
        _skill = skill;
    }
   
}
