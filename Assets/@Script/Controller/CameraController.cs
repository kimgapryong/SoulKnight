using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BaseController
{
    [SerializeField]
    private float moveSpeed = 14f;

    private Transform player;

    protected override bool Init()
    {
        if(base.Init() == false)    
            return false;

        Manager.Instance.SetCamera(this);

        return true;
    }
    void LateUpdate()
    {
        Vector3 target = player.position;
        target.z = -10f; 

        float t = 1f - Mathf.Exp(-moveSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, target, t);
    }

    public void ChangePlayer(PlayerController player)
    {
        this.player = player.transform;
    }
}
