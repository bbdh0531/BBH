using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BDH_PlayerCtrl : MonoBehaviour
{
    public KeyCode[] MoveForward;

    public KeyCode[] MoveRight;

    public KeyCode AttackKey;

    public KeyCode JumpKey;


    public int combo;
    
    public float AttackDelay;

    public float LastAttackTime;

    public float AttackTargetingSpeed;

    public float AttackTargetDistance;

    public float AttackLookAtSpeed;

    public Transform CameraPivot;

    public Transform MeshPivot;


    Transform tr;

    BDH_PalyerAnimation animator;

    BDH_Character character;

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


    Quaternion TargetLookAt(Transform point1 ,Transform point2)
    {

        Quaternion direction = Quaternion.LookRotation(point2.position - point1.position);

        direction.x = point1.rotation.x;

        direction.z = point1.rotation.z;

        return direction;
    }

    IEnumerator AttackTargeting ()
    {
        Debug.Log("Attack Start");

        Collider[] rangeInObject = Physics.OverlapSphere(tr.position, character.AttackDistance, 1 << 7);

        if (rangeInObject == null)// StopCoroutine("AttackTargeting");
            Debug.Log("NULL");

        Collider finalObject = rangeInObject[0];


        float range1 = Vector3.Distance(tr.position, finalObject.transform.position);

        for(int i = 0; i < rangeInObject.Length; i++)
        {

            float range2 = Vector3.Distance(tr.position, rangeInObject[i].transform.position);

            if(range1 > range2)
            {
                range1 = range2;

                finalObject = rangeInObject[i];
            }

        }

       // tr.rotation = LookAtAxisY(finalObject.transform.position);

        //CameraPivot.rotation = LookAtAxisY(finalObject.transform.position);

        Debug.Log(finalObject.name);

        Vector3 targetPosition = finalObject.transform.position;

        targetPosition.y = tr.position.y;

        while(Vector3.Distance(tr.position, finalObject.transform.position) >= AttackTargetDistance)
        {

            Debug.Log("Target Come up" + finalObject.name);

            tr.position = Vector3.Lerp(tr.position, targetPosition, Time.deltaTime * AttackTargetingSpeed);

            MeshPivot.rotation = TargetLookAt(MeshPivot, finalObject.transform);

            animator.Direction = Vector2.up;

            animator.GetAnimator.SetFloat("Walk Axis Z", 1.0f);

            yield return new WaitForEndOfFrame();
        
        }

        combo++;

        character.isAttacking = true;

        LastAttackTime = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

        character = GetComponent<BDH_Character>();

        animator =  GetComponent<BDH_PalyerAnimation>();

        MoveForward[0] = KeyCode.W;
        MoveForward[1] = KeyCode.S;

        MoveRight[0] = KeyCode.A;
        MoveRight[1] = KeyCode.D;

        AttackKey = KeyCode.Mouse0;

        JumpKey = KeyCode.Space; 

    }

    // Update is called once per frame
    void Update()
    {

        if (character.isDie || !character.isGround) return;

        #region Attack 
        if (AttackDelay <= LastAttackTime)
        {
            if(Input.GetKeyDown(AttackKey))
            {

                StartCoroutine(AttackTargeting());

            }
        }
        else
        {
            LastAttackTime += Time.deltaTime;
        }
        #endregion


        #region Move
        if (character.isAttacking) return;

        animator.Direction = new Vector2(ReturnAxis(MoveRight[0], MoveRight[1]),
            ReturnAxis(MoveForward[0], MoveForward[1]));

        if (ReturnAxis(MoveRight[0], MoveRight[1]) != 0 || ReturnAxis(MoveForward[0], MoveForward[1]) != 0)
        {
            MeshPivot.forward = MoveDirection();

            tr.position += MoveDirection() * character.Speed * Time.deltaTime;
        }

        #endregion

        if(Input.GetKeyDown(JumpKey))
        {
            character.isJumpping = true;

            tr.position += Vector3.up * Time.deltaTime * character.JumpPower;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("Hit");
            
            character.TakeDamage(coll.gameObject.GetComponent<BDH_Character>().Damage);
        }
    }

}
