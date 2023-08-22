using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationSystem : MonoBehaviour
{


    public Vector2 direction;

    Animator animator;

    CharacterPhysics physics;
    public void Walk(Vector2 moveDirection)
    {
        //if (!physics.isGround) return;

        if(moveDirection.magnitude == 0)
        {
            animator.SetFloat("Walk Axis X", 0.0f);
            animator.SetFloat("Walk Axis Z", 0.0f);
        }

        if (moveDirection.x != 0)
            animator.SetFloat("Walk Axis X", moveDirection.x);

        if (moveDirection.y != 0)
            animator.SetFloat("Walk Axis Z", moveDirection.y);

    }

    public bool ReturnAnimationEnd(string clipName, float endTime = 1.0f)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(clipName)
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= endTime)
            return true;

        else
            return false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        Walk(direction);
    }

}
