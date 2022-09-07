using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    public GameObject player;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void wow()
    {
        ParticleManager.instance.PlayDashParticle(player, false);
        ParticleManager.instance.PlayParticle(player2, ParticleManager.ParticleType.ElecJump);
    }
}
