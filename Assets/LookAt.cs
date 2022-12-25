using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] Transform lookAtTarget;
    [SerializeField] float minYRotation;
    [SerializeField] float maxYRotation;
    private float oldYRotation;


    private void Start()
    {
        oldYRotation = transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        Vector3 lookAtVector = lookAtTarget.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookAtVector).normalized;

        Vector3 euler = targetRotation.eulerAngles;
        float yValue = Mathf.Clamp(GetEulerAngleInRange(euler.y - oldYRotation), minYRotation, maxYRotation);
        targetRotation = Quaternion.Euler(euler.x, GetEulerAngleInRange(yValue + oldYRotation), euler.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.02f);
    }

    private static float GetEulerAngleInRange(float angle)
    {
        angle %= 360f;
        angle = angle > 180f ? angle - 360f : angle;
        angle = angle < -180f ? angle + 360f : angle;

        return angle;
    }
}
