using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BDH_CameraController : MonoBehaviour
{

    public float MouseSensitivity;

    public float CameraDistanceMin; 
    public float CameraDistanceMax;

    public float CameraFollowSpeed;

    public Transform RootPivot;
    public Transform RealCameraPivot;

    public float distance;


    Vector3 cameraDirection;

    Transform tr;

    Vector2 ReturnMouse()
    {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * MouseSensitivity;
    }

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CameraRotation = tr.rotation.eulerAngles;

        float x = CameraRotation.x - ReturnMouse().y;

        if (x < 180) x = Mathf.Clamp(x, -1f, 70f);
        else x = Mathf.Clamp(x, 355f, 361f);

        tr.rotation = Quaternion.Euler(x, CameraRotation.y + ReturnMouse().x, CameraRotation.z);
    }

    void LateUpdate()
    {

        //Vector3 direction = RootPivot.position - RealCameraPivot.position;

        //Debug.DrawRay(RealCameraPivot.position, direction, Color.green);

        //RaycastHit hit;


        //if(Physics.Raycast(RealCameraPivot.position, direction, out hit))
        //{

        //    if (hit.collider.gameObject.layer == 6)
        //        distance = CameraDistanceMax;
        //    else
        //        distance = Mathf.Clamp(hit.distance, CameraDistanceMin, CameraDistanceMax);

        //}
    }
}
