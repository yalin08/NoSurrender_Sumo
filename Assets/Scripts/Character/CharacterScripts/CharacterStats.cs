using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int SizePoints=1;
    public float SpeedMultiplier=1;


    public void UpdateStats(int Value)
    {
        SizePoints += Value;
    }

}
