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
    private PotionSO currentPotionRequest;
    public PotionSO CurrentPotionRequest => currentPotionRequest;

    public void StartRequest(PotionSO potionSO)
    {
        currentPotionRequest = potionSO;
        potionImage.sprite = potionSO.potionSprite;
        indredientsImage[0].sprite = potionSO.ingredients[0].sprite;
        indredientsImage[1].sprite = potionSO.ingredients[1].sprite;
        indredientsImage[2].sprite = potionSO.ingredients[2].sprite;
        slider.maxValue = potionSO.reqTime;
        slider.value = slider.maxValue;
        currentTime = potionSO.reqTime;
        start = true;
    }

    private void Update()
    {
        if (!start)
            return;

        slider.value -= Time.deltaTime;
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            LivesManager.OnRequestFailed?.Invoke();
            RequestManager.OnRequestFailed?.Invoke();
            Destroy(this.gameObject);
        }


    }


}
