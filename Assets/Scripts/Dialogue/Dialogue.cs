using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "DialogueObject")]
public class Dialogue : ScriptableObject
{
    [TextArea]
    public List<string> sentences;

    public Sphinx.GameEvent postDialogueEvent;
}
