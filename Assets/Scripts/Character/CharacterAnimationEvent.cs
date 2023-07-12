using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvent : MonoBehaviour
{
    CharacterMove move;
    public GameObject crown;
    public TrailRenderer trail;
    public TrailRenderer trailCrit;

    public GameObject hitParticle;
    public GameObject hitCritParticle;
    private void Awake()
    {
        move = GetComponentInParent<CharacterMove>();

    }
    public void EnableTrailCrit()
    {
        trail.enabled = false;
        trailCrit.enabled = true;
        Instantiate(hitParticle, transform.position, transform.rotation);
    }
    public void EnableTrail()
    {
        trailCrit.enabled = false;
        trail.enabled = true;
        Instantiate(hitCritParticle, transform.position, transform.rotation);
    }


    public void MoveAgain() //Gets up after fall animation finishes
    {
        move.canMove = true;
        trail.enabled = false;
        trailCrit.enabled = false;
    }
}
