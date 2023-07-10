using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CharacterMove : MonoBehaviour
{
    [HideInInspector] public CharacterStats Stats;
    [HideInInspector] public AnimationController ac;
    Rigidbody rb;


    public bool CanMove = true;

    public float KnockBackTimer;

    private void OnValidate()
    {
        Stats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody>();
        ac = GetComponent<AnimationController>();

    }
    private void Update()
    {
        if (CanMove)
        {

            float f = 1 - 1 / (1 + 0.3f * (Stats.SizePoints / 100));
            f = 1 - f / 2;
            ac.Run(f);
           // Debug.Log(f);
        }

    }

    Quaternion LookRotation(Transform RotationObject)
    {
        var lookPos = RotationObject.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);

        return rotation;

    }
    public void MoveTo(Transform MoveToObject)
    {
        if (!CanMove)
            return;

        Vector3 MoveToVector = new Vector3(MoveToObject.position.x, 0, MoveToObject.position.z);
        Vector3 MoveFromVector = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 direction = Vector3.Normalize(MoveToVector - MoveFromVector);

        transform.rotation = LookRotation(MoveToObject);
        Vector3 velocity = direction * Stats.Speed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    public void Knockback(Vector3 vector3)
    {
        Debug.Log("y");
        rb.AddForce(vector3 );
        ac.Fall(vector3.sqrMagnitude);
    }

}
