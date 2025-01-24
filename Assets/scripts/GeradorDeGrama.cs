using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeradorDeGrama : MonoBehaviour
{
    public GameObject[] spritesDeGrama; // Um array de sprites de grama
    public int quantidadeDeGrama = 20; // Número de objetos de grama a serem gerados
    public GameObject[] locaisDeSpawn; // Array de GameObjects que marcam os locais de spawn
    private List<GameObject> locaisDisponiveis = new List<GameObject>();

    private void Start()
    {
        InicializarLocaisDisponiveis();
        SpawnDeGramaRapido();
    }

    private void InicializarLocaisDisponiveis()
    {
        // Inicialize a lista de locais disponíveis com os locais de spawn
        foreach (GameObject local in locaisDeSpawn)
        {
            locaisDisponiveis.Add(local);
        }
    }

    private void SpawnDeGramaRapido()
    {
        for (int i = 0; i < quantidadeDeGrama; i++)
        {
            // Certifique-se de que ainda existem locais de spawn disponíveis
            if (locaisDisponiveis.Count == 0)
            {
                break;
            }

            // Escolha aleatoriamente um local de spawn
            int indiceAleatorio = Random.Range(0, locaisDisponiveis.Count);
            GameObject localDeSpawn = locaisDisponiveis[indiceAleatorio];

            // Escolha aleatoriamente uma sprite de grama do array
            GameObject spriteGrama = spritesDeGrama[Random.Range(0, spritesDeGrama.Length)];

            // Instancie a sprite de grama no local de spawn escolhido
            Instantiate(spriteGrama, localDeSpawn.transform.position, Quaternion.identity);

            // Remova o local de spawn da lista de locais disponíveis
            locaisDisponiveis.RemoveAt(indiceAleatorio);
        }

        // Desative os locais restantes que não foram utilizados
        foreach (GameObject local in locaisDisponiveis)
        {
            local.SetActive(false);
        }
    }
}
