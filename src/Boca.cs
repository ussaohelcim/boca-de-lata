using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boca : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject boneco;
    public Personagem corpo;
    public int velocidadeGiro;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(new Vector3(0,0,velocidadeGiro*Time.deltaTime));
    }
    void OnTriggerEnter(Collider fora)
    {
        if(fora.gameObject.CompareTag("tiro"))
        {
            Bala.TipoArma bullet = fora.gameObject.GetComponent<Bala>().arma;
            fora.gameObject.SetActive(false);
            //bullet.transform.tag = "tirojogador";
            corpo.Engolir(bullet);       
        }
    }
}
