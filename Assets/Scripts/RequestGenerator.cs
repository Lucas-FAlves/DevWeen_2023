using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RequestGenerator : MonoBehaviour
{
    public PotionSO[] listaPoc;
    // Start is called before the first frame update
    public PotionSO CreateRequest(PotionSO[] pedidos)
    {   
        //Cria indice para saber quantiade de pedidos na fila
        Dictionary<PotionSO, int> qtdPedidos = new Dictionary<PotionSO, int>(); 
        foreach (PotionSO elemento in pedidos)
        {
            if (qtdPedidos.ContainsKey(elemento) == false)
            {
                qtdPedidos.Add(elemento, 0);
            }
            qtdPedidos[elemento]= qtdPedidos[elemento]+1;
        }
        
        //Cria lista para definir qual será o próximo pedido
        PotionSO[] lista = listaPoc;
        for(int i = 0; i <= qtdPedidos.Count; i++)
        {
            if(qtdPedidos.ElementAt(i).Value >= 3)
            {
                lista = lista.Where(e => e != qtdPedidos.ElementAt(i).Key).ToArray();
            }
        }
        //Define próximo pedido
        System.Random valorSorteado = new System.Random();
        return lista[valorSorteado.Next(0,lista.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
