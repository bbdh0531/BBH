using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDH_Character : MonoBehaviour
{

    [Header("ü��")]
    public int MaxHp;

    [Header("���ݷ�")]
    public int MaxDamage;

    [Header("���� ��Ÿ�")]
    public float AttackDistance;


    [Header("�̵��ӵ�")]
    public float MaxSpeed;


    [Space(10)]
    [Header("���� ĳ���� ����")]
    [Space(4)]
    public int Hp;

    [Header("���ݷ�")]
    public int Damage;

    [Header("�̵��ӵ�")]
    public float Speed;

    [Header("����")]
    public float JumpPower;

    [Header("�׾��°�")]
    public bool isDie = false;

    [Header("���� �ִ°�")]
    public bool isGround = false;

    [Header("����")]
    public bool isJumpping = false;

    [Header("������ �ΰ�")]
    public bool isAttacking = false;

    void Start()
    {
        Hp = MaxHp;
        
        Damage = MaxDamage;
     
        Speed = MaxSpeed;
    }

}
