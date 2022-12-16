using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class target : MonoBehaviour
{
    public Vector2Int cord;
    public Vector2Int dir;
    public LightRays rays;
    public GameObject deathScreen;
    public GameObject winScreen;
    public BlackFold blackFold;

    public bool isEndTarget;

    public void SetEndTrigger(bool bis)
    {
        isEndTarget = bis;
    }

    public void OnTargetClicked()
    {
        rays.startLaser();
        blackFold.clearFold();

        if (isEndTarget)
        {
            winScreen.SetActive(true);
        }
        else
        {
            deathScreen.SetActive(true);
        }
    }
}
