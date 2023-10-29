using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class RequestManager : MonoBehaviour
{
    private PotionSO[] allPotionsSO;
    [SerializeField] private GameObject requestPrefab;

    [SerializeField] private int maxRequests = 5;
    private int currentRequests = 0;

    [SerializeField] private float timeBetweenRequests = 15f;
    private float currentTime = 0f;

    private static List<Request> requests = new List<Request>();

    public static Action OnRequestFailed;

    private void Awake()
    {
        allPotionsSO = Resources.LoadAll<PotionSO>("PotionsSO");
    }

    private void OnEnable()
    {
        OnRequestFailed += RequestFailed;
    }
    private void OnDisable()
    {
        OnRequestFailed -= RequestFailed;
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
        var rng = UnityEngine.Random.Range(0, allPotionsSO.Length);
        var potionSO = allPotionsSO[rng];
        var pedidoClone = Instantiate(requestPrefab, transform);
        requests.Add(pedidoClone.GetComponent<Request>());
        pedidoClone.GetComponent<Request>().StartRequest(potionSO);
        currentRequests = transform.childCount;
    }

    public static PotionSO DeliverRequest(PotionSO potionSO)
    {
        foreach (var request in requests)
        {
            if(request.CurrentPotionRequest == potionSO)
            {
                ScoreManager.OnPotionScore?.Invoke(potionSO.Score);
                Debug.Log("potion " + potionSO.id);
                break;
            }
        }
        //Invoke
        return requests.ElementAt(0).CurrentPotionRequest;
    }

    public void RequestFailed()
    {
        currentRequests = transform.childCount;
    }


}
