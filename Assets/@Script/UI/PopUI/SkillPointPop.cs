using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointPop : UI_Pop
{
    enum Images
    {
        SkillImage,
    }
    enum Texts
    {
        SkillName,
        Damage_Txt,
        Mp_Txt,
        Target_Txt,
        Arange_Txt,
        CoolTime_Txt,
        Point_Txt,
    }
    enum Buttons
    {
        Close_Btn,
        Point_Btn,
    }
    private Define.SkillType _type;
    private SkillData _data;
    private int _level;
    private Skill _skill;
    private SkillImageFragment _imageFramgnet;
    protected override bool Init()
    {
        if(base.Init() == false)    
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        ResetState();
        BindEvent(GetButton((int)Buttons.Point_Btn).gameObject, ClickPoint);
        BindEvent(GetButton((int)Buttons.Close_Btn).gameObject, () => { Manager.UI.ClosePopUI(this); });

        return true;
    }

    public void SetInfo(Define.SkillType type, int level, SkillData data, Skill skill, SkillImageFragment fa)
    {
        _type = type;
        _data = data;
        _level = level;
        _skill = skill;
        _imageFramgnet = fa;
    }

    private void ClickPoint()
    {
        _skill.SkillLevelUp(_type, _level, _data);
        _imageFramgnet.Refresh();

        MainCanvas main = Manager.UI.SceneUI as MainCanvas;
        Debug.Log(main);
        main.SkillIcon();
    }

    private void ResetState()
    {
        GetImage((int)Images.SkillImage).sprite = _data.Image;

        GetText((int)Texts.SkillName).text = $"{_data.SkillName}";
        GetText((int)Texts.Damage_Txt).text = $"Damage: {_data.Damage}%";
        GetText((int)Texts.Mp_Txt).text = $"Mp: {_data.Mp}";
        GetText((int)Texts.Target_Txt).text = $"Target: {_data.Target}";
        GetText((int)Texts.Arange_Txt).text = $"Arange: {_data.SkillArange}";
        GetText((int)Texts.CoolTime_Txt).text = $"CoolTime: {_data.CoolTime}";
        GetText((int)Texts.Point_Txt).text = $"Point: {_data.SkillPoint}";
    }
}
