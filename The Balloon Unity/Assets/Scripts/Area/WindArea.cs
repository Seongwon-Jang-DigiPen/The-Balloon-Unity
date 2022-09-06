using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    public GameObject angleObj;
    public float windForce = 1.0f;
    Vector3 objLocalEulerAngle;
    Vector2 angle;

    private void Start()
    {
        objLocalEulerAngle = angleObj.transform.localEulerAngles;
        angle = new Vector2(Mathf.Cos(objLocalEulerAngle.z * Mathf.Deg2Rad), Mathf.Sin(objLocalEulerAngle.z * Mathf.Deg2Rad)).normalized;
    }

    private void Update()
    {
        if(angleObj.transform.localEulerAngles != objLocalEulerAngle)
        {
            objLocalEulerAngle = angleObj.transform.localEulerAngles;
            angle = new Vector2(Mathf.Cos(objLocalEulerAngle.z * Mathf.Deg2Rad), Mathf.Sin(objLocalEulerAngle.z * Mathf.Deg2Rad)).normalized;
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag(Player.playerTag) && collision.GetComponent<Player>().balloonState.state == BALLOONSTATE.WATER)
        {
            return;
        }
        collision.GetComponent<Rigidbody2D>().AddForce(angle * windForce, ForceMode2D.Force);
    }
}
