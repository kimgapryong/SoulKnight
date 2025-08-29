using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCanvas : UI_Base
{
   enum Images
    {
        Hp,
    }
    enum Texts
    {
        Hp_Txt,
    }
    private EnemyStatus mon;
    Transform p;
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        p = transform.parent;
        mon = transform.parent.GetComponent<EnemyStatus>();
        mon.hpAction = HpAction;

        return true;
    }
    void LateUpdate()
    {
        if (p == null) return;
        
        var local = p.eulerAngles;
        transform.localEulerAngles = local;
    }
    public void HpAction(float cur, float max)
    {
        GetImage((int)Images.Hp).fillAmount = cur / max;
        GetText((int)Texts.Hp_Txt).text = $"{cur}/{max}";
    }

}
