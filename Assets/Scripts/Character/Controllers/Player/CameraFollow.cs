using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CameraFollow : Singleton<CameraFollow>
{

    public Vector3 offset;

    public Transform FollowObject;
    public float speed;

    public void ShakeCamera()
    {

    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, FollowObject.position + offset, speed * Time.deltaTime);
    }
}
