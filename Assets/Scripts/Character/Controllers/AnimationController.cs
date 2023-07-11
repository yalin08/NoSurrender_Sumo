using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;


    public void Run(float f)
    {
        animator.speed = f;
        animator.SetTrigger("Run");
    }

    public void Fall(float f)
    {
        animator.speed = f;
        animator.SetTrigger("Fall");
    }

    public void Dance()
    {
        animator.speed =1;
        animator.SetTrigger("Dance");
    }


}
