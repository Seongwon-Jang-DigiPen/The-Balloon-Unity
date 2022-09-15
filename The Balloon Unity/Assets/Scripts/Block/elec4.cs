using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elec4 : MonoBehaviour
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
            transform.position = new Vector3(31.5f, 67.5f, 0);
        }
    }
}
