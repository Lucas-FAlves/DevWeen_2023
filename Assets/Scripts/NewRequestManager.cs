using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class NewRequestManager : MonoBehaviour
{
    [Header("Poções")]
    [SerializeField] private List<PotionSO> pocoes = new List<PotionSO>();
    
    [Header("Configurações pedido")]
    public List<PotionSO> listaPedidos = new List<PotionSO>();  //Quando funcionar, vou deletar todas as menções a ela
    public int limiteRepeticoes, limitePedidos;
    public float delayPedidos;
    private float auxDelay;
    public Dictionary<PotionSO, float> pedidos = new Dictionary<PotionSO, float>();

    private void GeraPedido(Dictionary<PotionSO, float> pedidosAtuais)
    {
        Dictionary<PotionSO, int> qtdPedidos = new Dictionary<PotionSO, int>();

        foreach(PotionSO pocao in pocoes)
        {
            qtdPedidos.Add(pocao,0);
        }
        foreach(var elemento in pedidosAtuais)
        {
            qtdPedidos[elemento.Key] +=1;
            if(qtdPedidos[elemento.Key] >= limiteRepeticoes)
            {
                qtdPedidos.Remove(elemento.Key);
            }
        }

        //Define Poção a ser sorteada
        System.Random valorSorteado = new System.Random();
        int indice = valorSorteado.Next(0,qtdPedidos.Count);
        //Adiciona poção aos pedidos
        listaPedidos.Add(qtdPedidos.ElementAt(indice).Key);
        pedidos.Add(qtdPedidos.ElementAt(indice).Key, qtdPedidos.ElementAt(indice).Key.reqTime);
    }

    private void Awake() 
    {
        auxDelay = delayPedidos;
    }
    private void Update() 
    {
        float timePassed = Time.deltaTime;
        if (pedidos.Count < limitePedidos)
        {
            auxDelay -= timePassed;
            if(auxDelay<=0)
            {
                auxDelay = delayPedidos;
                GeraPedido(pedidos);
            }
        }
    }
        
        
    
}


