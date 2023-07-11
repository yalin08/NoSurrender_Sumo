using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using DG.Tweening;

public class CameraFollow : Singleton<CameraFollow>
{

    public Vector3 offset;

    public Transform followObject;
    public float speed;

    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        transform.position = followObject.position + offset;
    }

    public void ShakeCamera()
    {
        mainCamera.DOShakePosition(0.1f, 1f);
    }

    void LateUpdate()
    {
        if(followObject!=null)
        transform.position = Vector3.Lerp(transform.position, followObject.position + offset, speed * Time.deltaTime);
    }
}
