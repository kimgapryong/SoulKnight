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
    public Color color;

    [Header("연속 공격 발사체 수")]
    public int Again;

    [Header("확률형 스킬")]
    public float Persent;
    public float PersentTime;
}
