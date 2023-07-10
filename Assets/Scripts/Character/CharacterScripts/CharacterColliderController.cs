using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColliderController : MonoBehaviour
{
    [HideInInspector] public CharacterStats Stats;
    private void OnValidate()
    {
        Stats = GetComponent<CharacterStats>();
    }
}
