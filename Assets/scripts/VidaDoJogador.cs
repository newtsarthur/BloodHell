using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaDoJogador : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaAtual;
    public float duracaoCorDeDano = 0.0000000000000000000000000000000000000000000000000001f; // Duração da cor de dano

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public Material damageMaterial; // Atribua o Material de dano invertido aqui

    private float corDeDanoTimer = 0f;
    private bool isCorDeDanoAtiva = false;

    private void Start()
    {
        vidaAtual = vidaMaxima;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material; // Salva o material original
    }

    private void Update()
    {
        if (isCorDeDanoAtiva)
        {
            corDeDanoTimer -= Time.deltaTime;
            if (corDeDanoTimer <= 0)
            {
                // A cor de dano expirou, volte ao material original
                spriteRenderer.material = originalMaterial;
                isCorDeDanoAtiva = false;
            }
        }
    }

    public void ReceberDano(int quantidade)
    {
        if (!isCorDeDanoAtiva)
        {
            // Apenas se a cor de dano não estiver ativa, mude o material do sprite e inicie o temporizador
            isCorDeDanoAtiva = true;
            spriteRenderer.material = damageMaterial; // Altera para o Material de dano invertido
            corDeDanoTimer = duracaoCorDeDano;
        }
        vidaAtual -= quantidade;

        if (vidaAtual <= 0)
        {
            // Implemente a lógica para o jogador morrer, como reiniciar o jogo ou mostrar uma tela de derrota.
        }
    }

    public void Curar(int quantidade)
    {
        vidaAtual = Mathf.Min(vidaAtual + quantidade, vidaMaxima);
    }
}
