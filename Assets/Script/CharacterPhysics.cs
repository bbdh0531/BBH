using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysics : MonoBehaviour
{

    public float GravityScale = 0.1f;

    public float LandingDistance;

    public bool isGround { get { return isGround; } private set { isGround = value; } }

    public Transform CharacterPivot;


    float currentGravityPower;

    Vector3 velocity;

    Transform tr;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        RaycastHit hit;

        if (Physics.Raycast(CharacterPivot.position, -tr.up, out hit, LandingDistance, 1 << 8))
        {
            currentGravityPower = 0.0f;

            isGround = true;

            animator.SetBool("is Touch Ground", true);

        }
        else
        {
            velocity = tr.position;

            currentGravityPower += Mathf.Abs(9.8f) * GravityScale * Time.deltaTime;

            velocity.y -= currentGravityPower;

            tr.position = velocity;

            isGround = false;

            animator.SetBool("is Touch Ground", false);

        }

        Debug.DrawRay(CharacterPivot.position, -tr.up * LandingDistance, Color.green);

    }
}
