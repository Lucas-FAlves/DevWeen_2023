using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Request : MonoBehaviour
{
    [SerializeField] private Image potionImage;
    [SerializeField] private Image[] indredientsImage;
    [SerializeField] private Slider slider;
    private bool start = false;

    private float currentTime;

    public void StartRequest(PotionSO potionSO)
    {
        potionImage.sprite = potionSO.potionSprite;
        indredientsImage[0].sprite = potionSO.ingredients[0].sprite;
        indredientsImage[1].sprite = potionSO.ingredients[1].sprite;
        indredientsImage[2].sprite = potionSO.ingredients[2].sprite;
        slider.maxValue = potionSO.reqTime;
        currentTime = potionSO.reqTime;
        start = true;
    }

    private void Update()
    {
        if (!start)
            return;

        slider.value -= Time.deltaTime;


    }


}
