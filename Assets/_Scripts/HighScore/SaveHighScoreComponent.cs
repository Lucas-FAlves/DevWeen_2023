using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveHighScoreComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI maxScoreComponent;
    [SerializeField] private TextMeshProUGUI lastScoreComponent;
    void Start()
    {
        maxScoreComponent.text = "Maior Pontuação: " + SaveHighScore.GetMaxScore();
        lastScoreComponent.text = "Pontuação Atual: " + SaveHighScore.GetLastScore();
    }
}
