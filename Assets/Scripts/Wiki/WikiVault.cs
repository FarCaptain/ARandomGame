using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WikiVault", menuName = "Wiki/WikiVault")]
public class WikiVault : ScriptableObject
{
    public List<Wiki> wikiList = new List<Wiki>();

    public void LoadAllWiki()
    {
        var allWiki = Resources.LoadAll("Wikis", typeof(Wiki));
        wikiList.Clear();
        foreach (var wiki in allWiki)
        {
            wikiList.Add(wiki as Wiki);
        }
    }
}
