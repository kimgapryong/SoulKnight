using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSkillFragment : UI_Base
{
   enum Images
    {
        Skill_Image,
    }
    enum Texts
    {
        Skill_Txt,
    }
    public Define.SkillType type;
    protected override bool Init()
    {
        if(base.Init() == false )
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        Refresh();
        return true;
    }

    public void Refresh()
    {
        Skill curSkill = Manager.Player._skill;
        Debug.Log(curSkill);
        if(!curSkill._skillDataDic.TryGetValue(type, out SkillData data))
        {
            GetImage((int)Images.Skill_Image).color = Color.gray;
            GetText((int)Texts.Skill_Txt).gameObject.SetActive(false);
            return;
        }

        GetImage((int)Images.Skill_Image).sprite = data.Image;

        GetText((int)Texts.Skill_Txt).gameObject.SetActive(true);
        GetText((int)Texts.Skill_Txt).text = data.SkillName;
    }
}
