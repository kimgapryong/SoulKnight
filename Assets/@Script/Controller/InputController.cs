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
            if(mousePos.x < Manager.Pos.transform.position.x)
                Manager.Pos.transform.eulerAngles = Vector3.zero;
            else if(mousePos.x > Manager.Pos.transform.position.x)
                Manager.Pos.transform.eulerAngles = new Vector3(0, -180, 0);

            Manager.Pos.transform.position = mousePos;
            PlayerPosition();
        }
        
    }

    private void PlayerPosition()
    {
        Transform posRoot = Manager.Pos.transform;
        Vector3 p1 = posRoot.Find("Cha1Pos").position;
        Vector3 p2 = posRoot.Find("Cha2Pos").position;
        Vector3 p3 = posRoot.Find("Cha3Pos").position;

        int othersSlot = 0;

        foreach (PlayerController[] plaList in Manager.Character._playerDic.Values)
        {
            var pc = plaList[0];
            var auto = plaList[1];

            if (pc == Manager.Player)
            {
                pc.SetPoint(p1);
                continue;
            }

            auto.SetPoint(othersSlot == 0 ? p2 : p3);
            othersSlot++;
        }
    }
}
