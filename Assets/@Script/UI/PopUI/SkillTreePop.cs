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
    enum Buttons
    {
        Close_Btn,
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
        BindButton(typeof(Buttons));

        Refresh(_status.SkillPoint);
        _status.pointAction = Refresh;

        BindEvent(GetButton((int)Buttons.Close_Btn).gameObject, () => { ClosePopupUI(); });

        foreach (var value in _skill._data)
        {
            Manager.UI.MakeSubItem<SkillTreeFragment>(GetObject((int)Objects.SkillContent).transform, callback: (fa) =>
            {
                fa.SetInfo(value.Type, value.Datas, _skill);
            });
        }
        return true;
    }
    public void SetInfo(PlayerController player)
    {
        _player = player;
        _status = player._status;
        _skill = player._skill;
    }

    public void Refresh(int point)
    {
        GetText((int)Texts.Point_Txt).text = $"SkillPoint: {point}";
    }
}
