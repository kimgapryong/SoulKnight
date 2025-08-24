using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : BaseController
{
    Camera mainCam;

    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        mainCam = Camera.main;

        return true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
        {
            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Manager.Player.SetPoint(mousePos);
        }
        
    }
}
