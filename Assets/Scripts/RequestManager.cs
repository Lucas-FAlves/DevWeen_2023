using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    // Start is called before the first frame update
    PotionSO[] pedidos;
    RequestGenerator generator;
    int lenPed;
    void Start()
    {
        generator = gameObject.GetComponent<RequestGenerator>();
        pedidos = pedidos.Append(generator.CreateRequest(pedidos)).ToArray();
    }

    // Update is called once per frame
    IEnumerator CreateCall()
    {
        pedidos = pedidos.Append(generator.CreateRequest(pedidos)).ToArray();
        yield return new WaitForSeconds(1.3f);
    }
    void Update()
    {
        if(pedidos.Length<lenPed)
        {
            StartCoroutine(CreateCall());
        }
    }
}
