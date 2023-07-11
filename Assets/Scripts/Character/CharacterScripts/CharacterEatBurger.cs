using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEatBurger : MonoBehaviour
{
    [HideInInspector] public CharacterStats Stats;
    public int GainByEating;

    private void OnValidate()
    {
        Stats = GetComponentInParent<CharacterStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Burger"))
        {
            BurgerSpawner.Instance.BurgersOnTheScene.Remove(other.gameObject.transform);
            Destroy(other.gameObject);
            Stats.UpdateStats(GainByEating);
          
        }
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
