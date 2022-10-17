using UnityEngine;
using TMPro;
using System;

public class CharacterBasicsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthValueText;
    [SerializeField] private TextMeshProUGUI faceValueText;

    private void Start()
    {
        PlayerStats.instance.onCharacterBasicsChanged += updateUI;
        healthValueText.text = PlayerStats.instance.maxHealth.ToString();
        faceValueText.text = PlayerStats.instance.maxFace.ToString();
    }

    private void OnDestroy()
    {
        PlayerStats.instance.onCharacterBasicsChanged -= updateUI;
    }

    private void updateUI()
    {
        healthValueText.text = PlayerStats.instance.currentHealth.ToString();
        faceValueText.text = PlayerStats.instance.currentFace.ToString();
    }
}
