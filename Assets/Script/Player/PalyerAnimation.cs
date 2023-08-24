using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PalyerAnimation : MonoBehaviour
{


    public float NextAttackTiming;

    public Vector2 Direction;

    Animator animator;

    Character character;

    PlayerCtrl playerCtrl;

    public bool IsAnimationClipEnd(string clipName, float endTime = 1.0f)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(clipName)
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= endTime)
            return true;
        else
            return false;
    }

    public void Die()
    {
        animator.SetTrigger("Die");

        character.isDie = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();  

        animator = GameObject.Find("Mesh").GetComponent<Animator>();

        playerCtrl = GetComponent<PlayerCtrl>();    
    }

    // Update is called once per frame
    void Update()
    {
        #region Landing, Fall
        if (character.isGround) animator.SetBool("Is Touch Ground", true);

        else animator.SetBool("Is Touch Ground", false);
        #endregion


        #region Attack
        if (character.isAttacking)
        {
            if (playerCtrl.combo == 1)
            {
                animator.SetInteger("Attack Key", 0);
                animator.SetBool("Attacking", true);
            }

            if (playerCtrl.combo == 2 && IsAnimationClipEnd("Attack 1", NextAttackTiming))
                animator.SetInteger("Attack Key", 1);

            if (playerCtrl.combo == 3 && IsAnimationClipEnd("Attack 2", NextAttackTiming))
                animator.SetInteger("Attack Key", 2);
                
            if(playerCtrl.combo == 4 && IsAnimationClipEnd("Attack 3", NextAttackTiming))
            {
                Debug.Log("Combo 3");

                animator.SetInteger("Attack Key", 0);
                
                playerCtrl.combo = 0;
            }


            if(IsAnimationClipEnd($"Attack {playerCtrl.combo}", 1.0f))
            {
                animator.SetBool("Attacking", false);

                character.isAttacking = false;

                playerCtrl.combo = 0;
            }

            return;
        }
        #endregion


        #region Walk

        if (Direction.magnitude == 0)
        {
            animator.SetFloat("Walk Axis X", 0.0f);

            animator.SetFloat("Walk Axis Z", 0.0f);

            return;
        }

        if (Direction.x != 0) animator.SetFloat("Walk Axis X", Direction.x);

        if (Direction.y != 0) animator.SetFloat("Walk Axis Z", Direction.y);

        #endregion
    }
}
