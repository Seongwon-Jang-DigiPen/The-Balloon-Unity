using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BALLOONSTATE
{
    Flat, NORMAL, WATER, ELECTRIC
}
public class BalloonState : MonoBehaviour
{
    public BALLOONSTATE state;
    public Rigidbody2D rigid;
    public Animator animator;
    public float MaxSpeed = 10.0f;
    public float acceleration = 3.0f;
    public float decceleration = 3.0f;
    public float MaxDownSpeed = 4f;
    public float MaxDownFastSpeed = 7f;
    public float jumpForce;
}
