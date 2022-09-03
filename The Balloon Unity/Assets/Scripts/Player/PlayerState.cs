using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator animator;
    public float speed;
    public float jumpForce;
    public IAction action = new ActionNothing();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
