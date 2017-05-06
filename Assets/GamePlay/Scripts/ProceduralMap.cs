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



        // gera linhas -  limitadores do mapa - linhas nao jogaveis
        for (int linha = 0; linha < qtdLimitadoresBlocos; linha++)
        {
            GerarLinhaInicialFinal(blocoPrefab[idBloco], meio, linha, posXInicial - (tamanhoBloco * qtdLimitadoresBlocos), posZInicial);
        }


        // gera as linhas iniciais do mapa
        for (int linha = 0; linha < qtdLinhasInicioFim; linha++)
        {
            bool dec = false;
            if (linha == 0)
            {
                dec = false;
            } else
            {
                dec = true;
            }

            GerarLinha(blocoPrefab[idBloco], meio, posXInicial, ocupaBlocos[idBloco], dec, dec, !temDecoracao[idBloco], idBloco);
        }


        // gera linhas jogaveis (mapa en si)
        for (int linha = 0; linha < qtdLinhas; linha++)
        {
            idBloco = Random.Range(0, blocoPrefab.Length);

            GerarLinha(blocoPrefab[idBloco], meio, posXInicial, ocupaBlocos[idBloco], temDecoracao[idBloco], temColetavel[idBloco], !temDecoracao[idBloco], idBloco);
        }

        // gera as linhas finais (chegada)
        for (int linha = 0; linha < qtdLinhasInicioFim; linha++)
        {
            GerarLinha(blocoChegada, meio, posXInicial, 1, false, false, false, idBloco);
        }


        // linhas nao jogaveis no final da fase.(chegada)
        idBloco = 0; //bloco de grama
        for (int linha = 0; linha < qtdLimitadoresBlocos; linha++)
        {
            GerarLinhaInicialFinal(blocoPrefab[idBloco], meio, linha, posXInicial - (tamanhoBloco * qtdLimitadoresBlocos), limiteLinhaCena * tamanhoBloco);
        }

    }


    void GerarLinha(GameObject _blocoPrefab, int _meio, float _posXInicial, int _ocupaBlocos, bool _decoravel, bool _coletavel, bool _spawn, int _idBloco)
    {
        Vector3 posicaoBloco = Vector3.zero;

        // gerar blocos limite lado esquerdo
        for (int blocoAtual = 0; blocoAtual < qtdLimitadoresBlocos; blocoAtual++)
        {
            float psX = (_posXInicial - (qtdLimitadoresBlocos * tamanhoBloco)) + tamanhoBloco * blocoAtual;
            float psY = _blocoPrefab.transform.position.y;
            float psZ = _blocoPrefab.transform.position.z + (tamanhoBloco * limiteLinhaCena);
            posicaoBloco = new Vector3(psX, psY, psZ);

            Instantiate(_blocoPrefab, posicaoBloco, _blocoPrefab.transform.rotation, blocosLimitadores);

            // quantod blocos de sombra sera adicionado nessa parte | esquerda ou direita do mapa
            for (int i = 0; i < _ocupaBlocos; i++)
            {
                Instantiate(blocoLimitador, 
                    new Vector3(psX, blocoLimitador.transform.position.y, 
                    (tamanhoBloco * limiteLinhaCena) + tamanhoBloco * i), 
                    _blocoPrefab.transform.rotation, blocosLimitadores);
            }
        }

        // blocos jogaveis
        for (int blocoAtual = 0; blocoAtual <= blocosLinhas; blocoAtual++)
        {
            posicaoBloco = new Vector3(_posXInicial + (tamanhoBloco * blocoAtual), _blocoPrefab.transform.position.y, _blocoPrefab.transform.position.z + (tamanhoBloco * limiteLinhaCena));
            Instantiate(_blocoPrefab, posicaoBloco, _blocoPrefab.transform.rotation, blocosJogaveis);

            //parte de coletaveis e decorados
            if (_decoravel && _coletavel)
            {
                int rand = Random.Range(0, 100);
                if (rand < prioridadeColetavel)
                {
                    InserirColetaveis(posicaoBloco, _ocupaBlocos);
                } else
                {
                    InserirDecoracao(posicaoBloco);
                }
            } else if (!_decoravel && _coletavel)
            {
                InserirColetaveis(posicaoBloco, _ocupaBlocos);
            }
        }

        // gerar blocos limite lado direito
        for (int blocoAtual = 0; blocoAtual < qtdLimitadoresBlocos; blocoAtual++)
        {
            float psX = (_posXInicial + (blocosLinhas + 1) * tamanhoBloco) + tamanhoBloco * blocoAtual;
            float psY = _blocoPrefab.transform.position.y;
            float psZ = _blocoPrefab.transform.position.z + (tamanhoBloco * limiteLinhaCena);
            posicaoBloco = new Vector3(psX, psY, psZ);

            Instantiate(_blocoPrefab, posicaoBloco, _blocoPrefab.transform.rotation, blocosLimitadores);

            // quantod blocos de sombra sera adicionado nessa parte | esquerda ou direita do mapa
            for (int i = 0; i < _ocupaBlocos; i++)
            {
                Instantiate(blocoLimitador,
                    new Vector3(psX, blocoLimitador.transform.position.y,
                    (tamanhoBloco * limiteLinhaCena) + tamanhoBloco * i),
                    _blocoPrefab.transform.rotation, blocosLimitadores);
            }
        }

        limiteLinhaCena += _ocupaBlocos;

    }

    void GerarLinhaInicialFinal(GameObject _blocoPrefab, int meio, int linhaAtual, float posXinicial, float posZInicial)
    {
        Vector3 posicaoBloco = Vector3.zero;

        for (int blocoAtual = 0; blocoAtual <= (blocosLinhas + (qtdLimitadoresBlocos * 2)); blocoAtual++)
        {
            posicaoBloco = new Vector3(posXinicial + (tamanhoBloco * blocoAtual), _blocoPrefab.transform.position.y, posZInicial + (tamanhoBloco * linhaAtual));
            Instantiate(_blocoPrefab, posicaoBloco, _blocoPrefab.transform.rotation, blocosLimitadores);

            // instanciando os blocos limitadores
            Instantiate(blocoLimitador, new Vector3(posicaoBloco.x, blocoLimitador.transform.position.y, posicaoBloco.z), blocoLimitador.transform.rotation, blocosLimitadores);
        }
    }

    void InserirDecoracao(Vector3 _posicaoBloco)
    {
        int rand = Random.Range(0, 100);
        if (rand <= chanceDecoracao)
        {
            int idDec = Random.Range(0, decoracaoPrefab.Length);
            Instantiate(decoracaoPrefab[idDec], new Vector3(_posicaoBloco.x, _posicaoBloco.y + tamanhoBloco, _posicaoBloco.z), decoracaoPrefab[idDec].transform.rotation, blocosDecoracao);

        }
    }
    void InserirColetaveis(Vector3 _posicaoBloco, int _ocupaBlocos)
    {
        for (int i = 0; i < _ocupaBlocos; i++)
        {
            int rand = Random.Range(0, 100);
            if (rand <= chanceColetavel)
            {
                int idColetavel = Random.Range(0, coletaveisPrefab.Length);
                Instantiate(coletaveisPrefab[idColetavel], new Vector3(_posicaoBloco.x, _posicaoBloco.y + tamanhoBloco, _posicaoBloco.z + (tamanhoBloco * i)), coletaveisPrefab[idColetavel].transform.rotation, blocosColetaveis);
            }
        }
    }

    void InserirSpawnBlocos()
    {

    }
    #endregion
}
