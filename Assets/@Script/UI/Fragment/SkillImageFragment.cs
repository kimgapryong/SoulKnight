using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillImageFragment : UI_Base
{
   enum Images
    {
        SkillImage,
    }
    enum Texts
    {
        Level_Txt,
    }

    private Define.SkillType _type;
    private SkillData _data;
    private int _level;
    private Skill _skill;
    protected override bool Init()
    {
        if(base.Init() == false) 
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        Refresh();

        BindEvent(gameObject, SkillUpgrade);
        return true;
    }

    public void SetInfo(Define.SkillType type,int level, SkillData data, Skill skill)
    {
        _type = type;
        _data = data;
        _level = level;
        _skill = skill;
    }

    public void Refresh()
    {
        bool[] array = _skill._skilCheckDic[_type];
        if (array[_level])
            GetComponent<Image>().color = Color.green;

        GetImage((int)Images.SkillImage).sprite = _data.Image;

        GetText((int)Texts.Level_Txt).text = $"LEVEL:{_level + 1}"; // 숫자가 0부터 시작임
    }

    private void SkillUpgrade()
    {
        Manager.UI.ShowPopUI<SkillPointPop>(callback: (pop) =>
        {
            pop.SetInfo(_type, _level, _data,_skill, this);
        });
        /*_skill.SkillLevelUp(_type, _level, _data);
        Refresh();*/
    }
}
