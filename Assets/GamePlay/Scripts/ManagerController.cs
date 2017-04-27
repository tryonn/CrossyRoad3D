using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject hudInfoGamePlay, hudTitulo, hudGameOver, hudLoading, hudLoja;

    [Header("HUD GAME Atributos")]
    public Text moedasTxt, tempoTxt, faseTxt, moedasMapaTxt, tomateMapaTxt, precoPersonagemTxt;

    
    public int moedas, tempo, fase, moedasMapa, tomatesMapa, moedasColetadas, tomatesColetados, precoPersonagem;


	// Use this for initialization
	void Start () {
        player = FindObjectOfType(typeof(PlayerController)) as PlayerController;

        moedasTxt.text = moedas.ToString();

        switch (currentState)
        {
            case GameState.TITULO:
                hudInfoGamePlay.SetActive(false);
                hudTitulo.SetActive(true);
                hudGameOver.SetActive(false);
                hudLoading.SetActive(false);
                hudLoja.SetActive(false);

                break;
            case GameState.GAMEPLAY:
                StartCoroutine("ContagemRegressiva");

                hudInfoGamePlay.SetActive(true);
                hudTitulo.SetActive(false);
                hudGameOver.SetActive(false);
                hudLoading.SetActive(false);
                hudLoja.SetActive(false);

                break;

            case GameState.GAMEOVER:

                break;
        }

		
	}
	
	// Update is called once per frame
	void Update () {
		
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
            StartCoroutine("ContagemRegressiva");
        }
    }
    public void AtualizarMoedas(int valor)
    {
        moedas += valor;
        moedasTxt.text = moedas.ToString();
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
    }
    #endregion
}
