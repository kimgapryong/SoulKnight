using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCanvas : UI_Scene
{
    [SerializeField]
    private PlayerData[] _datas;
    enum Objects
    {
        Start_Btn,
        Explanation_Btn,
        Exit_Btn,
    }

    protected override bool Init()
    {
        if(base.Init() == false) 
            return false;

        BindObject(typeof(Objects));

        BindEvent(GetObject((int)Objects.Start_Btn).gameObject, () =>
        {
            Debug.Log("Å¬¸¯");
            Manager.UI.ShowPopUI<CharacterPop>(callback: (pop) =>
            {
                pop.SetInfo(_datas);
            });
        });

        return true;
    }
}
