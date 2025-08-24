using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : BaseController
{
    public Vector3 Direct { get; protected set; }

    private Define.State _state;
    public Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            ChangeAnim(value);
        }
    }

    protected Rigidbody2D rb;
    protected Animator anim;
    protected SpriteRenderer sr;

    protected Status _status;
    private bool _canAtk = true;

    protected override bool Init()
    {
        if(base.Init() == false) 
            return false;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        return true;
    }
    protected void SetStatus(Status status)
    {
        _status = status;
    }
    private void Update()
    {
        UpdateMethod();
    }

    protected virtual void ChangeAnim(Define.State state) { }
        

    protected void UpdateMethod()
    {
        switch (State)
        {
            case Define.State.Attack:
                Attack();
                break;
            case Define.State.Idle:
                Idle();
                break;
        }

        sr.flipX = (Direct.x > 0f) ? false
                : (Direct.x < 0f) ? true
                : sr.flipX;
    }

    private void FixedUpdate()
    {
        if(State != Define.State.Move)
            return;
        Move();
    }

    protected virtual void Attack() { Debug.Log("공격"); }
    protected virtual void Move() { }
    protected virtual void Idle() { Debug.Log("대기"); }

    protected virtual void OnDamage(CreatureController attker, float damage)
    {
        if(!_canAtk)
            return;

        _canAtk = false;
        _status.CurHp -= damage;    

        if(_status.CurHp <= 0)
        {
            OnDie();
            return;
        }
        StartCoroutine(WaitTime(callback: () => _canAtk = true));

        sr.color = Color.red;
        StartCoroutine(WaitTime(0.15f, () => sr.color = Color.white));
    }
    protected virtual void OnDie() { Debug.Log("뒤짐"); }

    protected virtual IEnumerator WaitTime(float time = 0.3f, Action callback = null)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }
}
