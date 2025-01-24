// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class CartaPrefab : MonoBehaviour
// {
//     public Sprite[] spritesDasCartas; // Array de sprites das cartas
//     public int atributoEscolhido; // O índice do atributo escolhido
//     public TextMeshProUGUI atributoText; // Texto para exibir o atributo da carta
//     public Button botaoMudarSprite; // Botão 2D na tela para mudar o sprite
//     public string atributoPersonalizado; // Atributo definido manualmente como uma string pública

//     public float valorAleatorioMinimo = 1.0f; // Valor mínimo aleatório do atributo (float)
//     public float valorAleatorioMaximo = 11.0f; // Valor máximo aleatório do atributo (float)

//     private float valorAleatorio; // Valor aleatório do atributo (float)

//     private void Start()
//     {
//         if (!string.IsNullOrEmpty(atributoPersonalizado))
//         {
//             // Se um atributo personalizado foi definido, use-o
//             if (atributoText != null)
//             {
//                 atributoText.text = "Atributo: " + atributoPersonalizado;
//             }
//         }
//         else
//         {
//             // Verifique se o índice do atributo escolhido é válido
//             if (atributoEscolhido >= 0 && atributoEscolhido < spritesDasCartas.Length)
//             {
//                 // Defina o sprite da carta com base no atributo escolhido
//                 Image imagemCarta = GetComponent<Image>();
//                 imagemCarta.sprite = spritesDasCartas[atributoEscolhido];

//                 // Gere um valor aleatório para o atributo dentro dos limites definidos
//                 valorAleatorio = Random.Range(valorAleatorioMinimo, valorAleatorioMaximo);

//                 // Atualize o texto do atributo como uma string pública
//                 if (atributoText != null)
//                 {
//                     atributoText.text = "Atributo: " + valorAleatorio.ToString("F2"); // Exibe o float com 2 casas decimais
//                 }
//             }
//             else
//             {
//                 Debug.LogError("Índice de atributo inválido!");
//             }
//         }

//         // Adicione um evento de clique ao botão 2D para mudar o sprite
//         if (botaoMudarSprite != null)
//         {
//             botaoMudarSprite.onClick.AddListener(MudarSpriteDoBotao);
//         }
//     }

//     // Função para obter o valor aleatório do atributo (float)
//     public float ObterValorAleatorioDoAtributo()
//     {
//         return valorAleatorio;
//     }
//     public string ObterNomeDoAtributo()
//     {
//         return atributoPersonalizado;
//     }

//     // Função para mudar o sprite do botão
//     private void MudarSpriteDoBotao()
//     {
//         if (botaoMudarSprite != null)
//         {
//             Image imagemBotao = botaoMudarSprite.GetComponent<Image>();
//             if (imagemBotao != null && atributoEscolhido >= 0 && atributoEscolhido < spritesDasCartas.Length)
//             {
//                 imagemBotao.sprite = spritesDasCartas[atributoEscolhido];
//             }
//         }
//     }
// }

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartaPrefab : MonoBehaviour
{
    [SerializeField] private Sprite[] spritesDasCartas; // Array com os sprites das cartas
    [SerializeField] private int atributoEscolhido; // Índice do atributo escolhido
    [SerializeField] private TextMeshProUGUI atributoText; // Texto que exibe o atributo
    [SerializeField] private Button botaoMudarSprite; // Referência ao botão que mudará o sprite
    [SerializeField] private string atributoPersonalizado; // Nome do atributo, se houver

    [SerializeField] private float valorAleatorioMinimo = 1.0f; // Valor mínimo do valor aleatório
    [SerializeField] private float valorAleatorioMaximo = 11.0f; // Valor máximo do valor aleatório

    private float valorAleatorio;

    private void Awake()
    {
        if (atributoText == null || botaoMudarSprite == null || spritesDasCartas == null)
        {
            Debug.LogError("Certifique-se de que todos os campos obrigatórios estão atribuídos no Inspector!");
        }
    }

    private void Start()
    {
        // Inicializa a carta e o sprite do botão assim que o jogo começar
        if (!string.IsNullOrEmpty(atributoPersonalizado))
        {
            GerarValorAleatorio();
            AtualizarTextoAtributo(atributoPersonalizado);
        }
        else
        {
            InicializarCarta();
        }

        // Muda o sprite do botão na inicialização, sem necessidade de clique
        MudarSpriteDoBotao();
    }

    private bool AtributoValido(int indice)
    {
        // Verifica se o índice do atributo está dentro do intervalo válido
        return indice >= 0 && indice < spritesDasCartas.Length;
    }

    // private void AtualizarTextoAtributo(string texto)
    // {
    //     // Atualiza o texto que exibe o atributo
    //     if (atributoText != null)
    //     {
    //         atributoText.text = atributoPersonalizado + $"\n {valorAleatorio}";
    //     }
    // }


    public void AtualizarValorAleatorio()
    {
        // Gera o valor aleatório
        valorAleatorio = GerarValorAleatorio();
        
        // Verifica se o valor gerado está dentro do intervalo esperado
        if (valorAleatorio == 0)
        {
            Debug.LogWarning("O valor aleatório gerado é 0. Verifique os valores mínimo e máximo.");
        }else        {
            Debug.LogWarning("O valor aleatório ainda não foi gerado ou é zero.");
        }


        // Atualiza o texto exibido com o novo valor aleatório
        AtualizarTextoAtributo(valorAleatorio.ToString("F2"));
    }

    public float GerarValorAleatorio()
    {
        if (valorAleatorioMinimo >= valorAleatorioMaximo)
        {
            Debug.LogError("O valor mínimo é maior ou igual ao valor máximo! Corrija os valores.");
            return valorAleatorioMinimo; // Retorna o valor mínimo em caso de erro.
        }

        // Gera um valor aleatório entre o valor mínimo e o valor máximo
        return Random.Range(valorAleatorioMinimo, valorAleatorioMaximo);
    }

    public void InicializarCarta()
    {
        // Verifica se o índice do atributo é válido e aplica o sprite
        if (AtributoValido(atributoEscolhido))
        {
            // Acessa a imagem da carta e define o sprite
            GetComponent<Image>().sprite = spritesDasCartas[atributoEscolhido];
            
            // Gera o valor aleatório corretamente aqui
            GerarValorAleatorio();
            valorAleatorio = GerarValorAleatorio(); // Gera o valor aleatório
            AtualizarTextoAtributo(valorAleatorio.ToString("F2")); // Atualiza o texto com o valor aleatório gerado
        }
        else
        {
            Debug.LogError($"Índice {atributoEscolhido} é inválido!");
        }
    }
    public void AtualizarTextoAtributo(string texto)
    {
        // Atualiza o texto que exibe o atributo
        if (atributoText != null && valorAleatorio != 0)
        {
            atributoText.text = atributoPersonalizado + $"\n {valorAleatorio:F2}";
        }
        else
        {
            Debug.LogWarning("O valor aleatório ainda não foi gerado ou é zero.");
        }
    }
    public float ObterValorAleatorioDoAtributo()
    {
        // Verifica se o texto está disponível e tenta extrair o valor numérico dele
        if (atributoText != null)
        {
            // Usa a substring após o último espaço para pegar o valor numérico
            string texto = atributoText.text;
            string valorString = texto.Substring(texto.LastIndexOf("\n") + 1).Trim();

            // Tenta converter o valor extraído para float
            if (float.TryParse(valorString, out float valorConvertido))
            {
                return valorConvertido;  // Retorna o valor numérico extraído
            }
        }
        return 0;  // Retorna 0 se não conseguir extrair o valor ou se o valor for inválido
    }


    public string ObterNomeDoAtributo() => atributoPersonalizado;

    private void MudarSpriteDoBotao()
    {
        // Verifica se o botão e o índice são válidos
        if (botaoMudarSprite != null && AtributoValido(atributoEscolhido))
        {
            // Acessa o componente Image do botão
            Image imagemBotao = botaoMudarSprite.GetComponent<Image>();

            // Verifica se o componente Image foi encontrado
            if (imagemBotao != null && spritesDasCartas.Length > 0)
            {
                // Atribui o sprite do array para o botão
                imagemBotao.sprite = spritesDasCartas[atributoEscolhido];

                // Atualiza o texto, caso necessário
                AtualizarTextoAtributo($"Atributo: {atributoEscolhido + 1}"); // Exemplo de exibição do índice
            }
            else
            {
                Debug.LogError("Imagem do botão ou lista de sprites não encontrada!");
            }
        }
        else
        {
            Debug.LogWarning("Botão ou índice inválido!");
        }
    }
}