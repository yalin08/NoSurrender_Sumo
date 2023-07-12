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
    bool gameFinished;
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
    public void ChangeOffset(float f) //Zooms out as you get bigger
    {
        _offset.z = offset.z - f;
    }
    public void OnGameFinish() //Zooms in to the winner
    {
        if (gameFinished) 
            return;
        _offset.y = _offset.y / 2;
        _offset.z = offset.z / 2;
        gameFinished = true;
    }

    void LateUpdate()
    {
        if(followObject!=null)
        transform.position = Vector3.Lerp(transform.position, followObject.position + _offset, speed * Time.deltaTime);
    }
}
