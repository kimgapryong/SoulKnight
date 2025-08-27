using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Skill Data", fileName ="Skill Data")]
public class SkillData : ScriptableObject
{
    public string SkillName;
    public Sprite Image;

    public int Target;
    public int SkillPoint;
    public float Damage;
    public float Mp;
    public float SkillArange;
    public float CoolTime;

    //연속공격
    public int Again;
}
