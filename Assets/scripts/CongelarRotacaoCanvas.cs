using UnityEngine;

public class CongelarRotacaoCanvas : MonoBehaviour
{
    public RectTransform canvasRectTransform;
    // public SpriteRenderer spriteRenderer;
    // spriteRenderer = GetComponent<SpriteRenderer>();

    private void Start()
    {
        // Obtenha o RectTransform do Canvas
        canvasRectTransform = GetComponent<RectTransform>();

        // Defina a rotação Z para 0
        Vector3 novaRotacao = canvasRectTransform.rotation.eulerAngles;
        novaRotacao.z = 0;
        canvasRectTransform.rotation = Quaternion.Euler(novaRotacao);
    }
    private void Update()
    {
        // Define a rotação Z do Canvas para 0 graus
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
