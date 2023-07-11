using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.EventSystems;

public class PlayerController : ControllerBase
{


    public VariableJoystick Joystick;

    public Transform MoveToObject;
    float x = 0;
    float z = 1;
    public void DetermineWhereToGo()
    {


        if (Mathf.Abs(Joystick.Horizontal) >= 0.15f || Mathf.Abs(Joystick.Vertical) >= 0.15f)
        {
            x = Joystick.Horizontal;
            z = Joystick.Vertical;
        }

        Vector3 vector = new Vector3(x, 0, z).normalized;

        MoveToObject.position = transform.position + vector;
        MoveToObject.LookAt(transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        DetermineWhereToGo();
        if (!Move.canMove)
        {
            return;
        }
    
        Move.MoveTo(MoveToObject);

        if (Input.GetKeyDown(KeyCode.A))
        {
            Move.ac.Fall(1);
        }

    }
}
