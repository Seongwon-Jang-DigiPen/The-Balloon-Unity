using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject target;
    public float CameraSpeedScale = 2.0f;
    void Update()
    {
         
    }

    private void LateUpdate()
    {
        Vector3 cameraPos = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * CameraSpeedScale);
        cameraPos.z = transform.position.z;
        transform.position = cameraPos;
    }
}
