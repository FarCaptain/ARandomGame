using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadStartSceneData : MonoBehaviour
{
    public GameData startData;

    public Transform player;
    public UnityEvent OnArcadeClicked;

    private void OnEnable()
    {
        LoadData();
    }

    public void LoadData()
    {
        if (!startData.load)
            return;

        player.position = startData.playerPosition;
        if (startData.arcadeClicked)
            OnArcadeClicked?.Invoke();
    }

    public void SaveData()
    {
        startData.load = true;

        startData.playerPosition = player.position;

        // this is not right but only when clicked can we switch scenes
        startData.arcadeClicked = true;
    }
}
