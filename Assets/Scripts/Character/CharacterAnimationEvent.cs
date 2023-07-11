using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvent : MonoBehaviour
{
    CharacterMove move;
    public GameObject crown;
    private void OnValidate()
    {
        move = GetComponentInParent<CharacterMove>();

    }


    public void MoveAgain()
    {
        move.canMove = true;
    }
}
