using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeHordas : MonoBehaviour
{
    public GameObject[] inimigosPrefab;
    public GameObject[] chefesPrefab;
    public Transform[] spawnPointsInimigos;
    public Transform[] spawnPointsBoss;
    public float aumentoDeDanoEntreHordas = 0.1f;
    public float aumentoDeDanoPorBoss = 0.5f;

    private const float posicaoZ = -0.148f;
    private bool[] usedSpawnPointsInimigos;
    private bool[] usedSpawnPointsBoss;
    private int totalDeInimigos = 0;
    private int inimigosSpawnados = 0;
    public GerenciadorDeCartas gerenciadorDeCartas;
    private bool isHordaAtiva = false;
    public int hordasDesdeUltimoBoss = 0;

    public int contadorHorda = 0;
    private int cartasEscolhidas;

    private void Start()
    {
        usedSpawnPointsInimigos = new bool[spawnPointsInimigos.Length];
        usedSpawnPointsBoss = new bool[spawnPointsBoss.Length];

        // Inicialmente, desative todos os pontos de spawn
        SetSpawnPointsActive(spawnPointsInimigos, false);
        SetSpawnPointsActive(spawnPointsBoss, false);
    }

    public void IniciarHorda(int cartasEscolhidas)
    {
        if (cartasEscolhidas > 0 && !isHordaAtiva)
        {
            this.cartasEscolhidas = cartasEscolhidas;
            contadorHorda = cartasEscolhidas;

            Debug.Log("Iniciando horda...");

            // Ative os pontos de spawn antes de criar os inimigos
            SetSpawnPointsActive(spawnPointsInimigos, true);
            SetSpawnPointsActive(spawnPointsBoss, true);

            // Verifique se é hora de spawnar um boss
            if (cartasEscolhidas % 10 == 0)
            {
                GameObject[] prefabsDoBoss = chefesPrefab;
                SpawnInimigos(prefabsDoBoss, 1); // Spawn de 1 boss
                hordasDesdeUltimoBoss = 0;
                contadorHorda = 1;
            }
            else
            {
                GameObject[] prefabsDaHorda = inimigosPrefab;
                int inimigosParaSpawn = cartasEscolhidas;
                SpawnInimigos(prefabsDaHorda, inimigosParaSpawn);
                hordasDesdeUltimoBoss++;
            }

            // Desative os pontos de spawn após spawnar os inimigos
            SetSpawnPointsActive(spawnPointsInimigos, false);
            SetSpawnPointsActive(spawnPointsBoss, false);

            isHordaAtiva = true;
            ResetUsedSpawnPoints();
        }
    }

    private void SpawnInimigos(GameObject[] prefabsDaHorda, int quantidade)
    {
        inimigosSpawnados = 0;

        for (int i = 0; i < quantidade; i++)
        {
            GameObject prefabSelecionado = prefabsDaHorda[Random.Range(0, prefabsDaHorda.Length)];
            Transform spawnPoint = GetRandomSpawnPoint(prefabsDaHorda);

            Vector3 spawnPosition = spawnPoint.position;
            spawnPosition.z = posicaoZ;

            GameObject novoInimigo = Instantiate(prefabSelecionado, spawnPosition, Quaternion.identity);
            VidaDoInimigo vidaInimigo = novoInimigo.GetComponent<VidaDoInimigo>();

            totalDeInimigos++;
            inimigosSpawnados++;
            novoInimigo.SetActive(true);

            // Aumentar a vida máxima dos inimigos
            if (cartasEscolhidas > 0)
            {
                vidaInimigo.vidaMaxima += Mathf.RoundToInt(aumentoDeDanoEntreHordas * vidaInimigo.vidaMaxima);
            }
        }
    }

    private Transform GetRandomSpawnPoint(GameObject[] prefabsDaHorda)
    {
        if (prefabsDaHorda == inimigosPrefab)
        {
            int spawnIndex = GetRandomUnusedSpawnIndex(usedSpawnPointsInimigos);
            usedSpawnPointsInimigos[spawnIndex] = true;
            return spawnPointsInimigos[spawnIndex];
        }
        else
        {
            int spawnIndex = GetRandomUnusedSpawnIndex(usedSpawnPointsBoss);
            usedSpawnPointsBoss[spawnIndex] = true;
            return spawnPointsBoss[spawnIndex];
        }
    }

    private int GetRandomUnusedSpawnIndex(bool[] usedSpawnPoints)
    {
        int spawnIndex = Random.Range(0, usedSpawnPoints.Length);
        while (usedSpawnPoints[spawnIndex])
        {
            spawnIndex = (spawnIndex + 1) % usedSpawnPoints.Length;
        }
        return spawnIndex;
    }

    private void ResetUsedSpawnPoints()
    {
        for (int i = 0; i < usedSpawnPointsInimigos.Length; i++)
        {
            usedSpawnPointsInimigos[i] = false;
        }
        for (int i = 0; i < usedSpawnPointsBoss.Length; i++)
        {
            usedSpawnPointsBoss[i] = false;
        }
    }

    public void InimigoDestruido(bool isBoss)
    {
        contadorHorda--;

        if (isBoss)
        {
            hordasDesdeUltimoBoss = 0;
            contadorHorda = 0;
        }

        if (contadorHorda == 0)
        {
            if (gerenciadorDeCartas != null)
            {
                gerenciadorDeCartas.InstanciarCartas(this.cartasEscolhidas);
            }

            // Reative os pontos de spawn ao finalizar a horda
            SetSpawnPointsActive(spawnPointsInimigos, true);
            SetSpawnPointsActive(spawnPointsBoss, true);

            isHordaAtiva = false;
        }
    }

    private void SetSpawnPointsActive(Transform[] spawnPoints, bool isActive)
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            spawnPoint.gameObject.SetActive(isActive);
        }
    }
}
