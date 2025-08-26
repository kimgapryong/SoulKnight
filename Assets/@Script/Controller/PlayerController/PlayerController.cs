using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    private Define.HeroType _type;
    protected Vector3 endPoint;
    private string animString;
    protected float dist; // 도착지점 거리
    public MonsterController monster; // 현재 몬스터
    protected bool atkCool; // 공격시간 쿨타임
    protected bool isWalk; //공격시 이동중인지

    public Skill _skill;
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        State = Define.State.Idle;
        dist = 0.001f;
        
        _skill= transform.Find("SkillAnim").GetComponent<Skill>();

       
       
        return true;
    }

    public void SetInfo(PlayerStatus status)
    {
        _status = status;
    }

    protected override void ChangeAnim(Define.State state)
    {
        if (!(this is AutoPlayerController))
        {
            if(State == Define.State.Idle)
                return;
            Manager.Character.ChangeAutoState(State);
        }
            

        string animKey = "Side";
        if (Mathf.Abs(Direct.x) - Mathf.Abs(Direct.y) < 0)
            if (Direct.y > 0)
                animKey = "B";
            else
                animKey = "F";
        else if (Mathf.Abs(Direct.x) - Mathf.Abs(Direct.y) > 0)
            animKey = "Side";

        switch (state)
            {
                case Define.State.Attack:
                {
                    if (isWalk)
                    {
                        animString = $"Walk_{animKey}";
                        anim.Play(animString);
                    }
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
                    string idleKey = animString == "Walk_Side" ? "Idle_Side" : animString == "Walk_F" ? "Idle_F" : animString == "Walk_B" ? "Idle_B" : "Idle_Side";
                    anim.Play(idleKey);
                }   
                    break;
            }
    }
    protected override void Move()
    {
        monster = null;
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
        if(monster == null)
            return;

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
        Direct = (monster.transform.position - transform.position).normalized;
        rb.MovePosition(Vector2.MoveTowards(rb.position, (Vector2)monster.transform.position, _status.Speed * Time.fixedDeltaTime));
    }

    protected virtual void NormalAttack()
    {
        monster.OnDamage(this, _status.Damage);
    }
    public void SetPoint(Vector3 point)
    {
        State = Define.State.Move;
        ChangeAnim(Define.State.Move);
        endPoint = point;
    }
    public void SetTarget(MonsterController monster)
    {
        
        if(this.monster == monster)
            return;

        ChangeAnim(Define.State.Move);
        State = Define.State.Attack;
        this.monster = monster;
    }

    private void OnEnable()
    {
        if (this is AutoPlayerController)
            return;

        if(_skill == null)
            _skill = transform.Find("SkillAnim").GetComponent<Skill>();

        _skill.SetPlayer(this);

    }
  
}
