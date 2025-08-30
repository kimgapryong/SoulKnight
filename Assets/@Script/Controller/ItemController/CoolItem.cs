using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolItem : Item_Base
{
    public override void ItemAbilty()
    {
        Skill skill = Manager.Player._skill;
        if(skill.itemTimeCor != null)
        {
            skill.itemTime = false;
            StopCoroutine(skill.itemTimeCor);
        }
        skill.itemTimeCor = StartCoroutine(Abilty(skill));
    }

    private IEnumerator Abilty(Skill skill)
    {
        skill.itemTime = true;
        yield return new WaitForSeconds(precent);
        skill.itemTime = false;
    }
}
