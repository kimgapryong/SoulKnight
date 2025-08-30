using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item_Base : BaseController
{
    public ItemData data;
    public string itemName;
    public Define.ItemType itemType;
    public Sprite Image;
    protected float precent;
    public float maxItem;
    protected override bool Init()
    {
        if(base.Init() == false)    
            return false;

        itemName = data.ItemName;
        itemType = data.Type;
        precent = data.Precent;
        maxItem = data.ItemMax;
        Image = data.Image;

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pla = collision.GetComponent<PlayerController>();
        if (pla == null )
            return;

        Manager.Item.AddItem(pla, this);
        gameObject.transform.position = Vector2.one * 2000f;
    }

    public abstract void ItemAbilty();
}
