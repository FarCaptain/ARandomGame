using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveData : MonoBehaviour
{
    // keep the reference
    public GameData returnSceneData;

    private void Start()
    {
        returnSceneData.load = true;
    }
}
