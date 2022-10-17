using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOutlineSetting", menuName = "Data/OutlineSettings")]
public class OutlineSetting : ScriptableObject
{
    public Color itemColor;
    public Color evidenceColor;
    public Color tooltipedColor;
    public Color interactableColor;
}

public enum outlinedObjectType { Item, Evidence, Tooltiped, Interactable }