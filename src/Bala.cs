using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    //CaixaDeSom caixaDeSom;

    int velocidade;
    public TipoArma arma;
    public float ttl;
    float tamanho;
    void Start()
    {
        //caixaDeSom = GameObject.FindGameObjectWithTag("som").GetComponent<CaixaDeSom>();
        //rb.AddForce(this.transform.rotation * Vector3.forward*velocidade,ForceMode.Impulse);
        this.transform.tag = "tiro";
        // Mover();

        switch (arma)
        {
            case TipoArma.CALIBER22:
                velocidade = 8;
                tamanho = 0.5f;
            break;
            
            case TipoArma.PISTOL:
                velocidade = 13;
                tamanho = 0.5f;
            break;

            case TipoArma.RIFLE:
                velocidade = 16;
                tamanho = 0.5f;
            break;

            case TipoArma.MINIGUN:
                velocidade = 20;
                tamanho = 0.3f;
            break;

            case TipoArma.MISSILE:
                velocidade = 12;
                tamanho = 0.7f;
            break;
            case TipoArma.SNIPER:
                velocidade = 20;
                tamanho = 0.5f;
            break;
            case TipoArma.LASER:
                velocidade = 20;
                tamanho = 0.5f;
            break;
        }
        
        this.transform.localScale = new Vector3(tamanho,tamanho,tamanho);
        velocidade *= 2;
    }
    void Update()
    {
        //Debug.Log("ttl: "+ttl);
        if(this.gameObject.activeSelf)
        {
            if(ttl <= 0)
            {
                //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
            }
            else
            {
                ttl -= Time.deltaTime;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Mover();
    }
    void Mover()
    {
        this.transform.position += transform.rotation * Vector3.forward * velocidade * Time.deltaTime;
    }
    void OnEnable()
    {
        ttl = 3;
        //trail.isVisible

    }
    void OnDisable()
    {
        ttl = 3;
    }
    void OnCollisionEnter(Collision fora)
    {
        this.gameObject.SetActive(false);
    }
    public void SetarPosicao(Vector3 posicao, Quaternion rotacao)
    {
        transform.position = posicao;
        transform.rotation = rotacao;
    }

    public enum TipoArma
    {
        CALIBER22, PISTOL, SNIPER, RIFLE, MISSILE, MINIGUN, LASER
    }
}
