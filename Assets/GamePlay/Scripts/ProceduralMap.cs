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
        MontarMapa();
	}

    #region Metodos criados pelo desenvolvedor
    public void MontarMapa()
    {
        int idBloco = 0;
        int meio = blocosLinhas / 2;
        float posXInicial = (meio * tamanhoBloco) *-1;
        float posZInicial = (qtdLimitadoresBlocos * tamanhoBloco) * -1;


        // gera as linhas iniciais do mapa
        for (int linha = 0; linha < qtdLinhasInicioFim; linha++)
        {
            GerarLinha(blocoPrefab[idBloco], meio, posXInicial, ocupaBlocos[idBloco], temDecoracao[idBloco], temColetavel[idBloco], !temDecoracao[idBloco], idBloco);
        }


        // gera linhas jogaveis (mapa en si)
        for (int linha = 0; linha < qtdLinhas; linha++)
        {
            idBloco = Random.Range(0, blocoPrefab.Length);

            GerarLinha(blocoPrefab[idBloco], meio, posXInicial, ocupaBlocos[idBloco], temDecoracao[idBloco], temColetavel[idBloco], !temDecoracao[idBloco], idBloco);
        }
    }
    void GerarLinha(GameObject _blocoPrefab, int _meio, float _posXInicial, int _ocupaBlocos, bool _decoravel, bool _coletavel, bool _spawn, int _idBloco)
    {
        Vector3 posicaoBloco = Vector3.zero;

        for (int blocoAtual = 0; blocoAtual <= blocosLinhas; blocoAtual++)
        {
            posicaoBloco = new Vector3(_posXInicial + (tamanhoBloco * blocoAtual), _blocoPrefab.transform.position.y, _blocoPrefab.transform.position.z + (tamanhoBloco * limiteLinhaCena));
            Instantiate(_blocoPrefab, posicaoBloco, _blocoPrefab.transform.rotation, blocosJogaveis);
        }

        limiteLinhaCena += _ocupaBlocos;

    }

    void GerarLinhaInicialFinal()
    {

    }

    void InserirDecoracao()
    {

    }
    void InserirColetaveis()
    {

    }

    void InserirSpawnBlocos()
    {

    }
    #endregion
}
