using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour
{
    public CaixaDeSom caixaDeSom;
    Stack<GameObject> pente, fora;
    public Tipo cargo;
    public int vidas;
    
    public GameObject bala;
    public Transform jogador;
    float fireRate;
    public GameObject luz;
    public Transform posArma;
    bool veiculo = false;
    public GameObject uiVida;
    TextMesh txtVida; 
    public int quantidadeBalas =4;
    // public Text dbg;

    //ia
    public bool vendoJogador, patrulhando, atirando, morto, viuJogador;
    NavMeshAgent agent;
    public float velocidade, alcanceVisao;
    [SerializeField]EstadoInimigo estado;
    Vector3 alvo;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        txtVida = uiVida.GetComponent<TextMesh>();

        GameObject bullet = Instantiate(bala.gameObject);
        bullet.SetActive(false);

        switch (cargo)
        {
            case Tipo.CAIPIRA:
                bullet.GetComponent<Bala>().arma = Bala.TipoArma.CALIBER22;
                vidas = 3;
                fireRate = 1.35f;
                velocidade = 4;
                // fireRate = 0.5f;
            break;
            case Tipo.POLICIAL:
                bullet.GetComponent<Bala>().arma = Bala.TipoArma.PISTOL;
                vidas = 4;
                fireRate = 1f;
                velocidade = 3;

                // fireRate = 0.3f;
            break;
            case Tipo.SWAT:
                bullet.GetComponent<Bala>().arma = Bala.TipoArma.PISTOL;
                vidas = 4;
                fireRate = 1.3f;
                velocidade = 3;
                // fireRate = 0.3f;
            break;
            case Tipo.SNIPER:
                bullet.GetComponent<Bala>().arma = Bala.TipoArma.SNIPER;
                vidas = 4;
                fireRate = 2;
                velocidade = 3;
                // fireRate = 1;
            break;
            case Tipo.SOLDADO:
                // fireRate = 0.1f;
                bullet.GetComponent<Bala>().arma = Bala.TipoArma.RIFLE;
                fireRate = 1f;
                velocidade = 5;
                vidas = 6;
            break;
            case Tipo.SOLDADOBAZUCA:
                vidas = 6;
                fireRate = 1;
                velocidade = 4;
                // fireRate = 2;
                bullet.GetComponent<Bala>().arma = Bala.TipoArma.MISSILE;
            break;
            case Tipo.JIPE:
                vidas = 10;
                fireRate = 0.7f;
                veiculo = true;
                velocidade = 10;
                // fireRate = 0.05f;
                bullet.GetComponent<Bala>().arma = Bala.TipoArma.MINIGUN;
            break;
            case Tipo.TANQUE:
                vidas = 30;
                fireRate = 2;
                velocidade = 6;
                veiculo = true;

                // fireRate = 1;
                bullet.GetComponent<Bala>().arma = Bala.TipoArma.MISSILE;
            break;
            case Tipo.METALGEAR:
                fireRate = 1;
                veiculo = true;
                vidas = 60;
                velocidade = 3;
                bullet.GetComponent<Bala>().arma = Bala.TipoArma.MISSILE;
            break;
            case Tipo.ALIENIGENA:
                fireRate = 1;
                vidas = 20;
                velocidade = 8;
                bullet.GetComponent<Bala>().arma = Bala.TipoArma.LASER;
            break;
        }
        
        fora = new Stack<GameObject>();
        pente = new Stack<GameObject>();

        
        for (int i = 0; i < quantidadeBalas; i++)
        {
            pente.Push(bullet);
            // pente.Peek().SetActive(false);
        }

        agent.speed = velocidade;
        estado = EstadoInimigo.PATRULHANDO;
        viuJogador = false;
        alcanceVisao = 30;
    }

    void Update()
    {
        // dbg.text = "pente "+pente.Count+"fora "+fora.Count;
        //distancia = agent.remainingDistance;
        NavMeshHit hit;

        bool bloqueado = NavMesh.Raycast(transform.position, jogador.transform.position, out hit, NavMesh.AllAreas);
                
        Debug.DrawLine(transform.position, jogador.transform.position, bloqueado ? Color.red : Color.green);

        //agent.SetDestination(jogador.transform.position);
        agent.SetDestination(alvo);

        switch (estado)
        {
            case EstadoInimigo.MORTO:
            break;
            case EstadoInimigo.PATRULHANDO:
            //nunca viu o jogador
                if(hit.distance < alcanceVisao)
                {
                    viuJogador = true;
                }
                if(viuJogador)
                {
                    estado = EstadoInimigo.SEGUINDOJOGADOR;
                }
                if(!patrulhando)
                {
                    StartCoroutine(Patrulhar());
                }
                //patrulhar
            break;
            case EstadoInimigo.ATACANDOJOGADOR:
            //seguindo e atirando no jogador
                if(agent.remainingDistance > alcanceVisao)
                {
                    estado = EstadoInimigo.SEGUINDOJOGADOR;
                }
                //seguir e atacar jogador
                AtacarJogador();
            break;
            case EstadoInimigo.SEGUINDOJOGADOR:
            //viu o jogador mas não ta atirando
                if(agent.remainingDistance < alcanceVisao)
                {
                    estado = EstadoInimigo.ATACANDOJOGADOR;
                }
                //seguir jogador
                SeguirJogador();
            break;
        }
    
        
    }
    IEnumerator FireRate(float s)
    {
        atirando = true;
        yield return new WaitForSeconds(s);
        Atirar();
        
    }
    void Atirar()
    {
        //Debug.Log("no pente do "+this.cargo+" tem "+pente.Count+" balas");
        atirando = false;
        GameObject bala;
        if(pente.Count<=0)
        {
            Recarregar();
        }

        bala = pente.Pop();

        bala.GetComponent<Bala>().SetarPosicao(posArma.position, posArma.rotation);

        bala.SetActive(true);

        fora.Push(bala);
        //StartCoroutine(ApagarLuz());
        //Instantiate(pente.Pop(), transform.position, transform.rotation);
        //Debug.Log("atirando: "+bala.GetComponent<Bala>().arma.ToString());
        //Instantiate(bala, posArma.position, posArma.rotation);
        //StartCoroutine(FireRate(1f));

    }
    public void Recarregar()
    {

        GameObject bullet = fora.Pop();
        bullet.SetActive(false);
        pente.Push(bullet);
    }
    void SeguirJogador()
    {
        //alvo = jogador.transform.position;
        SelecionarAlvo(jogador.transform.position);
    }
    void AtacarJogador()
    {
        //alvo = jogador.transform.position;
        SeguirJogador();
        if(!atirando)
        {
            StartCoroutine(FireRate(fireRate));
        }
        posArma.transform.LookAt(alvo);
    }
    void SelecionarAlvo(Vector3 target)
    {
        alvo = target;
    }
    void OnTriggerEnter(Collider fora)
    {
        //Debug.Log("colidiu");
        //Debug.Log(fora.name);
        if(fora.gameObject.CompareTag("tirojogador"))
        {
            //Destroy(fora.gameObject);
            fora.gameObject.SetActive(false);
            LevarDano();
        }
    }
    IEnumerator Patrulhar()
    {
        
        patrulhando = true;

        //alvo = new Vector3(Random.Range(transform.position.x-5,transform.position.x+5),transform.position.y,Random.Range(transform.position.z-5,transform.position.z+5));

        Vector3 target = new Vector3(Random.Range(transform.position.x-10,transform.position.x+10),transform.position.y,Random.Range(transform.position.z-10,transform.position.z+10));

        SelecionarAlvo(target);

        //agent.destination = new Vector3(Random.Range(transform.position.x-5,transform.position.x+5),transform.position.y,Random.Range(transform.position.z-5,transform.position.z+5));

        yield return new WaitForSeconds(3);


        patrulhando = false;
    }
    void LevarDano()
    {
        if(!uiVida.activeSelf)
        {
            uiVida.SetActive(true);
        }
        vidas--;
        txtVida.text = vidas.ToString();

        //caixaDeSom.Tocar(CaixaDeSom.Som.DANOINIMIGO);
        
        StartCoroutine(ApagarHud());
    }
    IEnumerator ApagarHud()
    {
        yield return new WaitForSeconds(1);
        if(uiVida.activeSelf) uiVida.SetActive(false);
    }
    void Morrer()
    {
        morto = true;
        // luz.gameObject.SetActive(true);
        // luz.gameObject.GetComponent<Light>().intensity = luz.gameObject.GetComponent<Light>().intensity * 2;
        transform.position = new Vector3(transform.position.x,transform.position.y+3,transform.position.z);
        transform.rotation = Quaternion.Euler(90,0,0);

        //caixaDeSom.Tocar(CaixaDeSom.Som.INIMIGOMORREU);

        Destroy(this.GetComponent<NavMeshAgent>());
        
    }
    public enum Tipo
    {
        CAIPIRA, POLICIAL, SWAT, SNIPER, SOLDADO, SOLDADOBAZUCA, JIPE, TANQUE, METALGEAR, ALIENIGENA
    }
    enum EstadoInimigo
    {
        MORTO, //morto
        PATRULHANDO, //nunca viu o jogador
        ATACANDOJOGADOR, //seguindo e atirando o jogador
        SEGUINDOJOGADOR //viu o jogador mas não ta atirando
    }
}
