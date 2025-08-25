using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MonsterController : CreatureController
{
    private string animString;
    private Vector3 endPoint;

    [SerializeField]
    private PlayerController target;

    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        Debug.Log("싲가");
        //테스트
        _status = new EnemyStatus(1, 100, 10, 4, 5, 6, 20, 1, 100);

        State = Define.State.Idle;

        return true;
    }
    public void SetInfo(EnemyStatus status)
    {
        _status = status;
    }

   /* protected override void ChangeAnim(Define.State state)
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
    }*/
    protected override void Move()
    {
        Debug.Log("이동중");
        if (target == null)
        {
            rb.velocity = Vector3.zero;
            State = Define.State.Idle;
            return;
        }

        //몬스터 공격 나중에 구현
       /* if (Vector2.Distance(transform.position, (Vector2)target.transform.position) <= _status.Arange)
        {
            rb.velocity = Vector3.zero;
            State = Define.State.Attack;
            return;
        }*/

        MovePlayer();
    }
    protected override void Idle()
    {
        Debug.Log("대기중");
        if (target != null)
        {
            State = Define.State.Move;
            rb.velocity = Vector3.zero;
            return;
        }
        
        RandomMove();
    }
    float moveTime = 1f;
    private void RandomMove()
    {
        Debug.Log("이동중");
        if(moveTime >= 1f)
        {
            moveTime = 0f;
            Direct = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
        moveTime += Time.deltaTime;
        rb.MovePosition(Vector2.MoveTowards(rb.position, rb.position + (Vector2)Direct * 4, _status.Speed * Time.fixedDeltaTime));
    }
    private void MovePlayer()
    {
        Debug.Log("이동중2");
        Direct = (endPoint - transform.position).normalized;
        rb.MovePosition(Vector2.MoveTowards(rb.position, (Vector2)endPoint, _status.Speed * Time.fixedDeltaTime));
    }
    protected override void UpdateMethod()
    {
        SearchPlayers();
        Debug.LogWarning(State);
        base.UpdateMethod();
    }
    private void SearchPlayers()
    {
        float minSqr = float.MaxValue;
        foreach(var player in Manager.Character._playerDic.Values)
        {
            if(Vector2.Distance(transform.position, player[0].transform.position) <= _status.Detection)
            {
                float curSqr = (player[0].transform.position - transform.position).magnitude;
                if(minSqr > curSqr)
                {
                    minSqr = curSqr;
                    endPoint = player[0].transform.position;
                    target = player[0];
                }
            }
            else
            {
                target = null;
            }
        }
    }
}
