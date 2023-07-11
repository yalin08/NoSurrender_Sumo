using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : ControllerBase
{

    Transform FindClosestBurger()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in BurgerSpawner.Instance.BurgersOnTheScene)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    Transform FindClosestCharacter()
    {
        Transform tMin = null;
  
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (CharacterStats t in GameManager.Instance.Characters)
        {
            if (t != Move.Stats)
            {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t.transform;
                    minDist = dist;
                }
            }
           
        }
        return tMin;
    }
    public Transform Target;

    // Update is called once per frame
    void Update()
    {
        if (BurgerSpawner.Instance.BurgersOnTheScene.Count > 0)
        {
            Target = FindClosestBurger();
        }
        else
        {
            Target = FindClosestCharacter();
        }

        if (GameManager.Instance.Characters[0] == Move.Stats)
            if (GameManager.Instance.Characters.Count > 1)
                Target = FindClosestCharacter();


        Move.MoveTo(Target);
       
    }
}
