using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    public bool load = false;
    public Vector3 playerPosition;
    public bool arcadeClicked = false;
}
