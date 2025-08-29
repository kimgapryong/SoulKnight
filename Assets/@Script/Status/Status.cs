using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public Action<float, float> hpAction;
    private bool _init;
    public string HeroName { get; private set; }
    public Sprite Image { get; private set; }
    public int Level { get; set; }
    public float Hp { get;  set; }
    private float _curHp;
    public float CurHp
    {
        get {  return _curHp; }
        set
        {
            _curHp = Mathf.Min(Hp, value);
            hpAction?.Invoke(_curHp, Hp);
        }
    }
    public float Damage { get; set; }
    public float Speed { get; set; }
    public float Defence {  get; set; } 
    public float Arange { get; set; }
    public float Detection { get; set; }
    public float AtkSpeed {  get; set; }    

    public virtual void SetInfo(string heroName, Sprite image,int level, float hp, float damage, float speed, float defence, float arange, float detection, float atkSpeed)
    {
        HeroName = heroName;
        Image = image;

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

    private void Start()
    {
        Init();
    }

    public virtual bool Init()
    {
        if (!_init)
        {
            _init = true;
            return true;
        }

        return false;
    }
}
