using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSkillFragment : UI_Base
{
   enum Images
    {
        Skill_Image,
    }
    enum Texts
    {
        Skill_Txt,
    }

    private Coroutine _cor;
    private Image image;
    public Define.SkillType type;
    protected override bool Init()
    {
        if(base.Init() == false )
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        image = GetComponent<Image>();

        Refresh();
        return true;
    }
    public void SetDic(MainCanvas canvas)
    {
        canvas._typeSkill.Add(type, this);
    }
    public void Refresh()
    {
        Skill curSkill = Manager.Player._skill;
        Debug.Log(curSkill);
        if(!curSkill._skillDataDic.TryGetValue(type, out SkillData data))
        {
            GetImage((int)Images.Skill_Image).color = Color.gray;
            GetText((int)Texts.Skill_Txt).gameObject.SetActive(false);
            return;
        }

        image.sprite = data.Image;
        image.color = Color.gray;

        GetImage((int)Images.Skill_Image).sprite = data.Image;

        GetText((int)Texts.Skill_Txt).gameObject.SetActive(true);
        GetText((int)Texts.Skill_Txt).text = data.SkillName;
    }

    public void StartAmount(float time, float max)
    {
        if(_cor != null)
        {
            GetImage((int)Images.Skill_Image).fillAmount = 0;
            StopCoroutine(_cor);
        }
        _cor = StartCoroutine(FillAmount(time,max));
    }
    private IEnumerator FillAmount(float time, float maxTime)
    {
        float curT = time;
        while(time < maxTime)
        {
            GetImage((int)Images.Skill_Image).fillAmount = curT / maxTime;
            curT += Time.deltaTime;
            yield return null;
        }
        GetImage((int)Images.Skill_Image).fillAmount = 1;
        _cor = null;
    }
}
