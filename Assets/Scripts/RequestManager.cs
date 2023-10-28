using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RequestManager : MonoBehaviour
{
    [Header("Poções")]
    public List<PotionSO> listaPoc;

    [Header("Configurações dos pedidos")]
    public List<PotionSO> pedidos;
    public int maximoPedidos,maximoRepeticao;
    public float delayPedidos;
    private float auxDelay;

    public PotionSO CreateRequest(List<PotionSO> pedidos)
    {   
        //Cria indice para saber quantiade de pedidos na fila
        Dictionary<PotionSO, int> qtdPedidos = new Dictionary<PotionSO, int>();
        List<PotionSO> lista = new List<PotionSO>(listaPoc); 
        foreach (PotionSO elemento in pedidos)
        {
            if (qtdPedidos.ContainsKey(elemento) == false)
            {
                qtdPedidos.Add(elemento, 0);
            }
            qtdPedidos[elemento] = qtdPedidos[elemento]+1;
            if(qtdPedidos[elemento] >= maximoRepeticao & lista.Contains(elemento))
            {
                lista.Remove(elemento);
            }
        }
        //Define próximo pedido
        System.Random valorSorteado = new System.Random();
        return lista[valorSorteado.Next(0,lista.Count)];
    }
    
    private void Awake() 
    {   
        auxDelay = delayPedidos;
        pedidos.Add(CreateRequest(pedidos));
    }

    // Update is called once per frame
    IEnumerator CreateCall()
    {
        pedidos.Add(CreateRequest(pedidos));
        yield return new WaitForSeconds(1f);
    }

    private void Update() 
    {   
        auxDelay -= Time.time * Time.deltaTime;
        if(pedidos.Count<maximoPedidos & auxDelay<=0)
        {
            auxDelay = delayPedidos;
            StartCoroutine(CreateCall());
        }
    }
}