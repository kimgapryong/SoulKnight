using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : UI_Scene
{
    List<CharacterChangeFragment> chaFragmentList = new List<CharacterChangeFragment>();
    List<MainSkillFragment> mainSkillFragments = new List<MainSkillFragment>();
    private SlotFragment[] slotFragments = new SlotFragment[6];

    public Dictionary<Define.SkillType, MainSkillFragment> _typeSkill = new Dictionary<Define.SkillType, MainSkillFragment>();
    private PlayerStatus _status;
   enum Objects
    {
        CharacterChange,
        Skill1_Content,
        Skill2_Content,
        Skill3_Content,
        Skill4_Content,
        Slot_Bg1, Slot_Bg2, Slot_Bg3, Slot_Bg4, Slot_Bg5, Slot_Bg6,
    }
    enum Images
    {
        Exp,
    }
    enum Texts
    {
        Exp_Txt,
    }

    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindObject(typeof(Objects));
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        
        //����Ʈ�� ���߰�
        foreach(var skill in transform.GetComponentsInChildren<MainSkillFragment>())
        {
            skill.SetDic(this);
            mainSkillFragments.Add(skill);
        }
        int cur = 0;
        foreach (var skill in transform.GetComponentsInChildren<SlotFragment>())
        {
            slotFragments[cur] = skill;
            cur++;
        }
            

        // �÷��̾� ���׾��ͽ� â
        for (int i = 0; i < Manager.Character.playerCount; i++)
        {
            PlayerController pla = Manager.Character._playerDic[i][0];
            Manager.UI.MakeSubItem<CharacterChangeFragment>(GetObject((int)Objects.CharacterChange).transform, "Character_Bg", (fa) =>
            {
                fa.SetInfo(pla);
                chaFragmentList.Add(fa);
            });
        }

        Manager.Character.ChangeAction += ChangeAction;

        _status = Manager.Player._status;
        Manager.Player._status.expAction = ExpAction;
        ExpAction();

        return true;
    }
    public void ChangeAction()
    {
        // �÷��̾� ���� ��
        foreach(var fa in chaFragmentList)
        {
            if(fa.myPla == Manager.Player)
            {
                fa.SelectBg();
                continue;
            }
            fa.DeSelectBg();
        }
        // �÷��̾� ��ų ������
        SkillIcon();
        ItemRefreshOrdered();

        Manager.Player._status.expAction = ExpAction;
        _status = Manager.Player._status;
        ExpAction();

    }
    public void SkillIcon()
    {
        foreach (var skill in mainSkillFragments)
            skill.Refresh();
    }
    public void ExpAction()
    {
        GetImage((int)Images.Exp).fillAmount = _status.CurExp / _status.Exp;
        GetText((int)Texts.Exp_Txt).text = $"{_status.CurExp}/{_status.Exp}";
    }

    public void ItemRefreshOrdered()
    {
        if (slotFragments == null || slotFragments.Length == 0) return;

        var type = Manager.Player._type;
        if (!Manager.Item._itemDic.TryGetValue(type, out var bag) || bag == null)
            bag = new Dictionary<Define.ItemType, ItemDatas>();

        int cur = 0;

        // Enum ������� ä���(�����ϴ� �͸�)
        foreach (var it in System.Enum.GetValues(typeof(Define.ItemType)))
        {
            if (cur >= slotFragments.Length) break;
            var itemType = (Define.ItemType)it;
            if (bag.TryGetValue(itemType, out var data))
            {
                slotFragments[cur].Refresh(itemType, data);
                cur++;
            }
        }

        // ���� ĭ ����
        for (int i = cur; i < slotFragments.Length; i++)
            slotFragments[i].NonItem();
    }
}
