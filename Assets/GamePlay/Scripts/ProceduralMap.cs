using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMap : MonoBehaviour {

    private ManagerController manager;

    [Header("Config Blocos Cenário")]
    public int          tamanhoBloco;
    public GameObject[] blocoPrefab;
    public int[]        ocupaBlocos;
    public bool[]       temDecoracao;
    public bool[]       temColetavel;
    public int          idBlocoAgua;
    public bool         spawnAguaL;
    public GameObject   blocoChegada;
    public GameObject   blocoLimitador;
    public GameObject   spawnPrefab;
    public int          limiteLinhaCena;


    [Header("Config Mapa")]
    public int blocosLinhas; // sempre use numero par
    public int qtdLinhas; // quantidade de vezes que vai ser gerado uma linha
    public int qtdLinhasInicioFim;// quantos blocos vai ter na base e na chegada
    public int qtdLimitadoresBlocos;

    [Header("Blocos Decoração e Coletaveis")]
    public GameObject[] decoracaoPrefab;
    public GameObject[] coletaveisPrefab;
    public string[] nomeColetavel;

    public int chanceDecoracao;
    public int prioridadeColetavel;
    public int chanceColetavel;


    [Header("Objetos Spawn")]
    public GameObject[] blocos0;
    public GameObject[] blocos1;
    public GameObject[] blocos2;
    public GameObject[] blocos3;
    public GameObject[] blocos4;
    public GameObject[] blocos5;


    [Header("Hieraquia Mapa")]
    public Transform blocosJogaveis;
    public Transform blocosLimitadores;
    public Transform blocosDecoracao;
    public Transform blocosColetaveis;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
