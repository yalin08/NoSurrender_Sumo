using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColliderController : MonoBehaviour
{
    [HideInInspector] public CharacterStats Stats;
    public int GainByEating;
    public float KnockbackAmount;
    private void OnValidate()
    {
        Stats = GetComponentInParent<CharacterStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            Destroy(other.gameObject);
            Stats.UpdateStats(GainByEating);
          
        }
        if (other.CompareTag("Character"))
        {
            Debug.Log("y");

            Vector3 direction = Vector3.Normalize(other.transform.position - transform.position);

            CharacterMove cm= other.GetComponent<CharacterMove>();
            cm.Knockback((direction*(Mathf.Sqrt(Stats.SizePoints)+ KnockbackAmount)*10));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
