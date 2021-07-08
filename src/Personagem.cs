using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personagem : MonoBehaviour
{
    
    public Text dbg;
    
    Rigidbody rb;

    public Stack<GameObject> estomago;
    Stack<Bala.TipoArma> novoEstomago;
    //public Stack<Image> inventario;
    //Dictionary<string,Image> sprites;
    public Image [] imagensInventario = new Image[30];
    Stack<TipoBala> balasIntestino = new Stack<TipoBala>(30);

    // public Text txtBalas;
    public Text txtEstomago;
    //movimentacao
    public int speed = 15;
    float h,v;
    //status
    public int vidas = 3;
    bool vivo = true;
    //mesh
    public GameObject boneco;

    public GameObject gameOver;
    public ParticleSystem sangue;

    // Stack<GameObject> penteCalibre22
    public CaixaDeSom som;

    //ataque
    public BoxCollider bxclldrSugador;
    public GameObject sugador;
    public bool atirando, sugando;
    public GameObject mira;
    bool podeTeletransportar = true;
    float tempoPoder;
    //balas
    Stack<GameObject> penteCalibre22,foraPenteCalibre22,
    pentePistola, foraPentePistola,
    penteSniper, foraPenteSniper,
    penteRifle, foraPenteRifle,
    penteMissil, foraPenteMissil,
    penteMinigun, foraPenteMinigun;
    
    public GameObject municaoCalibre22, municaoPistola, municaoSniper, municaoRifle, municaoMissil, municaoMinigun;
    
    void Start()
    {
        novoEstomago = new Stack<Bala.TipoArma>(30);
        rb = boneco.GetComponent<Rigidbody>();
        estomago = new Stack<GameObject>(30);
        //inventario = new Stack<Image>(30);
        AtualizarUI();

        penteCalibre22 = new Stack<GameObject>();
        foraPenteCalibre22 = new Stack<GameObject>();

        pentePistola = new Stack<GameObject>();
        foraPentePistola = new Stack<GameObject>();

        penteSniper = new Stack<GameObject>();
        foraPenteSniper = new Stack<GameObject>();

        penteRifle = new Stack<GameObject>();
        foraPenteRifle = new Stack<GameObject>();

        penteMissil = new Stack<GameObject>();
        foraPenteMissil = new Stack<GameObject>();

        penteMinigun = new Stack<GameObject>();
        foraPenteMinigun = new Stack<GameObject>();

        for (int i = 0; i < 20; i++)
        {

            GameObject bullet = Instantiate(municaoCalibre22);
            bullet.SetActive(false);
            penteCalibre22.Push(bullet);

            GameObject bulleta = Instantiate(municaoPistola);
            bulleta.SetActive(false);
            pentePistola.Push(bulleta);

            GameObject bulletas = Instantiate(municaoSniper);
            bulletas.SetActive(false);
            penteSniper.Push(bulletas);

            GameObject bulletasd = Instantiate(municaoRifle);
            bulletasd.SetActive(false);
            penteRifle.Push(bulletasd);

            GameObject bulletasdf = Instantiate(municaoMissil);
            bulletasdf.SetActive(false);
            penteMissil.Push(bulletasdf);

            GameObject bulletasdfg = Instantiate(municaoMinigun);
            bulletasdfg.SetActive(false);
            penteMinigun.Push(bulletasdfg);
            
        }
        Debug.Log("pente: "+penteCalibre22.Count+"fora: "+foraPenteCalibre22.Count);
        
    }   

    void Update()
    {

        if(vidas<=0)
        {
            Morrer();
        }
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Mirar();

        Organismo();

        if(Input.GetKey(KeyCode.LeftShift) && podeTeletransportar)
        {
            mira.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                 StartCoroutine(Teletransportar());
            }
        }
        else mira.SetActive(false);
        
        dbg.text = "h: "+h+" v: "+v+" life: "+vidas+"\nshooting: "+atirando+" sucking: "+sugando+"\nstomach: "+novoEstomago.Count;
    }
    void FixedUpdate()
    {
        Movimento();
    }
    void Movimento() => rb.velocity = (new Vector3(h, 0, v).normalized * speed) + new Vector3(0, -9, 0);
    void Mirar()
    {
        Plane plane = new Plane(Vector3.up, -this.transform.position.y );
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            mira.transform.position = ray.GetPoint(distance);
        }
        boneco.transform.LookAt(mira.transform);
        
    }
    void Organismo()
    {
        atirando = sugando ? false : Input.GetMouseButton(0);

        sugando = atirando ? false : Input.GetMouseButton(1);

        sugador.SetActive(sugando);

        if(Input.GetMouseButtonDown(0))
        {
            Cuspir();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Comer();
        }
    }
    public void Engolir(Bala.TipoArma bala)
    {

        if(novoEstomago.Count < 30)
        {
            novoEstomago.Push(bala);
        }
        else
        {
            Indigestão();
        }
        
        Debug.Log("pente: "+penteCalibre22.Count+"fora: "+foraPenteCalibre22.Count);

    }
    void Cuspir()
    {
        if(novoEstomago.Count > 0)
        {
            Bala.TipoArma bulletType = novoEstomago.Pop();
            

            if(bulletType == Bala.TipoArma.CALIBER22)
            {
                if(!(penteCalibre22.Count>0))
                {
                    Recarregar(bulletType);

                }

                GameObject bullet = penteCalibre22.Pop();
                bullet.SetActive(true);
                bullet.GetComponent<Bala>().SetarPosicao(sugador.transform.position,this.transform.rotation);
                foraPenteCalibre22.Push(bullet);


            }
            else if(bulletType == Bala.TipoArma.PISTOL)
            {
                if(!(pentePistola.Count>0))
                {
                // bullet = pentePistola.Pop();
                    Recarregar(bulletType);

                }
                GameObject bullet = pentePistola.Pop();
                bullet.SetActive(true);
                bullet.GetComponent<Bala>().SetarPosicao(sugador.transform.position,this.transform.rotation);
                foraPentePistola.Push(bullet);

            }
            else if(bulletType == Bala.TipoArma.RIFLE)
            {
                if(!(penteRifle.Count>0))
                {
                    Recarregar(bulletType);

                }
                GameObject bullet = penteRifle.Pop();
                bullet.SetActive(true);
                bullet.GetComponent<Bala>().SetarPosicao(sugador.transform.position,this.transform.rotation);
                foraPenteRifle.Push(bullet);

            }
            else if(bulletType == Bala.TipoArma.MISSILE)
            {
                if(!(penteMissil.Count>0))
                {
                    // bullet = penteMissil.Pop();
                    Recarregar(bulletType);

                }
                GameObject bullet = penteMissil.Pop();
                bullet.SetActive(true);
                bullet.GetComponent<Bala>().SetarPosicao(sugador.transform.position,this.transform.rotation);
                foraPenteMissil.Push(bullet);

            }
            else if(bulletType == Bala.TipoArma.MINIGUN)
            {
                if(!(penteMinigun.Count>0))
                {
                    // bullet = penteMinigun.Pop();
                    Recarregar(bulletType);

                }
                GameObject bullet = penteMinigun.Pop();
                bullet.SetActive(true);
                bullet.GetComponent<Bala>().SetarPosicao(sugador.transform.position,this.transform.rotation);
                foraPenteMinigun.Push(bullet);

            }
            

        }

        Debug.Log("pente: "+penteCalibre22.Count+"fora: "+foraPenteCalibre22.Count);
    }
    void Recarregar(Bala.TipoArma pente)
    {
        switch (pente)
        {
            case Bala.TipoArma.CALIBER22:
                for (int i = 0; i < foraPenteCalibre22.Count; i++)
                {
                    penteCalibre22.Push(foraPenteCalibre22.Pop());
                    
                }
            break;
            case Bala.TipoArma.PISTOL:

            break;
            case Bala.TipoArma.SNIPER:

            break;
            case Bala.TipoArma.RIFLE:

            break;
            case Bala.TipoArma.MISSILE:

            break;
            case Bala.TipoArma.MINIGUN:

            break;
        }
    }
    void Comer()
    {
        
        Debug.Log("entrou na funcao");
        Debug.Log("inventario: "+ estomago.Count);
        if(estomago.Count > 10)
        {
            int quantidade = (int)Mathf.Ceil(estomago.Count/10);
            vidas += quantidade;

            for (int i = 0; i <= quantidade*10; i++)
            {
                estomago.Pop();
            }

            Debug.Log("botou pra comer");
        }
        AtualizarUI();
    }

    void AtualizarInventario()
    {
        var lista = estomago;
        Stack<GameObject> inverso = new Stack<GameObject>(estomago.Count);

        for (int i = 0; i < lista.Count; i++)
        {
            inverso.Push(lista.Pop());
        }

        string txt="";

        for (int i = 0; i < inverso.Count; i++)
        {
            txt+="\n"+ inverso.Pop().GetComponent<Bala>().arma.ToString();
        }

        string [] arr = new string[estomago.Count];

        // txtBalas.text = txt;
    }
    void AtualizarUI()
    {
        char [] txt = txtEstomago.text.ToCharArray();

        for (int i = 0; i < 30; i++)
        {
            txt[i] = '-';
        }
        for (int i = 0; i < estomago.Count; i++)
        {
            txt[i] = 'X';
        }

        if(estomago.Count>9 && estomago.Count < 20)
        {

            txtEstomago.color = Color.yellow;

        }
        else if(estomago.Count >= 20)
        {

            txtEstomago.color = Color.red;
        }
        else
        {
            txtEstomago.color = Color.green;

        }
        txtEstomago.text = new string(txt);
        
    }
    void OnTriggerEnter(Collider fora)
    {
        if(fora.gameObject.CompareTag("tiro"))
        {
            fora.gameObject.SetActive(false);
            LevarDano();
        }
    }
    void LevarDano()
    {
        vidas--;
    }
    void Indigestão()
    {
        LevarDano();
        for (int i = 0; i < 10; i++)
        {
            novoEstomago.Pop();
        }
        AtualizarUI();
    }
    void Morrer()
    {
        gameOver.SetActive(true);

        // som.Tocar(CaixaDeSom.Som.JOGADORMORREU);
        Time.timeScale = 0;
        //Destroy(this.transform.parent.gameObject);
    }
    IEnumerator Teletransportar()
    {
        podeTeletransportar = false;

        yield return new WaitForSeconds(0.2f);
        this.gameObject.transform.parent.position = mira.transform.position;
        

        // transform.position = 
        StartCoroutine(HabilitarTeletransporte());
    }
    IEnumerator HabilitarTeletransporte()
    {
        yield return new WaitForSeconds(20);
        podeTeletransportar = true;
    }
    enum TipoBala
    {
        CALIBER22, PISTOL, SNIPER, RIFLE, MISSILE, MINIGUN, LASER
    }
}
