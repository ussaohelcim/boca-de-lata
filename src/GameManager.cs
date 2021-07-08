using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //video
    [Range(1,4)] public int graficos;
    string [] listaGraficos = {"LOW","MEDIUM","HIGH","ULTRA"};
    public Resolution [] resolucoes;
    public Slider sliderGraficos, sliderResolucoes;
    public Text labelGraficos, labelResolucao;

    //musicas
    [Range(0,1)] public float volumeMusica, volumeSFX;
    public Slider sliderSFX, sliderMusica;
    //game
    public bool pausado;
    void Start()
    {
        resolucoes = Screen.resolutions;
        sliderResolucoes.maxValue = resolucoes.Length;
        sliderGraficos.maxValue = listaGraficos.Length;
        sliderResolucoes.value = sliderResolucoes.maxValue;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pausado)
        {
            Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pausado)
        {
            Despausar();
        }
    }
    #region Game
    

    void Pause()
    {
        pausado = true;
        Time.timeScale = 0;
    }
    void Despausar()
    {
        pausado = false;
        Time.timeScale = 1;
    }
    void Menu()
    {
        SceneManager.LoadScene("MENU");
    }
    void Sair()
    {
        Application.Quit();
    }
    #endregion
    
    #region graficos
    

    public void MudarLabel(bool resolucao)
    {
        if(resolucao)
        {
            labelResolucao.text = "Resolution - "+resolucoes[(int)sliderResolucoes.value-1].width+"x"+resolucoes[(int)sliderResolucoes.value-1].height;
        }
        else
        {
            labelGraficos.text = "Graphics - "+ listaGraficos[(int)sliderGraficos.value-1];
        }
    }
    void MudarResolucao(Resolution resolution, FullScreenMode fullscreenmode, int frequencia)
    {
        Screen.SetResolution(resolution.width,resolution.height,fullscreenmode,frequencia);
    }
    #endregion

    #region volume
   

    void MudarVolume()
    {
        float sfx = volumeSFX;
        float musica = volumeMusica;
    }
    #endregion
    public void SalvarMudanças()
    {
        if(Screen.currentResolution.ToString() == resolucoes[(int)sliderResolucoes.value-1].ToString())
        {

        }
        else
        {
            
        }
        //MudarResolucao(resolucoes[sl])
    }
    
}
