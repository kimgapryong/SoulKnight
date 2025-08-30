using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : CreatureController
{
    public PlayerData data;

    private string animString;
    private Vector3 endPoint;
    protected float moveDist = 2f;

    protected bool atkCool = false;   
    private bool back = false;
    private bool sturn = false;
    public EnemyStatus _status;

    [SerializeField]
    protected PlayerController target;

    private Coroutine _sturn;
    private Coroutine _poision;
    private float CurSpeed = 2;
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        //Å×½ºÆ®
        _status = transform.GetComponent<EnemyStatus>();
        _status.SetEnemyData(data.HeroName, data.Image, data.Level, data.Hp, data.Damange, data.Speed, data.Defence, data.Arange, data.Detction, data.AtkSpeed, data.Exp);
        CurSpeed = _status.Speed;

        //SetStatus(_status);

        AddMy();
        State = Define.State.Idle;

        return true;
    }
    
   protected override void ChangeAnim(Define.State state)
    {
        switch (state)
        {
            case Define.State.Attack:
                anim.Play("Hit");
                break;
            case Define.State.Move:
                anim.Play("Walk");
                break;
            case Define.State.Idle:
                anim.Play("Walk");
                break;
        }
    }

    protected override void Move()
    {
        if (target == null)
        {
            rb.velocity = Vector3.zero;
            State = Define.State.Idle;
            return;
        }

        
        if (Vector2.Distance(transform.position, (Vector2)target.transform.position) <= _status.Arange && !atkCool)
        {
            rb.velocity = Vector3.zero;
            State = Define.State.Attack;
            return;
        }

        MovePlayer();
    }
    protected override void Idle()
    {
        if (atkCool)
        {
            rb.velocity = Vector2.zero;
            return;
        }

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
        if(moveTime >= 1f)
        {
            moveTime = 0f;
            Direct = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
        moveTime += Time.deltaTime;
        rb.MovePosition(Vector2.MoveTowards(rb.position, rb.position + (Vector2)Direct * moveDist, _status.Speed * Time.fixedDeltaTime));
    }
    private void MovePlayer()
    {
        Direct = (endPoint - transform.position).normalized;
        rb.MovePosition(Vector2.MoveTowards(rb.position, (Vector2)endPoint, _status.Speed * Time.fixedDeltaTime));
    }

    protected override void Attack()
    {
        atkCool = true;
        State = Define.State.Move;
        target.OnDamage(this, _status.Damage);
        StartCoroutine(WaitTime(_status.AtkSpeed, () => atkCool = false));
    }
    protected override void UpdateMethod()
    {
        if(back || sturn)
        {
            rb.velocity = Vector2.zero;
            return;
        }
            
        SearchPlayers();
        base.UpdateMethod();
    }
    private void SearchPlayers()
    {
        bool get = false;
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
                    get = true;
                }
            }
        }

        if(!get)
            target = null;
    }

    public override void OnDamage(CreatureController attker, float damage)
    {
        Apply(attker.transform.position, 10f);
        Hit(attker, damage);

        sr.color = Color.red;
        
        if (_poision != null || _sturn != null)
            StartCoroutine(WaitTime(0.15f, () => sr.color = Color.gray));
        else
            StartCoroutine(WaitTime(0.15f, () => sr.color = Color.white));
    }

    protected override void OnDie(CreatureController attker)
    {
        AutoPlayerController auto = attker.GetComponent<AutoPlayerController>();
        if(auto == null) 
            return;

        auto.AutoReset(this);
        auto._status.AddExp(_status.Amount);

        Destroy(gameObject);
    }
    public  void Apply(Vector2 source, float power, float upBonus = 0f)
    {

        State = Define.State.Idle;
        back = true;

        if(rb == null)
            return ;
        rb.velocity = Vector2.zero;
        Vector2 dir = ((Vector2)rb.position - source).normalized;
        rb.AddForce((dir + Vector2.up * upBonus) * power, ForceMode2D.Impulse);

        StartCoroutine(WaitTime(0.2f, () => { back = false; State = Define.State.Move; }));
    }

    public void Sturn(float time)
    {
        sturn = true;
        State = Define.State.Idle;

        Debug.Log(time);
        sr.color = Color.gray;

        if (_sturn != null)
            StopCoroutine(_sturn);

        _sturn = StartCoroutine(WaitTime(time, () => { sturn = false; sr.color = Color.white; _sturn = null;  Debug.Log("STURN"); }));

    }

    private void AddMy()
    {
        Manager.Monster._monList.Add(this);
    }

    public void Poision(float time,float damage,float speed)
    {
        sr.color = Color.gray;

        if (_poision != null )
            StopAllCoroutines();
        
        _poision = StartCoroutine(SetPoision(damage, speed));
        StartCoroutine(WaitTime(time, () => { StopCoroutine(_poision); _poision = null; _status.Speed = CurSpeed; sr.color = Color.white; }));
    }
    private IEnumerator SetPoision(float damage, float speed)
    {
        _status.Speed = _status.Speed * (speed / 100);
        while (true)
        {
            _status.CurHp -= damage;
            yield return new WaitForSeconds(1f);
        }
    }
}
