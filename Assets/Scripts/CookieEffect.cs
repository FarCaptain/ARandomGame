using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieEffect : MonoBehaviour
{
    private new SkinnedMeshRenderer renderer;
    public float lowValue = 0.4f;

    private float duration;
    private float accumulatedTime = 0f;

    private void Start()
    {
        renderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
    }
    public void SetTransparency(float _duration)
    {
        renderer.material.SetFloat("_Opacity", lowValue);
        duration = _duration;

        gameObject.layer = LayerMask.NameToLayer("Invisable");
    }

    private void Update()
    {
        if (duration > 0f)
        {
            accumulatedTime += Time.deltaTime;
            if (accumulatedTime >= duration)
            {
                duration = 0f;
                accumulatedTime = 0f;
                renderer.material.SetFloat("_Opacity", 1);
                gameObject.layer = LayerMask.NameToLayer("Player");
            }
        }
    }
}
