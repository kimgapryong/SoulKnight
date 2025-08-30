using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected bool _first = false;

    private void Start()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (!_first)
        {
            _first = true;
            return true;
        }
            
        return false;
    }
}
