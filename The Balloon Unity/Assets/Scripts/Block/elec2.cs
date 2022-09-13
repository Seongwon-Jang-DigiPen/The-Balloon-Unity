using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elec2 : MonoBehaviour
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
            transform.position = new Vector3(63.5f, 10.5f, 0);
        }
    }
}
