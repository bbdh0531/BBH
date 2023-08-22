using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode[] MoveForward;

    public KeyCode[] MoveRight;

    public KeyCode AttacKey;


    public int MaxCombpo;

    public float speed;

    public float AttackPushDelay;

    public float AttackPushLast;

    public float AttackDistance = 3f;

    public float AutoAttackTargetSpeed = 2f;

    public Transform CameraPivot;

    public Transform MeshPivot;

    bool isAttack = false;

    int combo;

    Transform tr;

    AnimationSystem animationSystem;



    float ReturnAxis(KeyCode k1, KeyCode k2)
    {
        if (Input.GetKey(k1) && Input.GetKey(k2)) return 0.0f;

        if (Input.GetKey(k1)) return 1.0f;

        if (Input.GetKey(k2)) return -1.0f;

        return 0.0f;
    }

    Vector3 MoveDirection()
    {
        return new Vector3(CameraPivot.forward.x, 0.0f, CameraPivot.forward.z) * ReturnAxis(MoveForward[0], MoveForward[1])
            + new Vector3(CameraPivot.right.x, 0.0f, CameraPivot.right.z) * ReturnAxis(MoveRight[0], MoveRight[1]);
    }

    IEnumerator TargetPointMove(Vector3 targetPoint, float speed = 1.0f, float distance = 1.0f)
    {
        while(Vector3.Distance(tr.position, targetPoint) > distance)
        {

            Debug.Log("A");
            tr.position = Vector3.Lerp(tr.position, targetPoint, Time.deltaTime * speed);

            yield return new WaitForEndOfFrame();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

        MoveForward[0] = KeyCode.W;
        MoveForward[1] = KeyCode.S;

        MoveRight[0] = KeyCode.A;
        MoveRight[1] = KeyCode.D;

    }

    // Update is called once per frame
    void Update()
    {

        if (isAttack) return;

        MeshPivot.forward = MoveDirection();

        tr.position += MoveDirection() * speed * Time.deltaTime;
        
        /*animationSystem.Walk(new Vector2(ReturnAxis(MoveRight[0], MoveRight[1]),
            ReturnAxis(MoveForward[0], MoveForward[1])));*/

        if (AttackPushLast > AttackPushDelay)
        {
            if(Input.GetKeyDown(AttacKey))
            {
                Debug.Log("Attack");
                
                AttackPushLast = 0.0f;

                Collider[] attackCanObject = Physics.OverlapSphere(tr.position, AttackDistance, 1 << 7);

                if (attackCanObject == null) return;

                Collider closeObject = attackCanObject[0];

                float distance1 = Vector3.Distance(tr.position, closeObject.transform.position);

                for (int i = 0; i < attackCanObject.Length; i++)
                {
                    float distance2 = Vector3.Distance(tr.position, attackCanObject[i].transform.position);

                    if(distance1 > distance2)
                    {
                        distance1 = distance2;
                        closeObject = attackCanObject[i];
                    }
                }

                Debug.Log(closeObject.name);

                //tr.position = Vector3.Lerp(tr.position, closeObject.transform.position, Time.deltaTime * AutoAttackTargetSpeed);
                StartCoroutine(TargetPointMove(closeObject.transform.position, AutoAttackTargetSpeed));

            }
        }
        else
        {
            AttackPushLast += Time.deltaTime;
        }
    }

}
