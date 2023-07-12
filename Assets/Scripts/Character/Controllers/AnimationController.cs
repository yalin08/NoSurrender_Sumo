using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;


    public void Run(float runSpeed)
    {
        animator.speed = runSpeed;
        animator.SetTrigger("Run");
    }

    public void Fall(float fallSpeed)
    {
        animator.speed = fallSpeed; 
        animator.SetTrigger("Fall");
    }

    public void Dance()
    {
        animator.speed =1;
        animator.SetTrigger("Dance");
    }


}
