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
    private static int currentRequests = 0;

    [SerializeField] private float timeBetweenRequests = 15f;
    private float currentTime = 0f;

    private static List<Request> requests = new List<Request>();

    public static Action OnRequestFailed;
    public static Action<int> OnPotionDelivered;

    private void Awake()
    {
        allPotionsSO = Resources.LoadAll<PotionSO>("PotionsSO");
    }

    private void OnEnable()
    {
        OnRequestFailed += RequestFailed;
        OnPotionDelivered += DeliverRequest;
    }
    private void OnDisable()
    {
        OnRequestFailed -= RequestFailed;
        OnPotionDelivered -= DeliverRequest;
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

    public void DeliverRequest(int id)
    {
        int i = 0;
        PotionSO potionSO = null;
        foreach(var potion in allPotionsSO)
        {
            if(potion.id == allPotionsSO[i].id)
            {
                potionSO = potion;
                break;
            }
            i++;
        }
        foreach (var request in requests)
        {
            if(request.CurrentPotionRequest.Equals(potionSO))
            {
                ScoreManager.OnPotionScore?.Invoke(potionSO.Score);
                AudioManager.instance.PlaySound("entrega");
                requests.Remove(request);
                Destroy(request);
                break;
            }
        }
    }

    public void RequestFailed()
    {
        currentRequests = transform.childCount;
        AudioManager.instance.PlaySound("falha");
    }


}
