using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKnockOthersAway : MonoBehaviour
{
    [HideInInspector] public CharacterStats Stats;
    [HideInInspector] public CharacterMove Move;
    

    public float KnockbackAmount;
    public float criticalMultiplier;
    private void OnValidate()
    {
        Stats = GetComponentInParent<CharacterStats>();
        Move = GetComponentInParent<CharacterMove>();
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Character"))
        {

            Vector3 directionEnemy = Vector3.Normalize(collision.transform.position - transform.position);
            Vector3 directionSelf = Vector3.Normalize(transform.position - collision.transform.position);

            CharacterMove cm = collision.gameObject.GetComponent<CharacterMove>();
            //  float f = KnockbackAmount + (Stats.SizePoints * 0.01f);





            cm.Knockback(directionEnemy * KnockbackAmount, Stats.SizePoints);
            cm.Stats.LastTouchedEnemy = Stats;
        }
        else if (collision.transform.CompareTag("WeakPoint"))
        {
            if (collision.transform.parent == transform) return;


            Vector3 directionEnemy = Vector3.Normalize(collision.transform.parent.position - transform.position);
            directionEnemy.y = 0;
            Vector3 directionSelf = Vector3.Normalize(transform.position - collision.transform.parent.position);

            Debug.Log("critical hit");
            CharacterMove cm = collision.transform.parent.GetComponent<CharacterMove>();
            //  float f = KnockbackAmount + (Stats.SizePoints * 0.01f);





            cm.Knockback(directionEnemy * KnockbackAmount * criticalMultiplier, Stats.SizePoints);
            cm.Stats.LastTouchedEnemy = Stats;
        }

    }



}
