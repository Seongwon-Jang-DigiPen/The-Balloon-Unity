using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticle : MonoBehaviour
{
    public ParticleSystem WindPref;
    // Start is called before the first frame update
    void Start()
    {
        GameObject area = transform.GetChild(0).gameObject;
        GameObject angle = transform.GetChild(1).gameObject;
        Vector3 size = new Vector3(0.5f, area.transform.localScale.y - 1, 0); ;
        if (area.transform.localScale.x > 4.5)
        {
            size = new Vector3(area.transform.localScale.x - 4, area.transform.localScale.y - 1, -3);
        }
        Vector3 center = area.GetComponent<Renderer>().bounds.center;
        Vector3 pos = new Vector3(center.x - (area.transform.localScale.x - size.x) / 2, center.y - 0.4f, -3);
        if (angle.transform.localEulerAngles.z >= 180)
        {
            pos = new Vector3(center.x + (area.transform.localScale.x - size.x) / 2, center.y - 0.4f, 0);
        }

        ParticleSystem Wind = Instantiate(WindPref, pos, Quaternion.identity);
        Wind.transform.RotateAround(area.transform.position, new Vector3(0, 0, 1), area.transform.localEulerAngles.z);
        var shape = Wind.shape;
        shape.scale = size;
        var emit = Wind.emission;
        emit.rateOverTime = area.transform.localScale.x * area.transform.localScale.y / 30;
        if (angle.transform.localEulerAngles.z >= 180)
        {
            Wind.transform.localScale = new Vector3(-1, 1, 1);
        }
        Wind.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
