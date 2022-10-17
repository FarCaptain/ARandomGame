using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wiki", menuName = "Wiki/Wiki")]
public class Wiki : ScriptableObject
{
    // Name must be unique
    // Link tag would be Wiki:Name
    public string Name;
    [TextArea]
    public string discription;
}
