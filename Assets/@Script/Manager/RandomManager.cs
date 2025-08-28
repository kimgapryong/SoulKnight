using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager
{
    public bool RollPercent(float percent)
    {
        if (percent <= 0f) return false;
        if (percent >= 100f) return true;
        return Random.value < (percent * 0.01f); // 50 -> 0.5
    }
}
