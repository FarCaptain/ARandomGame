using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLevel : MonoBehaviour
{
    public void GoTo(string levelName)
    {
        LevelManager.instance.LoadScene(levelName);
    }
}
