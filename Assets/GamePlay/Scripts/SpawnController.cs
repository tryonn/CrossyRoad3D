using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public List<GameObject> veiculos;

    public bool esquerda;

    public float delayEntreCarros;
    public float delayEntreSpawn;

    public int nCarros, nCarrosMax;

    public float minSpeed, maxSpeed, speedMove;

	// Use this for initialization
	void Start () {
        speedMove = Random.Range(minSpeed, maxSpeed) * -1;
        nCarros = Random.Range(1, nCarrosMax + 1);
        StartCoroutine("Spawn");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region metodos criados pelo desenvolvedor
    IEnumerator Spawn()
    {
        for (int i = 0; i < nCarros; i++)
        {
            int id = Random.Range(0, veiculos.Count); // faz sorteio de qual veiculo será lancado;
            GameObject tempVeiculo = Instantiate(veiculos[id], transform.position, transform.rotation);
            tempVeiculo.transform.parent = transform;
            tempVeiculo.GetComponent<Mover>().moveSpeed = speedMove;
            if (esquerda) tempVeiculo.transform.rotation = Quaternion.Euler(0, 180, 0);

            yield return new WaitForSeconds(delayEntreCarros);
        }

        yield return new WaitForSeconds(delayEntreSpawn);
        nCarros = Random.Range(1, nCarrosMax + 1);
        StartCoroutine("Spawn");
    }
    #endregion
}
