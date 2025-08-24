using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPop : UI_Pop
{
    enum Objects
    {
        HeroBg,
    }
    enum Buttons
    {
        GameStart_Btn
    }

    List<HeroSelectFragment> _heroList = new List<HeroSelectFragment>();
    PlayerData[] _datas;
    PlayerData startData;
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.GameStart_Btn).gameObject, () =>
        {
            Manager.Scene.Load("StartStage", () => { SpwanCharacter(); });
        });
        GetButton((int)Buttons.GameStart_Btn).gameObject.SetActive(false);

        for(int i = 0; i < _datas.Length; i++)
        {
            Manager.UI.MakeSubItem<HeroSelectFragment>(GetObject((int)Objects.HeroBg).transform, callback: (fragment) =>
            {
                fragment.SetInfo(this,_datas[i]);
                _heroList.Add(fragment);
            });
        }
        return true;
    }
    private void SpwanCharacter()
    {
        Vector3 vec = Vector3.zero;
        Manager.Character.CreatePlayer(startData, vec);
        for(int i =0; i < _datas.Length;i++)
        {
            if (_datas[i] == startData)
                continue;

            vec += Vector3.one;
            Manager.Character.CreatePlayer(_datas[i], vec);
        }
    }
    public void SetInfo(PlayerData[] datas)
    {
        _datas = datas;
    }

    public void GetData(PlayerData data)
    {
        startData = data;
    }
    public void ButtonActive()
    {
        if (!GetButton((int)Buttons.GameStart_Btn).gameObject.activeSelf)
            GetButton((int)Buttons.GameStart_Btn).gameObject.SetActive(true);
    }

    public void AllHeroBtn(HeroSelectFragment hero)
    {
        for(int i = 0; i < _heroList.Count; i++)
        {
            if (_heroList[i] == hero)
            {
                _heroList[i].BtnFalse();
                continue;
            }
            _heroList[i].BtnTrue();
        }
    }
}
