using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using DG.Tweening;

public class CameraFollow : Singleton<CameraFollow>
{

    public Vector3 offset;
     Vector3 _offset;

    public Transform followObject;
    public float speed;

    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        _offset = offset;
        transform.position = followObject.position + _offset;
    }

    public void ShakeCamera()
    {
        mainCamera.DOShakePosition(0.1f, 1f);
    }  
    public void ChangeOffset(float f)
    {
        _offset.z = offset.z - f;
    }

    void LateUpdate()
    {
        if(followObject!=null)
        transform.position = Vector3.Lerp(transform.position, followObject.position + _offset, speed * Time.deltaTime);
    }
}
