using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Status
{
    public float Amount { get; private set; }
    public EnemyStatus(int level, float hp, float damage, float speed, float defence, float arange, float amount) : base(level, hp, damage, speed, defence, arange)
    {
        Amount = amount;
    }
   
}
