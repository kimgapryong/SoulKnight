using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStage : BaseController
{
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        Manager.UI.ShowSceneUI<MainCanvas>();
        return true;
    }
}
