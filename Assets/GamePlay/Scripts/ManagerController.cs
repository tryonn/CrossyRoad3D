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

    public GameState currentState;

    [Header("HUD GAME")]
    public GameObject hudInfoGamePlay, hudTitulo, hudGameOver, hudLoading, hudLoja;

    [Header("HUD GAME Atributos")]
    public Text moedasTxt, tempoTxt, faseTxt, moedasMapaTxt, tomateMapaTxt;

    
    public int moedas, tempo, fase, moedasMapa, tomatesMapa, moedasColetadas, tomatesColetados;


	// Use this for initialization
	void Start () {

        switch (currentState)
        {
            case GameState.TITULO:
                break;
            case GameState.GAMEPLAY:
                StartCoroutine("ContagemRegressiva");
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
            currentState = GameState.GAMEOVER;
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
    #endregion
}
