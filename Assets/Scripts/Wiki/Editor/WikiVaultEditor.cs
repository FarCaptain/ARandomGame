using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WikiVault), editorForChildClasses: true)]
public class WikiVaultEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // don't use this expensive function in runtime
        GUI.enabled = !Application.isPlaying;

        WikiVault wv = target as WikiVault;
        if (GUILayout.Button("GrabAll"))
            wv.LoadAllWiki();
    }
}
