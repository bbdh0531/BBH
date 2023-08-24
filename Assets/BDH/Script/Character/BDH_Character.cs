using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDH_Character : MonoBehaviour
{

    [Header("체력")]
    public int MaxHp;

    [Header("공격력")]
    public int MaxDamage;

    [Header("공격 사거리")]
    public float AttackDistance;

    [Header("이동속도")]
    public float MaxSpeed;

    [Space(10)]
    [Header("현재 캐릭터 상태")]
    [Space(4)]
    public int Hp;

    [Header("공격력")]
    public int Damage;

    [Header("이동속도")]
    public float Speed;

    [Header("점프")]
    public float JumpPower;

    [Header("죽었는가")]
    public bool isDie = false;

    [Header("땅에 있는가")]
    public bool isGround = false;

    [Header("점프")]
    public bool isJumpping = false;

    [Header("공격중 인가")]
    public bool isAttacking = false;

    BDH_PalyerAnimation animator;

    public void TakeDamage(int damage)
    {
        if(Hp <= 0)
        {
            isDie = true;

            animator.GetAnimator.SetBool("Die", true);

        }
        else
        {
            Hp -= Damage;
        }
    }

    void Start()
    {
        Hp = MaxHp;
        
        Damage = MaxDamage;
     
        Speed = MaxSpeed;

        animator = GetComponent<BDH_PalyerAnimation>();
    }

}

