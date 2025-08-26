using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public Dictionary<Define.HeroName, Skill> _skillDic = new Dictionary<Define.HeroName, Skill>();

    public void SetSkillDic(Define.HeroName type, Skill skill)
    {
        _skillDic[type] = skill;
    }

    public void SkillUpgrade()
    {
        
    }
}
