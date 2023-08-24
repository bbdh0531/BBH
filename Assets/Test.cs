using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public float Distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            Collider[] collider = Physics.OverlapSphere(transform.position, 5f, 1<<7);

            Debug.Log(collider[0].name);

            transform.rotation = Quaternion.LookRotation(collider[0].transform.position);

        }
    }
}
