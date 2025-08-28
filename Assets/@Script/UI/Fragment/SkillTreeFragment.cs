using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeFragment : UI_Base
{
    private Skill _skill;
    private Define.SkillType _type;
    private List<SkillData> _data;

    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        for(int i = 0; i < _data.Count; i++)
        {
            Manager.UI.MakeSubItem<SkillImageFragment>(transform, callback: (fa) =>
            {
                fa.SetInfo(_type,i, _data[i], _skill);
            });
        }

        return true;
    }
    public void SetInfo(Define.SkillType type, List<SkillData> data, Skill skill)
    {
        _data = data;
        _type = type;
        _skill = skill;
    }
   
}
