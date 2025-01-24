using UnityEngine;
using UnityEngine.UI;

public class AtualizadorDeBarraDeVidaInimigo : MonoBehaviour
{
    public VidaDoInimigo vidaDoInimigo; // Referência ao script de vida do inimigo
    public Slider barraDeVida; // Referência à barra de vida na UI

    void Start()
    {
        // vidaDoInimigo = FindObjectOfType<VidaDoInimigo>(); // Obtenha a referência ao script de vida do inimigo no GameObject atual

        // Certifique-se de que as referências estejam corretamente atribuídas no Unity
        if (vidaDoInimigo == null || barraDeVida == null)
        {
            Debug.LogError("As referências não foram atribuídas corretamente.");
            enabled = false; // Desativa o script se as referências não estiverem corretas
            return;
        }
    }

    private void Update()
    {
        // Atualize o valor da barra de vida de acordo com a vida atual do inimigo
        barraDeVida.value = (float)vidaDoInimigo.vidaAtual / vidaDoInimigo.vidaMaxima;
    }
}
