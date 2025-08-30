using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager 
{
    public Dictionary<Define.HeroType, Dictionary<Define.ItemType, ItemDatas>> _itemDic = new Dictionary<Define.HeroType, Dictionary<Define.ItemType, ItemDatas>>();
    public void AddItem(PlayerController pla, Item_Base data)
    {
        Define.HeroType curType = pla._type;
        
        Dictionary<Define.ItemType, ItemDatas> dic;
        if (!_itemDic.TryGetValue(curType, out dic))
            dic = AddDic(curType);

        //아이템의 갯수가 6이상이면 리턴
        if(dic.Values.Count > 6)
            return;

        ItemDatas item;
        if (!dic.TryGetValue(data.itemType, out item))  //  만약에 아이템이 없으면 1개 추가
        {
            item = new ItemDatas() { name = data.itemName, count = 1, image = data.Image, itemAbilty = data.ItemAbilty };
            dic.Add(data.itemType, item);
            _itemDic[curType] = dic;
            return;
        }

        if(item.count >= data.maxItem)
            return;

        item.count++;
        dic[data.itemType] = item;
        _itemDic[curType] = dic;

        MainCanvas main = Manager.UI.SceneUI as MainCanvas;
        main.ItemRefreshOrdered();
    }
    public bool UseItem(Define.ItemType type)
    {
        Define.HeroType hero = Manager.Player._type;

        if (!_itemDic.TryGetValue(hero, out var bag) || !bag.TryGetValue(type, out var data))
            return false;

        data.itemAbilty?.Invoke();
        // 수량 차감
        data.count = Mathf.Max(0, data.count - 1);

        if (data.count <= 0)
            bag.Remove(type);
        else
            bag[type] = data;

        MainCanvas main = Manager.UI.SceneUI as MainCanvas;
        main.ItemRefreshOrdered();

        return true;
    }
    private Dictionary<Define.ItemType, ItemDatas> AddDic(Define.HeroType type)
    {
        Dictionary<Define.ItemType, ItemDatas> dic = new Dictionary<Define.ItemType, ItemDatas>();
        _itemDic.Add(type, dic);

        return dic;
    }
}
public struct ItemDatas
{
    public string name;
    public Sprite image;
    public int count;
    public Action itemAbilty;

}