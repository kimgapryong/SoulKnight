using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : BaseController
{
    private float damage;
    private CreatureController attker;
    private Vector3 dir;
    private float speed;

    private bool penetration;

    private bool chain = false;
    private int maxCount = 0;
    private int count = 0;
    public void SetInfo(CreatureController attker, Vector3 dir, float damage, float speed = 7f, bool penetration = false, float time = 5f)
    {
        this.attker = attker;
        this.dir = dir;
        this.damage = damage;
        this.speed = speed;
        this.penetration = penetration;

        AlignRotationToDir();
        if (penetration)
        {
            Destroy(gameObject, time);
            return;
        }
        Destroy(gameObject, time);    
        
    }
    private void AlignRotationToDir()
    {
        if (dir.sqrMagnitude < 0.0001f) return;
        dir = dir.normalized;

        float angle = Vector2.SignedAngle(Vector2.right, (Vector2)dir);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController m = collision.GetComponent<MonsterController>();
        if (m == null) return;

        m.OnDamage(attker, damage);

        if (chain && count < maxCount)
        {
            MonsterController mon = Manager.Monster.ChainMonster(m);
            dir = (m.transform.position - mon.transform.position).normalized;
            AlignRotationToDir();
            count++;
            return;
        }

        if(!penetration)
            Destroy(gameObject);
    }

    public void ChainAttack(int maxCount)
    {
        chain = true;
        this.maxCount = maxCount;
    }
}
