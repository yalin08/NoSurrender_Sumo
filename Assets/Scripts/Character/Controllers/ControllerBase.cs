using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterMove))]
[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(CharacterEatBurger))]
[RequireComponent(typeof(CharacterKnockOthersAway))]
[RequireComponent(typeof(AnimationController))]
public class ControllerBase : MonoBehaviour
{

  [HideInInspector]  public CharacterMove Move;

    private void OnValidate()
    {
        Move = GetComponent<CharacterMove>();
    }



}
