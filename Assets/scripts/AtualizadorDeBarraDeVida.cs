using UnityEngine;
using UnityEngine.UI;

public class AtualizadorDeBarraDeVida : MonoBehaviour
{
    public VidaDoJogador vidaDoJogador; // Referência ao script de vida do jogador
    public Slider barraDeVida; // Referência à barra de vida na UI

    void Start()
    {
        vidaDoJogador = FindObjectOfType<VidaDoJogador>();

        
        // Certifique-se de que as referências estejam corretamente atribuídas no Unity
        if (vidaDoJogador == null || barraDeVida == null)
        {
            Debug.LogError("As referências não foram atribuídas corretamente.");
            enabled = false; // Desativa o script se as referências não estiverem corretas
            return;
        }
    }

    private void Update()
    {
        // Atualize o valor da barra de vida de acordo com a vida atual do jogador
        barraDeVida.value = (float)vidaDoJogador.vidaAtual / vidaDoJogador.vidaMaxima;
    }
}
