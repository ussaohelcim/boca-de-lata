using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnador : MonoBehaviour
{
    public GameObject [] inimigos = new GameObject[6];

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Spawnar()
    {
        Vector3 pos = new Vector3(Random.Range(-20f,20f),1,Random.Range(-20f,20f));
        Instantiate(inimigos[Random.Range(0,inimigos.Length)],pos,Quaternion.identity);
    }
}
