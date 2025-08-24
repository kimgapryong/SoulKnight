using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    private Define.HeroType _type;
    protected Vector3 endPoint;
    private string animString;
    protected float dist;
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        dist = 0.3f;
        
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
            Debug.Log("³¡");
            rb.velocity = Vector2.zero;
            State = Define.State.Idle;
            return;
        }
        Debug.Log(_status.Speed);
        Direct = (endPoint - transform.position).normalized;
        rb.MovePosition(Vector2.MoveTowards(rb.position, (Vector2)endPoint, _status.Speed * Time.fixedDeltaTime));

    }

    public void SetPoint(Vector3 point)
    {
        State = Define.State.Move;
        endPoint = point;
    }
}
