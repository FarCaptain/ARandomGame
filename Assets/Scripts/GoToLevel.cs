using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLevel : MonoBehaviour
{
    // this class is used to retrieve the singleton after switching scenes
    public void GoTo(string levelName)
    {
        LevelManager.instance.LoadScene(levelName);
    }

    public void ShowClickStartPanel() => LevelManager.instance.ShowClickStartPanel();
    public void HiddeClickStartPanel() => LevelManager.instance.HiddeClickStartPanel();
}
