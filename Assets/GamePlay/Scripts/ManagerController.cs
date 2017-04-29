using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    TITULO,
    GAMEPLAY,
    GAMEOVER,
    FASECONCLUIDA
}

public class ManagerController : MonoBehaviour {

    private PlayerController player;

    public GameState currentState;

    [Header("HUD GAME")]
    public GameObject hudInfoGamePlay, hudTitulo, hudGameOver, hudLoading, hudLoja, btnFechar;

    [Header("HUD GAME Atributos")]
    public Text moedasTxt, tempoTxt, faseTxt, moedasMapaTxt, tomateMapaTxt, precoPersonagemTxt;

    
    public int moedas, tempo, fase, moedasMapa, tomatesMapa, moedasColetadas, tomatesColetados, precoPersonagem;


    [Header("Selecção de Personagem")]
    public Mesh[] skinPersonagem;
    private MeshFilter meshPersonage;
    private int idPersonagem; // valor inicial igual a zero
    public int[] precoPersonagens;



	// Use this for initialization
	void Start () {

        PlayerPrefs.SetInt("Personagem0", 1);


        player = FindObjectOfType(typeof(PlayerController)) as PlayerController;

        meshPersonage = player.GetComponentInChildren<MeshFilter>();


        idPersonagem = PlayerPrefs.GetInt("IdPersoangemAtual");

        meshPersonage.mesh = skinPersonagem[idPersonagem];

        moedas = PlayerPrefs.GetInt("moedas");
        moedasTxt.text = moedas.ToString();

        precoPersonagemTxt.text = precoPersonagens[idPersonagem].ToString();

        switch (currentState)
        {
            case GameState.TITULO:
                hudInfoGamePlay.SetActive(false);
                hudTitulo.SetActive(true);
                hudGameOver.SetActive(false);
                hudLoading.SetActive(false);
                hudLoja.SetActive(false);
                btnFechar.SetActive(true);
                break;
            case GameState.GAMEPLAY:
                StartCoroutine("ContagemRegressiva");

                hudInfoGamePlay.SetActive(true);
                hudTitulo.SetActive(false);
                hudGameOver.SetActive(false);
                hudLoading.SetActive(false);
                hudLoja.SetActive(false);
                btnFechar.SetActive(false);

                break;

            case GameState.GAMEOVER:
                btnFechar.SetActive(true);
                break;
        }

		
	}
	
	// Update is called once per frame
	void Update () {
		if (currentState == GameState.TITULO)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (PlayerPrefs.GetInt("Personagem" + idPersonagem.ToString()) == 1)
                {
                    hudLoading.SetActive(true);
                    SceneManager.LoadSceneAsync("demo1");
                } else
                {
                    if (moedas >= precoPersonagens[idPersonagem])
                    {
                        PlayerPrefs.SetInt("Personagem" + idPersonagem.ToString(), 1);
                        moedas -= precoPersonagens[idPersonagem];
                        PlayerPrefs.SetInt("moedas", moedas);
                        PlayerPrefs.SetInt("IdPersoangemAtual", idPersonagem); // grava o id do personagem atual para ir selecionado

                        hudLoja.SetActive(true);
                        SceneManager.LoadSceneAsync("demo1");
                    }
                }

            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SelecionarPersonagem(1);
            } else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SelecionarPersonagem(-1);
            }
        }
	}

    #region Metodos criados pelo desenvolvedor

    IEnumerator ContagemRegressiva()
    {
        tempoTxt.text = tempo.ToString();
        yield return new WaitForSeconds(1);
        tempo -= 1;

        if (tempo == 0)
        {
            player.GoHit();
        }

        if (currentState == GameState.GAMEPLAY)
        {
            btnFechar.SetActive(false);
            StartCoroutine("ContagemRegressiva");
        } else
        {
            btnFechar.SetActive(true);
        }
    }
    public void AtualizarMoedas(int valor)
    {
        moedas += valor;
        moedasTxt.text = moedas.ToString();
        PlayerPrefs.SetInt("moedas", moedas);
        moedasColetadas += 1;

    }

    public void AtualizarTempo(int valor)
    {
        tempo += valor;
        tempoTxt.text = tempo.ToString();
        tomatesColetados += 1;
    }

    public void GameOver()
    {
        currentState = GameState.GAMEOVER;
        hudInfoGamePlay.SetActive(false);
        hudGameOver.SetActive(true);
        btnFechar.SetActive(true);
    }

    public void JogarNovamente()
    {
        hudLoading.SetActive(true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void VoltarTitulo()
    {
        hudLoading.SetActive(true);
        SceneManager.LoadSceneAsync("titulo");
    }

    public void Sair()
    {
        Application.Quit();
    }

    void SelecionarPersonagem(int i)
    {
        idPersonagem += i;

        if(idPersonagem >= skinPersonagem.Length)
        {
            idPersonagem = 0;
        } else if (idPersonagem < 0)
        {
            idPersonagem = skinPersonagem.Length - 1;
        }

        meshPersonage.mesh = skinPersonagem[idPersonagem];



        precoPersonagemTxt.text = precoPersonagens[idPersonagem].ToString();

        // verifica se termos o personagem para exibir ou nao o hud loja.
        if (PlayerPrefs.GetInt("Personagem" + idPersonagem.ToString()) == 0)
        {
            hudLoja.SetActive(true);
        } else
        {
            hudLoja.SetActive(false);
            PlayerPrefs.SetInt("IdPersoangemAtual", idPersonagem);
        }
    }
    #endregion
}
