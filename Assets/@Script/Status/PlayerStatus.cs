using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    private const int MAX_LEVEL = 20;
    
    private Define.HeroType _type;

    public Action<float, float> mpAction;
    public Action<float, float> expAction;
    public float Mp { get; protected set; }
    private float _curMp;
    public float CurMp
    {
        get { return _curMp; }
        set
        {
            _curMp = value;
            mpAction.Invoke(value, Mp);
        }
    }
    public float Exp { get; protected set; } = 150;
    private float _curExp;
    public float CurExp
    {
        get { return _curExp; }
        set
        {
            _curExp = value;
            expAction.Invoke(value, Exp);
        }
    }
    public PlayerStatus(Define.HeroType type,int level, float hp, float damage, float speed, float defence, float arange, float detection, float atkSpeed, float exp, float mp) : base(level, hp, damage, speed, defence, arange, detection, atkSpeed)
    {
        _type = type;
        Exp = exp;
        CurExp = Exp;
        Mp = mp;
        CurMp = Mp;
    }

    private void LevelUp()
    {
        switch (_type)
        {
            case Define.HeroType.Archer:
                StatusUp(15,8, 1.3f, 1.2f);
                break;
            case Define.HeroType.Magic:
                StatusUp(10,10, 1.15f, 1.6f);
                break;
            case Define.HeroType.Sowrd:
                StatusUp(20,10, 1.5f, 1.2f);
                break;
        }
        Speed *= 1.2f;
        Exp *= 1.6f;
    }

    private void StatusUp(float hp, float damage, float defence, float mp)
    {
        Hp += hp;
        CurHp = Hp;
        Damage += damage;
        Defence *= defence;
        Mp *= mp;
        CurMp = Mp;
    }
    public void AddExp(float amount)
    {
        if(Level >= MAX_LEVEL)
        {
            CurExp = 0;
            return;
        }

        CurExp += amount;
        while(CurExp >= Exp)
        {
            CurExp -= Exp;
            LevelUp();
        }
    }

}
