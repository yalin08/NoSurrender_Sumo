using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterStats : MonoBehaviour
{
    public int SizePoints=0;
    public float Speed=1;


    public void UpdateStats(int Value)
    {
        SizePoints += Value;
       transform.DOScale(1+(SizePoints*0.001f),0.5f);

    }

}
