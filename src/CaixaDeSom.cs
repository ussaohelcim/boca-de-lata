using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaDeSom : MonoBehaviour
{
    //public AudioClip tiro, inimigoMorreu, jogadorMorreu, ok, missaoCompleta, enguliu, danoJogador, danoInimigo;
    AudioSource som;
    void Start()
    {
        som = GetComponent<AudioSource>();
        som.volume = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Tocar(Som tipo)
    {
        // switch (tipo)
        // {
        //     case Som.TIRO:
        //         som.PlayOneShot(tiro);
        //     break;
        //     case Som.INIMIGOMORREU:
        //         som.PlayOneShot(inimigoMorreu);
        //     break;
        //     case Som.JOGADORMORREU:
        //         som.PlayOneShot(jogadorMorreu);
        //     break;
        //     case Som.OK:
        //         som.PlayOneShot(ok);
        //     break;
        //     case Som.MISSAOCOMPLETA:
        //         som.PlayOneShot(missaoCompleta);
        //     break;
        //     case Som.ENGOLIU:
        //         som.PlayOneShot(enguliu);
        //     break;
        //     case Som.DANOJOGADOR:
        //         som.PlayOneShot(danoJogador);
        //     break;
        //     case Som.DANOINIMIGO:
        //         som.PlayOneShot(danoInimigo);
        //     break;
        // }
    }
    public enum Som
    {
        //TIRO, INIMIGOMORREU, JOGADORMORREU, OK, MISSAOCOMPLETA, ENGOLIU, DANOJOGADOR, DANOINIMIGO
    }
}
