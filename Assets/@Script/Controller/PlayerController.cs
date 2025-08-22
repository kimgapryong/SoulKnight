using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    private Define.HeroType _type;
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        
        return true;
    }

    public void SetInfo(PlayerStatus status)
    {
        _status = status;
    }
}
