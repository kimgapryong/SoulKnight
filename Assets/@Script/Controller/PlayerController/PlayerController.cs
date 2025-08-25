using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    private Define.HeroType _type;
    protected Vector3 endPoint;
    private string animString;
    protected float dist; // �������� �Ÿ�
    protected MonsterController monster; // ���� ����
    protected bool atkCool; // ���ݽð� ��Ÿ��
    private bool isWalk; //���ݽ� �̵�������
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        State = Define.State.Idle;
        dist = 0.001f;
        
        return true;
    }

    public void SetInfo(PlayerStatus status)
    {
        _status = status;
    }

    protected override void ChangeAnim(Define.State state)
    {
        string animKey = "Side";
        if (Mathf.Abs(Direct.x) - Mathf.Abs(Direct.y) > 0)
            animKey = "Side";
        else if (Mathf.Abs(Direct.x) - Mathf.Abs(Direct.y) < 0)
            if (Direct.y > 0)
                animKey = "B";
            else
                animKey = "F";

            switch (state)
            {
                case Define.State.Attack:
                {
                    if (isWalk)
                    {
                        Debug.Log("�ִϸ��̼� ����");
                        animString = $"Walk_{animKey}";
                        anim.Play(animString);
                    }
                    Debug.Log("�ִϸ��̼� ������");
                }
                    break;
                case Define.State.Move:
                {
                    animString = $"Walk_{animKey}";
                    anim.Play(animString);
                }    
                    break;
                case Define.State.Idle:
                {
                    string idleKey = animString == "Walk_Side" ? "Idle_Side" : animString == "Walk_F" ? "Idle_F" : "Idle_B";
                    anim.Play(idleKey);
                }   
                    break;
             
            }

        if (GetType() == typeof(PlayerController))
        {
            if(state == Define.State.Idle)
                return;
            Manager.Character.ChangeAutoState(state);
        }
            
    }
    protected override void Move()
    {
        if(Vector2.Distance(transform.position, endPoint) <= dist)
        {
            rb.velocity = Vector2.zero;
            State = Define.State.Idle;
            return;
        }
        
        Direct = (endPoint - transform.position).normalized;
        rb.MovePosition(Vector2.MoveTowards(rb.position, (Vector2)endPoint, _status.Speed * Time.fixedDeltaTime));

    }

    protected override void Attack()
    {
        if(Vector2.Distance(transform.position, monster.transform.position) <= _status.Arange)
        {
            isWalk = false;
            rb.velocity = Vector2.zero;
            ChangeAnim(Define.State.Attack);

            if (atkCool)
                return;

            atkCool = true;
            NormalAttack();

            State = Define.State.Idle;
            StartCoroutine(WaitTime(_status.AtkSpeed, () => { atkCool = false; State = Define.State.Attack; }));
        }
        else
        {
            isWalk = true;
            ChangeAnim(Define.State.Attack);
            AtkMove();
        }

    }
    private void AtkMove()
    {
        Debug.Log("�ȴ� ��");
        Direct = (monster.transform.position - transform.position).normalized;
        rb.MovePosition(Vector2.MoveTowards(rb.position, (Vector2)monster.transform.position, _status.Speed * Time.fixedDeltaTime));
    }

    protected virtual void NormalAttack()
    {
        monster.OnDamage(this, _status.Damage);
        
        Debug.Log(monster +"����");
    }
    public void SetPoint(Vector3 point)
    {
        State = Define.State.Move;
        endPoint = point;
    }
    public void SetTarget(MonsterController monster)
    {
        State = Define.State.Attack;
        if(this.monster == monster)
            return;

        this.monster = monster;
        Debug.Log("�����Ϸ�");
    }
}
