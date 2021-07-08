using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeFases : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject caipira, policial, swat, soldado, soldadoBazooka, sniper, jipe, tanque, metalgear, alienigena; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

class Fase
{
    int quantidadeInimigos;
    public Fase(int numero)
    {
        quantidadeInimigos = numero * 10;
    }
    public void NovaFase()
    {

    }
}