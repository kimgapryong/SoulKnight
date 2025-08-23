using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelectFragment : UI_Base
{
    enum Texts
    {
        PlayerName_Txt,
        HeroName_Txt,
        Atk_Txt,
        Hp_Txt,
        Mp_Txt,
        Def_Txt,
        Arg_Txt,
        Speed_Txt
    }
    enum Images
    {
        PlayerImage
    }
    enum Buttons
    {
        Select_Btn
    }

    PlayerData _data;
    CharacterPop chPop;
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));

        Refresh();

        BindEvent(GetButton((int)Buttons.Select_Btn).gameObject, () =>
        {
            chPop.AllHeroBtn(this);
            chPop.ButtonActive();
            chPop.GetData(_data);
        });
        return true;
    }

    public void SetInfo(CharacterPop chPop ,PlayerData data) 
    {
        this.chPop = chPop;
        _data = data;
    }

    private void Refresh()
    {
        GetImage((int)Images.PlayerImage).sprite = _data.Image;
        GetText((int)Texts.PlayerName_Txt).text = $"직업: {_data.Path}";
        GetText((int)Texts.HeroName_Txt).text = $"이름: {_data.HeroName}";
        GetText((int)Texts.Atk_Txt).text = _data.Damange.ToString() ;
        GetText((int)Texts.Hp_Txt).text = _data.Hp.ToString();
        GetText((int)Texts.Mp_Txt).text = _data.Mp.ToString();
        GetText((int)Texts.Def_Txt).text = _data.Defence.ToString();
        GetText((int)Texts.Arg_Txt).text = _data.Arange.ToString();
        GetText((int)Texts.Speed_Txt).text = _data.AtkSpeed.ToString();
    }

    public void BtnTrue()
    {
        GetButton((int)Buttons.Select_Btn).gameObject.SetActive(true);
    }
    public void BtnFalse()
    {
        GetButton((int)Buttons.Select_Btn).gameObject.SetActive(false);
    }
}
