using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpCoolItem : Item_Base
{
    public override void ItemAbilty()
    {
        Skill skill = Manager.Player._skill;
        if (skill.mpTimeCor != null)
        {
            skill.mpTime = false;
            StopCoroutine(skill.mpTimeCor);
        }
        skill.mpTimeCor = StartCoroutine(Abilty(skill));
    }

    private IEnumerator Abilty(Skill skill)
    {
        skill.mpTime = true;
        yield return new WaitForSeconds(precent);
        skill.mpTime = false;
    }
}
