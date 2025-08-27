using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreePop : UI_Pop
{
    enum Texts
    {
        Point_Txt
    }
    enum Objects
    {
        SkillContent,
    }

    private PlayerController _player;
    private PlayerStatus _status;
    private Skill _skill;

    protected override bool Init()
    {
        if(base.Init() == false)    
            return false;

        BindText(typeof(Texts));
        BindObject(typeof(Objects));

        return true;
    }
    public void SetInfo(PlayerController player)
    {
        _player = player;
        _status = player._status;
        _skill = player._skill;
    }

    public void Refresh()
    {
        GetText((int)Texts.Point_Txt).text = $"SkillPoint: {_status.SkillPoint}";

        foreach(var value in _skill._data)
        {

        }
    }
}
