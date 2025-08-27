using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Status
{
    public float Amount { get; private set; }

    public void SetEnemyData(string name, Sprite image,int level, float hp, float damage, float speed, float defence, float arange, float detection, float atkSpeed, float amount)
    {
        SetInfo(name, image, level, hp, damage, speed, defence, arange, detection, atkSpeed);
        Amount = amount;

    }

}
