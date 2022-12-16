using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    public Transform spawnPosition;

    public void Respawn()
    {
        transform.position = spawnPosition.position;
    }
}
