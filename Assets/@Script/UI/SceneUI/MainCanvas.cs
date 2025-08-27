using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : UI_Scene
{
    List<CharacterChangeFragment> chaFragmentList = new List<CharacterChangeFragment>();
   enum Objects
    {
        CharacterChange,
    }

    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindObject(typeof(Objects));

        Manager.Character.ChangeAction += SelectBg;

        for(int i = 0; i < Manager.Character.playerCount; i++)
        {
            PlayerController pla = Manager.Character._playerDic[i][0];
            Manager.UI.MakeSubItem<CharacterChangeFragment>(GetObject((int)Objects.CharacterChange).transform, "Character_Bg", (fa) =>
            {
                fa.SetInfo(pla);
                chaFragmentList.Add(fa);
            });
        }

        return true;
    }
    public void SelectBg()
    {
        foreach(var fa in chaFragmentList)
        {
            if(fa.myPla == Manager.Player)
            {
                fa.SelectBg();
                continue;
            }
            fa.DeSelectBg();
        }
    }
    
}
