using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputController : BaseController
{
    Camera mainCam;
    MainCanvas main;
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        mainCam = Camera.main;
        main = Manager.UI.SceneUI as MainCanvas;
        return true;
    }

    private void Update()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
        {
            if (mousePos.x < Manager.Pos.transform.position.x)
                Manager.Pos.transform.eulerAngles = Vector3.zero;
            else if (mousePos.x > Manager.Pos.transform.position.x)
                Manager.Pos.transform.eulerAngles = new Vector3(0, -180, 0);

            Manager.Pos.transform.position = mousePos;
            PlayerPosition();
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
                return;

            Collider2D col = Physics2D.OverlapPoint(mousePos, LayerMask.GetMask("Monster"));
            if (col)
            {
                MonsterController monster = col.GetComponent<MonsterController>();
                if (monster == null)
                    return;

                Manager.Player.SetTarget(monster);
            }
        }

        /*  if (Input.GetKeyDown(KeyCode.RightArrow))
          {
              Manager.Character.ChangePlayer(1);
          }
          else if (Input.GetKeyDown(KeyCode.LeftArrow))
          {
              Manager.Character.ChangePlayer(-1);
          }*/

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Manager.Character.ChangePlayer(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Manager.Character.ChangePlayer(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Manager.Character.ChangePlayer(2);
        }

        if (Input.GetKeyDown(KeyCode.Q)) { SetSkill(Define.SkillType.Skill1); }
        if (Input.GetKeyDown(KeyCode.W)) { SetSkill(Define.SkillType.Skill2); }
        if (Input.GetKeyDown(KeyCode.E)) { SetSkill(Define.SkillType.Skill3); }
        if (Input.GetKeyDown(KeyCode.R)) { SetSkill(Define.SkillType.Skill4); }
    }
    private void SetSkill(Define.SkillType type)
    {
        Manager.Player._skill._skillDic[type]?.Invoke();
        main._typeSkill[type].StartAmount(0, Manager.Player._skill._skillDataDic[type].CoolTime);
    }
    private void PlayerPosition()
    {
        Transform posRoot = Manager.Pos.transform;
        Vector3 p1 = posRoot.Find("Cha1Pos").position;
        Vector3 p2 = posRoot.Find("Cha2Pos").position;
        Vector3 p3 = posRoot.Find("Cha3Pos").position;

        int othersSlot = 0;

        foreach (PlayerController[] plaList in Manager.Character._playerDic.Values)
        {
            var pc = plaList[0];
            var auto = plaList[1] as AutoPlayerController;

            if (pc == Manager.Player)
            {
                pc.SetPoint(p1);
                continue;
            }

            auto.SetPoint(othersSlot == 0 ? p2 : p3);
            othersSlot++;
        }
    }
}
