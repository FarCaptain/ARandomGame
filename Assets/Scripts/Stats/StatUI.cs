using UnityEngine;
using TMPro;

public class StatUI : MonoBehaviour
{
    enum StatType
    {
        Strength,
        Coordination,
        Intelegence,
        Perception,
    }

    private TextMeshProUGUI text;
    [SerializeField] private StatType statType;
    void Start()
    {
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();

        UpdateStatUI();
        PlayerStats.instance.onPlayerStatsChanged += UpdateStatUI;
    }

    // could be better
    private void UpdateStatUI()
    {
        Stat stat = null;
        switch (statType)
        {
            case StatType.Strength:
                stat = PlayerStats.instance.strength;
                break;
            case StatType.Coordination:
                stat = PlayerStats.instance.coordination;
                break;
            case StatType.Intelegence:
                stat = PlayerStats.instance.intelligence;
                break;
            case StatType.Perception:
                stat = PlayerStats.instance.perception;
                break;
        }
        if(stat != null)
            text.SetText(stat.GetValue().ToString());
    }

    private void OnDestroy()
    {
        PlayerStats.instance.onPlayerStatsChanged -= UpdateStatUI;
    }
}
