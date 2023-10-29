using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RequestManager : MonoBehaviour
{
    [Header("Poções")]
    public List<PotionSO> listaPoc;

    private Dictionary<PotionSO, float> timerPedidos = new Dictionary<PotionSO, float>();
    
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
        PotionSO pocao = CreateRequest(pedidos);
        pedidos.Add(pocao);
        timerPedidos.Add(pocao, pocao.reqTime);
        yield return new WaitForSeconds(0f);
    }

    private void Update() 
    {   
        //Cooldown para adicionar novos pedidos na lista de pedidos 
        if(pedidos.Count<maximoPedidos)
        {   
            auxDelay -= Time.deltaTime;
            if(auxDelay<=0)
            {
                auxDelay = delayPedidos;
                StartCoroutine(CreateCall());
            }
        }

        
        //Timer por pedido
        if(pedidos.Count != 0)
        {
            foreach(var elemento in pedidos)
            {    
                timerPedidos[elemento] -= Time.deltaTime;
                if(timerPedidos[elemento] <= 0)
                {   
                    Debug.Log(elemento);
                    pedidos.Remove(elemento);
                    timerPedidos.Remove(elemento);
                }
            }
        }
        
    }
}