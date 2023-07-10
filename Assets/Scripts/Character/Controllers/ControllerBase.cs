using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterMove))]
[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(CharacterColliderController))]
public class ControllerBase : MonoBehaviour
{

  [HideInInspector]  public CharacterMove Move;

    private void OnValidate()
    {
        Move = GetComponent<CharacterMove>();
    }



}
