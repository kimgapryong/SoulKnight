using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStage : MonoBehaviour
{
    private bool _init;

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
