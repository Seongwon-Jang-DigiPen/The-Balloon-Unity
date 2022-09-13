using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField]    
    GameObject treeBody;

    private void Start()
    {
        treeBody.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("WaterBomb"))
        {
            treeBody.SetActive(true);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
