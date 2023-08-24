using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BDH_CharacterPhysics : MonoBehaviour
{

    public float GravityScale = 0.1f;

    public float LandingDistance;

    Transform root;

    float currentGravityPower;

    Vector3 velocity;

    Transform tr;

    Animator animator;

    BDH_Character character;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

        character = GetComponent<BDH_Character>();  

        root = GameObject.Find("Root").GetComponent<Transform>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        RaycastHit hit;

        if (Physics.Raycast(root.position, -tr.up, out hit, LandingDistance, 1 << 8))
        {

            currentGravityPower = 0.0f;

            Debug.Log("A");

            character.isGround = true;


           // animator.SetBool("is Touch Ground", true);

        }
        else
        {
            velocity = tr.position;

            currentGravityPower += Mathf.Abs(9.8f) * GravityScale * Time.deltaTime;

            velocity.y -= currentGravityPower;

            tr.position = velocity;

            character.isGround = false;

            // animator.SetBool("is Touch Ground", false);

        }

        Debug.DrawRay(root.position, -tr.up * LandingDistance, Color.green);

    }
}
