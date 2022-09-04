using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerControl
{
    [Header("Action")]
    [Header("Dash")]
    public float dashTime = 1.0f;
    public float DashForce = 10.0f;
    [Header("GetAir")]
    public float getAirTime = 3.0f;
    [Header("Sprinkle")]
    public GameObject WaterBomb;
    public float sprinkleTime = 2.0f;

    private bool IsDoAction = false;
    void DoAction() 
    {
        switch (player.balloonState.state)
        {
            case BALLOONSTATE.Flat:
                GetAir();
                break;
            case BALLOONSTATE.NORMAL:
                Dash();
                break;
            case BALLOONSTATE.WATER:
                Sprinkle();
                break;
        }
    }

    void GetAir()
    {
        if (isTouchingGround == true)
        {
            StartCoroutine(IGetAir());
        }
    }
    IEnumerator IGetAir()
    {
        IsDoAction = true;
        playerRb.velocity = new Vector3(0, 0);
        yield return new WaitForSeconds(getAirTime);
        if (IsDoAction == true)
        {
            player.ChangeState(BALLOONSTATE.NORMAL);
            IsDoAction = false;
        }
    }

    void Dash()
    {
        StartCoroutine(IDash());
    }

    IEnumerator IDash()
    {
        IsDoAction = true;

        //playerRb.AddForce(Vector2.right * transform.localScale.x * DashForce, ForceMode2D.Impulse);
        playerRb.velocity = new Vector3(transform.localScale.x * DashForce, 0);
        float gravity = playerRb.gravityScale;
        playerRb.gravityScale = 0;
        yield return new WaitForSeconds(dashTime);
        playerRb.gravityScale = gravity;
        if (IsDoAction == true)
        {
            playerRb.velocity = new Vector3(0,0);
            player.ChangeState(BALLOONSTATE.Flat);
            IsDoAction = false;
        }
    }
    void Sprinkle() { StartCoroutine(ISprinkle()); }
    IEnumerator ISprinkle()
    {
        IsDoAction = true;
        yield return new WaitForSeconds(sprinkleTime);
        IsDoAction = false;
    }
}
