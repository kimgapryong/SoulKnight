using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{
    private int index;
    public List<PlayerController> _playerList = new List<PlayerController>();

    private void AddPlayer(PlayerController player)
    {
        _playerList.Add(player);
    }

    public void CreatePlayer(PlayerData data, Vector3 pos)
    {
        GameObject player = Manager.Resources.Instantiate($"Players/{data.Path}", pos, Quaternion.identity);
        player.name = data.Path;

        PlayerController pc = player.AddComponent<PlayerController>();
        pc.SetInfo(new PlayerStatus(data.Type, data.Level, data.Hp, data.Damange, data.Speed, data.Defence, data.Arange, data.Detction, data.AtkSpeed, 100, data.Mp));

        //처음 등록한 플레이어
        if(_playerList.Count == 0)
        {
            index = 0;
            Manager.Instance.SetPlayer(pc);
            Manager.Camera.ChangePlayer(pc);
        }

        AddPlayer(pc);
    }
    public void ChangePlayer(int idx)
    {
        index += idx;

        if(index < 0)
            index = _playerList.Count - 1;

        if(index >= _playerList.Count)
            index = 0;

        foreach(PlayerController pc in _playerList) 
            pc.enabled = false;

        _playerList[index].enabled = true;
        Manager.Instance.SetPlayer(_playerList[index]);
        Manager.Camera.ChangePlayer(_playerList[index]);

    }
}
