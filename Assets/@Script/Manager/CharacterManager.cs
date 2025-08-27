using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{
    public Action ChangeAction;
    public int playerCount = 0;
    private int index;

    public Dictionary<int, PlayerController[]> _playerDic = new Dictionary<int, PlayerController[]>(); 
    
    public void CreatePlayer(PlayerData data, Vector3 pos)
    {
        PlayerController[] pcList = new PlayerController[2];

        GameObject player = Manager.Resources.Instantiate($"Players/{data.Path}", pos, Quaternion.identity);
        player.name = data.Path;

        PlayerStatus status = player.AddComponent<PlayerStatus>();
        status.SetPlayerData(data.Type,data.HeroName, data.Image, data.Level, data.Hp, data.Damange, data.Speed, data.Defence, data.Arange, data.Detction, data.AtkSpeed, 100, data.Mp);

        PlayerController pc = player.GetComponent<PlayerController>();
        pc.SetInfo(status);
        AutoPlayerController auto = player.AddComponent<AutoPlayerController>();
        auto.SetInfo(status);

        pcList[0] = pc;
        pcList[1] = auto;

        //처음 등록한 플레이어
        if(playerCount == 0)
        {
            index = 0;
            auto.enabled = false;
            Manager.Instance.SetPlayer(pc);
            Manager.Camera.ChangePlayer(pc);
        }
        else
        {
            pc.enabled = false;
            auto.enabled = true;

        }

        _playerDic.Add(playerCount, pcList);
        playerCount++;
    }
    public void ChangePlayer(int idx)
    {
        index += idx;

        if(index < 0)
            index = _playerDic.Count - 1;

        if(index >= _playerDic.Count)
            index = 0;


        //플레이어 자동화 활성/비활성
        foreach(PlayerController[] pcList in _playerDic.Values)
        {
            pcList[0].enabled = false; //PlayerController는 비활성화
            pcList[1].enabled = true; //AutoPlayerController는 활성화
        }

        _playerDic[index][0].enabled = true;
        _playerDic[index][1].enabled = false ;

        Manager.Instance.SetPlayer(_playerDic[index][0]);
        Manager.Camera.ChangePlayer(_playerDic[index][0]);

        ChangeAction.Invoke();

    }
    public void ChangeAutoState(Define.State state)
    {
        foreach (PlayerController[] pcList in _playerDic.Values)
        {
            if(_playerDic[index][1] == pcList[1])
                continue;

            AutoPlayerController auto = pcList[1] as AutoPlayerController;
            if (state == Define.State.Attack)
            {
                if(auto._cor != null) return;
                auto.StartAutoSkill();
            }
            else 
                auto.StopAutoSkill();
            auto.State = state;
        }
    }
}
