using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterStats : MonoBehaviour
{
    public int SizePoints=0;
    public float Speed=1;
    public float SpeedOnStart=1;
    [HideInInspector] public GameObject crown;
    [HideInInspector] public CharacterStats LastTouchedEnemy;
    private void Start()
    {
        SpeedOnStart = Speed;
        crown = GetComponentInChildren<CharacterAnimationEvent>().crown;
    }

    public void UpdateStats(int Value)
    {
        SizePoints += Value;
        float f = 1 - 1 / (1 + 0.1f * (SizePoints / 100));
        Speed = SpeedOnStart - f;
        f = f *2 ;

        transform.DOScale(1 + f,0.5f);

    }
    public void Die()
    {
        LastTouchedEnemy.UpdateStats( SizePoints);
        GameManager.Instance.Characters.Remove(this);
        Destroy(gameObject);

    }

    private void Update()
    {
        if (transform.position.y < -1f)
            Die();
    }

}
