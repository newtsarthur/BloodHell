using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorDeCartas : MonoBehaviour
{
    public Button[] cartasPrefab; // Array de botões que representam as cartas
    public Transform[] spawnPointsCartas; // Locais de spawn para as cartas
    public GameObject cartasContainer; // Objeto que contém as cartas
    public bool movimentoTravado = true; // Variável para controlar o movimento do jogador
    public Text contadorCartasText; // Texto para exibir a contagem de cartas escolhidas

    private List<Button> cartasExibidas; // Lista de botões que representam as cartas exibidas na tela
    public int cartasEscolhidas = 0; // Contador de cartas escolhidas
    private const float posicaoZ = -4.996948f; // Valor da posição Z
    public GerenciadorDeHordas gerenciadorDeHordas; // Referência ao script GerenciadorDeHordas

    public move move;
    private void Start()
    {
        move = FindObjectOfType<move>();

        cartasExibidas = new List<Button>();

        // Embaralhe as cartas
        ShuffleCartas();

        // Atualize o contador de cartas
        AtualizarContadorCartas();

        // Desative o contêiner de cartas no início
        if (cartasContainer != null)
        {
            cartasContainer.SetActive(false);
        }

        // Instancia e ativa os botões das cartas no início
    for (int i = 0; i < cartasPrefab.Length && i < spawnPointsCartas.Length; i++)
    {
        Button carta = Instantiate(cartasPrefab[i], spawnPointsCartas[i]);
        
        // Atualiza a posição para Z = 0
        carta.transform.position = new Vector3(
            spawnPointsCartas[i].position.x, 
            spawnPointsCartas[i].position.y, 
            0f // Define o Z diretamente como 0
        );

        // Associe um evento de clique ao botão da carta
        int index = i; // Capturar o valor de 'i' para uso no evento
        carta.onClick.AddListener(() => EscolherCarta(index));

        // Adiciona a carta à lista de exibidas
        cartasExibidas.Add(carta);

        // Exibe no log a posição da carta, agora com Z = 0
        Debug.Log($"Carta {index} posicionada em ({carta.transform.position.x}, {carta.transform.position.y}, {carta.transform.position.z})");
    }

    }
    // void Update(){
    //     if(gerenciadorDeHordas.contadorHorda == 0) {
    //     // Instancia e ativa os botões das cartas no início
    //         for (int i = 0; i < cartasPrefab.Length && i < spawnPointsCartas.Length; i++)
    //         {
    //             Button carta = Instantiate(cartasPrefab[i], spawnPointsCartas[i]);
    //             carta.transform.position = spawnPointsCartas[i].position; // Define a posição do botão

    //             // Associe um evento de clique ao botão da carta
    //             int index = i; // Capturar o valor de 'i' para uso no evento
    //             carta.onClick.AddListener(() => EscolherCarta(index));

    //             cartasExibidas.Add(carta);
    //         }
    //     }
    // }
    // Embaralhe as cartas aleatoriamente
    private void ShuffleCartas()
    {
        // Cria uma lista de índices para as cartas
        List<Button> cartasEmbaralhadas = new List<Button>(cartasPrefab);

        // Embaralha a lista de cartas
        for (int i = 0; i < cartasEmbaralhadas.Count; i++)
        {
            int randomIndex = Random.Range(i, cartasEmbaralhadas.Count);

            // Troca os elementos de posição
            Button temp = cartasEmbaralhadas[i];
            cartasEmbaralhadas[i] = cartasEmbaralhadas[randomIndex];
            cartasEmbaralhadas[randomIndex] = temp;

            // Atualiza o valor aleatório da carta
            CartaPrefab cartaPrefabScript = cartasEmbaralhadas[i].GetComponent<CartaPrefab>();
            if (cartaPrefabScript != null)
            {
                cartaPrefabScript.AtualizarValorAleatorio();  // Atualiza o valor aleatório para cada carta
            }
        }

        // Agora a lista embaralhada contém as cartas na ordem aleatória
        // Pode adicionar as cartas embaralhadas à lista de exibidas ou outro processo conforme necessário
        cartasPrefab = cartasEmbaralhadas.ToArray(); // Atualiza o array original de cartas
    }
    // private void AtualizarValoresAleatoriosDasCartas()
    // {
    //     foreach (var carta in cartasExibidas)
    //     {
    //         CartaPrefab cartaPrefab = carta.GetComponent<CartaPrefab>();
    //         if (cartaPrefab != null)
    //         {
    //             cartaPrefab.AtualizarValorAleatorio();
    //         }
    //     }
    // }

    // Lógica para escolher e atribuir a carta ao jogador
    private void EscolherCarta(int index)
    {
        if (index >= 0 && index < cartasExibidas.Count)
        {
            // Obtenha o valor do atributo da carta
            float valorDoAtributo = cartasExibidas[index].GetComponent<CartaPrefab>().ObterValorAleatorioDoAtributo();
            Debug.Log($"Retornando valor do atributo: {valorDoAtributo}");
            string nomeDoAtributo = cartasExibidas[index].GetComponent<CartaPrefab>().ObterNomeDoAtributo();
            // Adicione o valor do atributo ao jogador
            if (move != null)
            {
                // move.atributoDoJogador += valorDoAtributo;
                move.nomeDoAtributoAdd = nomeDoAtributo;

                if (move.nomeDoAtributoAdd == move.atributos[0])
                {
                    // Force
                    move.atributoDoJogador += valorDoAtributo;

                    // Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + valorDoAtributo);
                }
                if (move.nomeDoAtributoAdd == move.atributos[1])
                {
                    // Speed
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[2])
                {
                    // HP
                    move.atributoDoJogador += valorDoAtributo;
                    
                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);}

                if (move.nomeDoAtributoAdd == move.atributos[3])
                {
                    // Defense
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[4])
                {
                    // HP Regeneration
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[5])
                {
                    // Armor Penetration
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[6])
                {
                    // Critical Chance
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[7])
                {
                    // Attack Speed
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[8])
                {
                    // Pet
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[9])
                {
                    // Invisibility
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[10])
                {
                    // Mutation
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[11])
                {
                    // Immortality
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[12])
                {
                    // Charm
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[13])
                {
                    // Artifacts
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[14])
                {
                    // Toxic
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[15])
                {
                    // Vampirism
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[16])
                {
                    // Alternate Reality
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[17])
                {
                    // Transformation
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[18])
                {
                    // Super Speed
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[19])
                {
                    // Wall of Fire
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[20])
                {
                    // Animal Spirit
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[21])
                {
                    // Zumbi
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[22])
                {
                    // Reincarnation
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[23])
                {
                    // Animal Spirit
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[24])
                {
                    // Poison Resistance
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[25])
                {
                    // Penetration
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[26])
                {
                    // Homing
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[27])
                {
                    // Poison
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[28])
                {
                    // Burst Fire
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[29])
                {
                    // Double Tap
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[30])
                {
                    // Particle
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[31])
                {
                    // Multidirectional
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[32])
                {
                    // Extended Range
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
                if (move.nomeDoAtributoAdd == move.atributos[33])
                {
                    // Incendiary
                    move.atributoDoJogador += valorDoAtributo;

                    Debug.Log(move.nomeDoAtributoAdd);
                    Debug.Log($"Valor do {nomeDoAtributo}: " + move.atributoDoJogador);
                }
            }
            // Atualize o contador de cartas e a UI
            cartasEscolhidas++;
            Debug.Log("Horda: " + cartasEscolhidas);
            AtualizarContadorCartas();
            movimentoTravado = true;

            // Chame a função IniciarHorda do GerenciadorDeHordas
            if (gerenciadorDeHordas != null)
            {
                gerenciadorDeHordas.IniciarHorda(cartasEscolhidas);
            }

            // Após atribuir os atributos ao jogador, ative o contêiner de cartas novamente
            if (cartasContainer != null)
            {
                cartasContainer.SetActive(true);
            }

            // Destrua todas as cartas
            // Em vez de destruir os botões, apenas desative-os
            for (int i = 0; i < cartasExibidas.Count; i++)
            {
                cartasExibidas[i].gameObject.SetActive(false); // Desativa o objeto do botão
            }


            // Desbloqueie o movimento do jogador
            movimentoTravado = false;
        }
    }

    // Atualize o texto do contador de cartas
    private void AtualizarContadorCartas()
    {
        if (contadorCartasText != null)
        {
            contadorCartasText.text = "Cartas Escolhidas: " + cartasEscolhidas;
        }
    }
public void InstanciarCartas(int cartasEscolhidas)
{
    // Embaralhe as cartas
    ShuffleCartas();

    // Atualize o contador de cartas
    AtualizarContadorCartas();

    // Atualize o contador de cartas com base na quantidade escolhida
    cartasEscolhidas = Mathf.Max(0, cartasEscolhidas);
    cartasEscolhidas = Mathf.Min(cartasEscolhidas, cartasPrefab.Length);

    // Destrua os botões das cartas
    for (int i = 0; i < cartasExibidas.Count; i++)
    {
        Destroy(cartasExibidas[i].gameObject);
    }

    cartasExibidas.Clear(); // Limpe a lista de botões exibidos

    // Verifique se o contadorHorda no gerenciadorDeHordas é igual a zero
    if (gerenciadorDeHordas != null && gerenciadorDeHordas.contadorHorda == 0)
    {
        // movimentoTravado = true;
        cartasContainer.SetActive(false);

        // Instancia e ativa novos botões de cartas
        for (int i = 0; i < cartasPrefab.Length && i < spawnPointsCartas.Length; i++)
        {
            Button carta = Instantiate(cartasPrefab[i], spawnPointsCartas[i]);
            carta.transform.position = spawnPointsCartas[i].position;

            int index = i;
            carta.onClick.AddListener(() => EscolherCarta(index));

            cartasExibidas.Add(carta);
        }
    }
}





}
