using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public Define.HeroType Type;
    public string HeroName;

    public int Level;
    public float Hp;
    public float Mp;
    public float Damange;
    public float Speed;
    public float Defence;
    public float Arange;
}
