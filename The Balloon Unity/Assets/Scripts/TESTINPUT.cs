using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TESTINPUT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Onmove(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>().ToString());
    }
}
