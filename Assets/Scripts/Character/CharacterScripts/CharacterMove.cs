using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CharacterMove : MonoBehaviour
{
    [HideInInspector] public CharacterStats Stats;
    [HideInInspector] public AnimationController ac;
    [HideInInspector] public Rigidbody rb;


    public bool canMove = true;



    private void Awake()
    {
        Stats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody>();
        ac = GetComponent<AnimationController>();

    }
    private void Update()
    {
        if (canMove)
        {

            float runSpeed = 1 - 1 / (1 + 0.3f * (Stats.SizePoints / 100)); // Your animation speed gets slower as you get bigger
            runSpeed = 1 - runSpeed / 2;
            ac.Run(runSpeed);
            // Debug.Log(f);
        }

        if (transform.position.y < -0.5f)
        {
            Die();
        }
    }
    void Die()
    {

    }
    Quaternion LookRotation(Transform RotationObject)
    {
        var lookPos = RotationObject.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);

        return rotation;

    }
    Vector3 direction; Vector3 velocity;
    public void MoveTo(Transform MoveToObject)
    {
        if (!canMove)
            return;

        //Checks the direction of the target
        Vector3 MoveToVector = new Vector3(MoveToObject.position.x, 0, MoveToObject.position.z);
        Vector3 MoveFromVector = new Vector3(transform.position.x, 0, transform.position.z);
        direction = Vector3.Normalize(MoveToVector - MoveFromVector);


        //Moves towards that direction
        transform.rotation = Quaternion.RotateTowards(transform.rotation, LookRotation(MoveToObject), Stats.speed*100 *Time.deltaTime);
        velocity = Vector3.MoveTowards(velocity, direction * Stats.speed, Stats.speed *10* Time.deltaTime);
     
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    public void Knockback(Vector3 vector3, float otherSize) //Gets knocked over depending on other player's size
    {
        canMove = false;
        
        if (gameObject.layer == 3) //Camera shake if player gets knocked over
        {
            CameraFollow.Instance.ShakeCamera();
        }

        float sizeDifference = otherSize - Stats.SizePoints;
        if (sizeDifference < 1) sizeDifference = 1;

        sizeDifference += otherSize/2;

        float throwForce = Mathf.Pow(sizeDifference, 1f / 10f) * 0.6f;

        rb.velocity = Vector3.zero;
        rb.AddForce(vector3 * throwForce,ForceMode.Impulse);

        //   float FallTime = 1 - 1 / (1 + 0.1f * ());
        float FallTime = 1 - 1 / Mathf.Pow(1 + sizeDifference, 0.33f);
        FallTime = 3 - FallTime * 2;

        ac.Fall(FallTime);


    }


}
