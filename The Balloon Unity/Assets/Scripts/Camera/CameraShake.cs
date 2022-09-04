using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform thisTransform = null;

    public float shakeTime = 2.0f;
    public float shakeAmount = 3.0f;
    public float shakeSpeed = 2.0f;
    bool isShake = false;
    private void Start()
    {
        thisTransform = GetComponent<Transform>();
        DoShake();
    }
    
    public void DoShake()
    {
        if(isShake == false)
        {
            StartCoroutine(Shake());
            isShake = true;
        }
        else
        {
            StopCoroutine(Shake());
            StartCoroutine(Shake());
            isShake = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            DoShake();
        }
    }
    public IEnumerator Shake()
    {
        float elapsedTime = 0.0f;
        while(elapsedTime < shakeTime)
        {
            Vector3 origPos = thisTransform.localPosition;
            Vector3 randomPoint = origPos + Random.insideUnitSphere * shakeAmount;
            thisTransform.localPosition = Vector3.Lerp(thisTransform.localPosition, randomPoint, Time.deltaTime * shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        //thisTransform.localPosition = origPos;
        isShake = false;
    }

}
