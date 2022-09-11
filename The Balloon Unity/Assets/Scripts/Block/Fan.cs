using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public WindArea area;
    public WindParticle particle;
    public Switch st;
    bool isOn = true;

    private void Start()
    {
        if (st != null)
        {
            ChangeState();
        }
        else
        {
            isOn = true;
            area.enabled = isOn;
            particle.enabled = isOn;
            particle.WindPref.Play();
        }
    }

    void Update()
    {
        if(st != null && isOn != st.turnSwitch)
        {
            ChangeState();
        }
    }

    void ChangeState()
    {
        isOn = st.turnSwitch;
        area.enabled = isOn;
        area.gameObject.SetActive(isOn);
        particle.enabled = isOn;
        if (isOn)
            particle.WindPref.Play();
        else
            particle.WindPref.Stop();
        
    }
}
