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
    public PotionSO CreateRequest(List<PotionSO> pedidos)
    { 
        //Cria indice para saber quantiade de pedidos na fila
        Dictionary<PotionSO, int> qtdPedidos = new Dictionary<PotionSO, int>();
        List<PotionSO> lista = listaPoc; 
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
        Debug.Log("Antes chamada System");
        System.Random valorSorteado = new System.Random();
        Debug.Log("Depois chamada System");
        Debug.Log(lista[valorSorteado.Next(0,lista.Count)]);
        return lista[valorSorteado.Next(0,lista.Count)];
    }
    
    private void Awake() 
    {
        pedidos.Add(CreateRequest(pedidos));
    }

    // Update is called once per frame
    IEnumerator CreateCall()
    {
        pedidos.Add(CreateRequest(pedidos));
        yield return new WaitForSeconds(1.3f);
    }

    void UpdateRequests(PotionSO pocao)
    {   
        pedidos.Remove(pocao);
        if(pedidos.Count<maximoPedidos)
        {
            StartCoroutine(CreateCall());
        }
    }
    private void Update() {
        if(pedidos.Count<maximoPedidos)
        {
            pedidos.Add(CreateRequest(pedidos));
        }
    }
}
