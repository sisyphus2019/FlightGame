using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetPlanePosition;
    public float smoothSpeed = 0.3f;
    Vector3 speed;

    private void Start() {
        //init();
    }

    public void init()
    {
        //Debug.Log("CameraFollow-init");
        GameObject plane = GameObject.FindGameObjectWithTag("plane");
        targetPlanePosition = plane.transform.Find("CameraFollowPosition");
    }
    private void LateUpdate() {
        if(targetPlanePosition.position.y > transform.position.y)
        {
            Vector3 targetPosition = new Vector3(transform.position.x , transform.position.y , targetPlanePosition.position.z);
            transform.position = Vector3.SmoothDamp(transform.position,targetPosition, ref speed, smoothSpeed * Time.deltaTime);
        }
    }
}
