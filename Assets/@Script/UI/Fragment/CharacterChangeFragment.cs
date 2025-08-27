using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChangeFragment : UI_Base
{
    enum Images
    {
        Select,
        HeroImage,
        Hp,
        Mp,
    }
    enum Texts
    {
        Level_Txt,
        Hp_Txt,
        Mp_Txt,
    }
    enum Buttons
    {
        Skill_Btn
    }
    public PlayerController myPla;
    private PlayerStatus _status;
    
    protected override bool Init()
    {
        if(base.Init() == false)    
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        Refresh();

        _status.levelAction += Refresh;
        _status.hpAction += HpAction;
        _status.mpAction += MpAction;

        if (myPla != Manager.Player)
            DeSelectBg();

        return true;
    }
    public void Refresh()
    {
        GetImage((int)Images.HeroImage).sprite = _status.Image;
        GetText((int)Texts.Level_Txt).text = $"LEVEL: {_status.Level}";
    }
    public void SetInfo(PlayerController pla)
    {
        myPla = pla;
        _status = pla._status;
    }

    public void SelectBg()
    {
        GetImage((int)Images.Select).gameObject.SetActive(true);
    }
    public void DeSelectBg()
    {
        GetImage((int)Images.Select).gameObject.SetActive(false);
    }
    public void HpAction(float cur, float max)
    {
        GetImage((int)Images.Hp).fillAmount = cur / max;
        GetText((int)Texts.Hp_Txt).text = $"{cur}/{max}";
    }
    public void MpAction(float cur, float max)
    {
        GetImage((int)Images.Mp).fillAmount = cur / max;
        GetText((int)Texts.Mp_Txt).text = $"{cur}/{max}";
    }
}
