using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item Data", menuName ="New Item Data")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public Define.ItemType Type;
    public Sprite Image;

    [Header("퍼센트 단위")]
    public float Precent;

    public float ItemMax;

}
