using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    private PotionSO[] allPotionsSO;
    [SerializeField] private GameObject requestPrefab;

    [SerializeField] private int maxRequests = 5;
    private int currentRequests = 0;

    [SerializeField] private float timeBetweenRequests = 15f;
    private float currentTime = 0f;
    private void Awake()
    {
        allPotionsSO = Resources.LoadAll<PotionSO>("PotionsSO");
    }

    private void Update()
    {
        if (currentRequests >= maxRequests)
            return;

        currentTime += Time.deltaTime;

        if (currentTime < timeBetweenRequests)
            return;

        Request();
    }

    private void Request()
    {
        currentTime = 0;
        var rng = Random.Range(0, allPotionsSO.Length);
        var potionSO = allPotionsSO[rng];
        var pedidoClone = Instantiate(requestPrefab, transform);
        pedidoClone.GetComponent<Request>().StartRequest(potionSO);
        currentRequests++;
    }

}
