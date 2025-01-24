using UnityEngine;

public class grama : MonoBehaviour
{
    public float inclinacaoMaxima = 15.0f; // Ângulo máximo de inclinação
    public float inclinacaoJogador = 15.0f; // Ângulo de inclinação quando o jogador colide
    public float velocidadeRetorno = 1000000.0f; // Velocidade de retorno da grama
    private bool jogadorNaGrama = false;
    private Collider2D gramaCollider;
    private Collision2D jogadorCollision;
    private Quaternion rotacaoOriginal;
    private float direcao = 1.0f;
    private Animator animator;

    move move;

    private float contador = 0.3f;
    public float taxaDeDecrescimento = 0.1f;

    private void Start()
    {
        gramaCollider = GetComponent<Collider2D>();
        rotacaoOriginal = transform.rotation;
        animator = GetComponent<Animator>();
        move = FindObjectOfType<move>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            jogadorNaGrama = true;
            gramaCollider.isTrigger = true; // Ativa IsTrigger quando o jogador colide com a grama
            jogadorCollision = collision; // Armazena a colisão para uso no Update
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            jogadorNaGrama = false;
            gramaCollider.isTrigger = false; // Desativa IsTrigger quando o jogador sai da grama
        }
    }

    private void Update()
    {
        if (jogadorNaGrama && jogadorCollision != null)
        {
            // Calcule o ângulo de inclinação com base na velocidade do jogador.
            float velocidadeJogador = jogadorCollision.relativeVelocity.magnitude;
            float anguloInclinacao = Mathf.Clamp(velocidadeJogador, 0, inclinacaoJogador);

            // Aplique a rotação à grama
            transform.rotation = rotacaoOriginal * Quaternion.Euler(0, 0, anguloInclinacao * direcao);

            if(move.horizontal < 0) {
                animator.SetBool("IsFacingRight", false); // Define o parâmetro para verdadeiro
                animator.SetBool("Left", false); // Define o parâmetro para verdadeiro
                animator.SetBool("Right", true);
                 // Define o parâmetro para verdadeiro
                animator.Play("PlantsRight"); // Define o parâmetro para verdadeiro
            }

            if(move.horizontal > 0) {
                animator.SetBool("IsFacingRight", false); // Define o parâmetro para verdadeiro
                animator.SetBool("Left", true); // Define o parâmetro para verdadeiro
                animator.SetBool("Right", false); // Define o parâmetro para verdadeiro
                animator.Play("PlantsLeft"); // Define o parâmetro para verdadeiro
            }


            // Inverta a direção se a inclinação ultrapassar o limite
            if (Mathf.Abs(anguloInclinacao) >= inclinacaoMaxima)
            {
                direcao *= -1.0f;
            }
        }
        else
        {
            // Retorne suavemente à rotação original quando o jogador sai da grama
            Quaternion alvoRotacao = Quaternion.Euler(0, 0, 0.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, alvoRotacao, Time.deltaTime * velocidadeRetorno);
            
        if (contador > 0)
        {
            contador -= taxaDeDecrescimento * Time.deltaTime;
            if(contador <= 0)
            {
                animator.SetBool("Right", false);
                animator.SetBool("Left", false);
                animator.SetBool("IsFacingRight", true);
                contador = 0.3f;

            }
        }

        }
    }
}