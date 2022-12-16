using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneThemeSongSwitch : MonoBehaviour
{
    [SerializeField] private string themeName;
    private void Start()
    {
        AudioManager.instance.StopAll();
        AudioManager.instance.Play(themeName);
    }
}
