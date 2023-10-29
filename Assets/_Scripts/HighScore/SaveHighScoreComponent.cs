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
        maxScoreComponent.text = "Texto Max Score: " + SaveHighScore.GetMaxScore();
        lastScoreComponent.text = "Texto Last Score: " + SaveHighScore.GetLastScore();
    }
}
