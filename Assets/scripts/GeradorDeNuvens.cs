using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeradorDeNuvens : MonoBehaviour
{
    public GameObject[] prefabsDeNuvem; // Um array de prefabs de nuvens
    public GameObject[] locaisDeSpawn; // Array de GameObjects que marcam os locais de spawn
    public float velocidadeNuvem = 2f; // Velocidade de movimento das nuvens
    public float tempoMinimoSpawn = 10f; // Tempo mínimo entre spawns de nuvens
    public float tempoMaximoSpawn = 46f; // Tempo máximo entre spawns de nuvens

    private List<GameObject> locaisDisponiveis = new List<GameObject>();

    private void Start()
    {
        InicializarLocaisDisponiveis();
        StartCoroutine(SpawnDeNuvens());
    }

    private void InicializarLocaisDisponiveis()
    {
        foreach (GameObject local in locaisDeSpawn)
        {
            local.SetActive(false); // Desativa os locais de spawn inicialmente
            locaisDisponiveis.Add(local);
        }
    }

    private IEnumerator SpawnDeNuvens()
    {
        while (true)
        {
            if (locaisDisponiveis.Count > 0)
            {
                int indiceAleatorio = Random.Range(0, locaisDisponiveis.Count);
                GameObject localDeSpawn = locaisDisponiveis[indiceAleatorio];

                localDeSpawn.SetActive(true); // Ativa o local de spawn

                // Escolhe um prefab de nuvem aleatoriamente
                GameObject prefabNuvem = prefabsDeNuvem[Random.Range(0, prefabsDeNuvem.Length)];

                // Instancia a nuvem no local de spawn
                Vector3 posicaoSpawn = localDeSpawn.transform.position;
                posicaoSpawn.z = -7f; // Define a posição Z fixa
                GameObject nuvem = Instantiate(prefabNuvem, posicaoSpawn, Quaternion.identity);

                // Configura a movimentação da nuvem
                nuvem.AddComponent<MovimentoNuvem>().Configurar(velocidadeNuvem);

                // Destroi a nuvem após 10 segundos
                Destroy(nuvem, 80f);

                yield return new WaitForSeconds(0.1f); // Pequeno atraso para garantir a ativação do local de spawn
                localDeSpawn.SetActive(false); // Desativa o local de spawn novamente
            }

            // Aguarda um tempo aleatório antes de gerar a próxima nuvem
            float tempoProximoSpawn = Random.Range(tempoMinimoSpawn, tempoMaximoSpawn);
            yield return new WaitForSeconds(tempoProximoSpawn);
        }
    }
}

public class MovimentoNuvem : MonoBehaviour
{
    private float velocidade;

    public void Configurar(float velocidadeNuvem)
    {
        velocidade = velocidadeNuvem;
    }

    private void Update()
    {
        // Move a nuvem para a esquerda
        transform.Translate(Vector3.left * velocidade * Time.deltaTime);
    }
}
