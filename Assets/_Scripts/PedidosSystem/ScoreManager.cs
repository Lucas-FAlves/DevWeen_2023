using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static Action<float> OnPotionScore;
    private TextMeshProUGUI textComponent;
    private float currentPoints = 0;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        OnPotionScore += ScorePoints;
    }

    private void OnDisable()
    {
        OnPotionScore -= ScorePoints;
    }

    private void ScorePoints(float points)
    {
        currentPoints += points;
        SaveHighScore.SetLastScore(currentPoints);
        textComponent.text = "Points: " + currentPoints.ToString("0.00");
    }

}
