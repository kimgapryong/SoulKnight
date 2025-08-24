using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayerController : PlayerController
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }
    protected override void Move()
    {
        Debug.LogWarning("¼¼ÆÃ");
        base.Move();
    }
}
