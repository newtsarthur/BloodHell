using UnityEngine;

public class BloodSpawner : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float inactivityTimer = 5.0f; // Tempo de inatividade antes de destruir

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Desative o SpriteRenderer no início
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        if (spriteRenderer.enabled)
        {
            // Se o SpriteRenderer estiver visível, inicie o temporizador de inatividade
            inactivityTimer -= Time.deltaTime;
            if (inactivityTimer <= 0)
            {
                // O tempo de inatividade expirou, então desative o SpriteRenderer e aguarde para destruir o objeto
                spriteRenderer.enabled = false;
            }
        }
    }

    public void SpawnBlood(Vector2 position)
    {
        // Zere o temporizador de inatividade
        inactivityTimer = 5.0f;

        // Defina a posição do objeto de sangue
        transform.position = new Vector3(position.x, position.y, 0.13f);

        // Ative o SpriteRenderer para mostrar o sangue
        spriteRenderer.enabled = true;
    }
}
