using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municao 
{
    public Stack<GameObject> lista;
    public Municao(GameObject tipo, int quantidade)
    {
        lista = new Stack<GameObject>(quantidade);

        while(lista.Count != quantidade)
        {
            lista.Push(tipo);
        }
    }
    public void Push(GameObject bala)
    {
        lista.Push(bala);
    }
    public GameObject Pop()
    {
        return lista.Pop();
    }
}
