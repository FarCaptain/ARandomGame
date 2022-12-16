using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    float multifier = 5f;
    float axisY = 0f;
    bool startUpdate = false;

    private void OnEnable()
    {
        startRotation();
    }

    private void OnDisable()
    {
        stopRotation();
    }

    public void startRotation()
    {
        axisY = 0f;
        startUpdate = true;
    }

    public void stopRotation()
    {
        startUpdate = false;
        axisY = 0f;
    }

    private void FixedUpdate()
    {
        if (startUpdate)
        {
            axisY = (axisY + Time.deltaTime * multifier) % 360f;
            transform.localEulerAngles = new Vector3(0f, axisY, 0f);
        }
    }
}
