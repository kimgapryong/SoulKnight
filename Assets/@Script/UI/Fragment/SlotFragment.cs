using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFragment : UI_Base
{
    enum Images
    {
        ItemImage
    }
    enum Texts
    {
        ItemCount,
    }
    private Define.ItemType _type;
    private ItemDatas _data;
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        BindEvent(gameObject, () => { Manager.Item.UseItem(_type); });

        GetImage((int)Images.ItemImage).gameObject.SetActive(false);
        GetText((int)Texts.ItemCount).gameObject.SetActive(false);

        return true;
    }

    public void Refresh(Define.ItemType item, ItemDatas data)
    {
        _type = item;
        _data = data;
        GetImage((int)Images.ItemImage).gameObject.SetActive(true);
        GetImage((int)Images.ItemImage).sprite = data.image;

        GetText((int)Texts.ItemCount).gameObject.SetActive(true);
        GetText((int)Texts.ItemCount).text = data.count.ToString();
    }
    public void NonItem()
    {
        GetImage((int)Images.ItemImage).gameObject.SetActive(false);
        GetText((int)Texts.ItemCount).gameObject.SetActive(false);
    }

    
}
