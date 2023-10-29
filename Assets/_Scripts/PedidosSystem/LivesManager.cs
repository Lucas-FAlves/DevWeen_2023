using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    public static Action OnRequestFailed;
    private int currentLives = 3;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        OnRequestFailed += FailedRequest;
    }
    private void OnDisable()
    {
        OnRequestFailed -= FailedRequest;
    }

    private void FailedRequest()
    {
        currentLives--;
        textComponent.text = "Lives = " + currentLives;
        if (currentLives < 0)
            Debug.Log("fuck");
    }
}
