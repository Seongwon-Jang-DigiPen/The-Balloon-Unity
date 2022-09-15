using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elec3 : MonoBehaviour
{
    public GameObject touch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touch.GetComponent<RedBlueSwitch>().touched == false)
        {
            transform.position = new Vector3(115.5f, 78.5f, 0);
        }
    }
}
