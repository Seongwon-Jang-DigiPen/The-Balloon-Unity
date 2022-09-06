using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    public ParticleSystem par;

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
        Vector3 wow = new Vector3(2, 0, 0);
        ParticleManager.instance.PlayParticle(wow, ParticleManager.ParticleType.ElecJump);
    }
}
