using UnityEngine;
using UnityEngine.UI;

public class ColorLifeEnemy : MonoBehaviour
{
    public Image image;
    public CorLenteEnemy[] inimigos;
    private Color corAleatoria;
    public Color corAleatoria2;


    void Start()
    {
        image = GetComponent<Image>();
        // CorLenteEnemy = FindObjectOfType<CorLenteEnemy>();
        inimigos = FindObjectsOfType<CorLenteEnemy>();
    }
    void Update(){
        MudarCorDaImagem();
    }

    public void MudarCorDaImagem()
    {
        // Acesse a cor aleatória em formato Color32
        // image.GetComponent<Image>().color = new Color32(255,255,225,100);
        corAleatoria = new Color32(255,255,225,100);
        corAleatoria = corAleatoria2;
        // Defina a cor da imagem
        image.color = corAleatoria;
    }
}
