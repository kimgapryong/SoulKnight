using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{
    public List<PlayerController> _playerList = new List<PlayerController>();
   
    private void AddPlayer(PlayerController player)
    {
        _playerList.Add(player);
    }

    public void CreatePlayer(PlayerData data, Vector3 pos)
    {
        GameObject player = Manager.Resources.Instantiate($"Players/{data.Path}", pos, Quaternion.identity);
        player.name = data.HeroName;

        PlayerController pc = player.AddComponent<PlayerController>();
        pc.SetInfo(new PlayerStatus(data.Type, data.Level, data.Hp, data.Damange, data.Speed, data.Defence, data.Arange, data.Detction, data.AtkSpeed, 100, data.Mp));

        AddPlayer(pc);
    }
}
