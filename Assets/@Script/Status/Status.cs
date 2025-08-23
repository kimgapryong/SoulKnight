using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    public Action<float, float> hpAction;
    public int Level { get; set; }
    public float Hp { get; protected set; }
    private float _curHp;
    public float CurHp
    {
        get {  return _curHp; }
        set
        {
            _curHp = value;
            hpAction.Invoke(value, Hp);
        }
    }
    public float Damage { get; set; }
    public float Speed { get; set; }
    public float Defence {  get; set; } 
    public float Arange { get; set; }
    public float Detection { get; set; }
    public float AtkSpeed {  get; set; }    

    public Status(int level, float hp, float damage, float speed, float defence, float arange, float detection, float atkSpeed)
    {
        Level = level;
        Hp = hp;
        CurHp = hp;
        Damage = damage;
        Speed = speed;
        Defence = defence;
        Arange = arange;
        Detection = detection;
        AtkSpeed = atkSpeed;
    }
}
