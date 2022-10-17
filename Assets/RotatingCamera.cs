using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    float multifier = 5f;
    bool startUpdate = false;
    private void Update()
    {
        if(startUpdate)
            transform.Rotate(Vector3.up * multifier * Time.deltaTime);
    }

    public void startRotation()
    {
        if (startUpdate)
            return;

        transform.rotation = Quaternion.identity;
        startUpdate = true;
    }

    public void stopRotation()
    {
        if (!startUpdate)
            return;

        startUpdate = false;
        transform.rotation = Quaternion.identity;
    }
}
