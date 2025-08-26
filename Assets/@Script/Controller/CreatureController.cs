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
            //if (_state == value) return;
            _state = value;
            ChangeAnim(value);
        }
    }

    protected Rigidbody2D rb;
    protected Animator anim;
    protected SpriteRenderer sr;

    public Status _status;
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
        

    protected virtual void UpdateMethod()
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

        float y = (Direct.x > 0f) ? 0f
                : (Direct.x < 0f) ? -180f
                : transform.position.y;

        Vector3 rot = transform.eulerAngles;
        rot.y = y;
        transform.eulerAngles = rot;
    }

    private void FixedUpdate()
    {
        if(State != Define.State.Move)
            return;
        Move();
    }

    protected virtual void Attack() {  }
    protected virtual void Move() { }
    protected virtual void Idle() { rb.velocity = Vector2.zero; }

    public virtual void OnDamage(CreatureController attker, float damage)
    {
        if(!_canAtk)
            return;

        _canAtk = false;
        _status.CurHp -= damage;    

        if(_status.CurHp <= 0)
        {
            OnDie(attker);
            return;
        }
        StartCoroutine(WaitTime(callback: () => _canAtk = true));

        sr.color = Color.red;
        StartCoroutine(WaitTime(0.15f, () => sr.color = Color.white));
    }
    protected virtual void OnDie(CreatureController attker) { Debug.Log("µÚÁü"); }

    protected virtual IEnumerator WaitTime(float time = 0.3f, Action callback = null)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }
}
