using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VidaDoInimigo : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaAtual;
    public float duracaoCorDeDano = 0.0000000000000000000000000000000000000000000000000001f; // Duração da cor de dano

    public SpriteRenderer spriteRenderer;
    public Material originalMaterial;
    public Material damageMaterial; // Atribua o Material de dano invertido aqui

    private float corDeDanoTimer = 0f;
    private bool isCorDeDanoAtiva = false;
    // Inimigo inimigo;
    public bool isBoss;
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
            spriteRenderer.material = damageMaterial; // Altera para o Material de dano invertido
            isCorDeDanoAtiva = true;
            corDeDanoTimer = duracaoCorDeDano;
        }
        vidaAtual -= quantidade;

        // if (vidaAtual <= 0)
        // {
        //     // Implemente a lógica para o inimigo morrer, como tocar uma animação de morte ou desativar o GameObject.
        //     Destroy(gameObject);
        // }

        if (vidaAtual <= 0)
        {
            // bool isBoss = /* defina se este inimigo é um chefe ou não */;
            
            // O inimigo foi derrotado, chame a função InimigoDestruido no GerenciadorDeHordas
            GerenciadorDeHordas gerenciadorHordas = FindObjectOfType<GerenciadorDeHordas>();
            if (gerenciadorHordas != null)
            {
                gerenciadorHordas.InimigoDestruido(isBoss);
            }

            // Destrua o objeto do inimigo
            Destroy(gameObject);
        }
    }

    public void Curar(int quantidade)
    {
        vidaAtual = Mathf.Min(vidaAtual + quantidade, vidaMaxima);
    }
}
