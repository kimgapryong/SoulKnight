using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : BaseController
{
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

    protected Status _status;
    private bool _canAtk = true;
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
            case Define.State.Run:
                Run();
                break;
            case Define.State.Idle:
                Idle();
                break;
        }
    }

    protected virtual void Attack() { }
    protected virtual void Run() { }
    protected virtual void Idle() { }

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
    }
    protected virtual void OnDie() { Debug.Log("µÚÁü"); }

    protected virtual IEnumerator WaitTime(float time = 0.3f, Action callback = null)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }
}
