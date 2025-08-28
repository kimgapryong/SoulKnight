using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : UI_Scene
{
    List<CharacterChangeFragment> chaFragmentList = new List<CharacterChangeFragment>();
    List<MainSkillFragment> mainSkillFragments = new List<MainSkillFragment>();
    private PlayerStatus _status;
   enum Objects
    {
        CharacterChange,
        Skill1_Content,
        Skill2_Content,
        Skill3_Content,
        Skill4_Content,
    }
    enum Images
    {
        Exp,
    }
    enum Texts
    {
        Exp_Txt,
    }

    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindObject(typeof(Objects));
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        
        //리스트에 값추가
        foreach(var skill in transform.GetComponentsInChildren<MainSkillFragment>())
            mainSkillFragments.Add(skill);

        // 플레이어 스테어터스 창
        for(int i = 0; i < Manager.Character.playerCount; i++)
        {
            PlayerController pla = Manager.Character._playerDic[i][0];
            Manager.UI.MakeSubItem<CharacterChangeFragment>(GetObject((int)Objects.CharacterChange).transform, "Character_Bg", (fa) =>
            {
                fa.SetInfo(pla);
                chaFragmentList.Add(fa);
            });
        }

        Manager.Character.ChangeAction += ChangeAction;

        _status = Manager.Player._status;
        Manager.Player._status.expAction = ExpAction;
        ExpAction();

        return true;
    }
    public void ChangeAction()
    {
        // 플레이어 선택 바
        foreach(var fa in chaFragmentList)
        {
            if(fa.myPla == Manager.Player)
            {
                fa.SelectBg();
                continue;
            }
            fa.DeSelectBg();
        }
        // 플레이어 스킬 아이콘
        SkillIcon();

        Manager.Player._status.expAction = ExpAction;
        _status = Manager.Player._status;

    }
    public void SkillIcon()
    {
        foreach (var skill in mainSkillFragments)
            skill.Refresh();
    }
    public void ExpAction()
    {
        Debug.Log(_status);
        GetImage((int)Images.Exp).fillAmount = _status.CurExp / _status.Exp;
        GetText((int)Texts.Exp_Txt).text = $"{_status.CurExp}/{_status.Exp}";
    }
    
}
