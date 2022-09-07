using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager m_instance;
    public static ParticleManager instance
    {
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<ParticleManager>();
            return m_instance;
        }
    }

    public enum ParticleType
    {
        Jump,
        Flat,
        WaterJump,
        ElecJump,
        IntoWater,
        Dash,
    }

    public ParticleSystem JumpPref;
    public ParticleSystem FlatJumpPref;
    public ParticleSystem WaterJumpPref;
    public ParticleSystem ElecJumpPref;
    public ParticleSystem WaterPref;

    public ParticleSystem DashPref;

    private GameObject player;
    private ParticleSystem Dash;

    public void PlayParticle(Vector3 pos, ParticleType particletype)
    {
        ParticleSystem temp = JumpPref;
        switch(particletype)
        {
            case ParticleType.Flat:
                temp = FlatJumpPref;
                break;
            case ParticleType.WaterJump:
                temp = WaterJumpPref;
                break;
            case ParticleType.ElecJump:
                temp = ElecJumpPref;
                break;
            case ParticleType.IntoWater:
                temp = WaterPref;
                break;
        }
        ParticleSystem effect = Instantiate(temp, pos, Quaternion.identity);
        effect.Play();
    }

    public void PlayDashParticle(GameObject gameobject, bool right)
    {
        player = gameobject;
        Vector3 pos = new Vector3(0, 0, 0);
        Dash = Instantiate(DashPref, pos, Quaternion.identity);
        if (right == true)
        {
            var shape = Dash.shape;
            Vector3 rot = new Vector3(0, 0, -36);
            shape.rotation = rot;
        }
        Dash.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Dash != null)
        {
            var shape = Dash.shape;
            Vector3 temp = player.GetComponent<Renderer>().bounds.center;
            shape.position = temp;
        }
    }
}
