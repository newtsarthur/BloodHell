using UnityEngine;
using System.Collections.Generic;

public class Inimigo : MonoBehaviour
{
    public Transform jogador; // Referência ao jogador
    public float velocidadeMovimento = 1.0f; // Velocidade de movimento do inimigo
    public List<GameObject> bulletPrefabs; // Lista de prefabs de tiro
    public Transform firePoint; // Ponto de disparo dos tiros
    public float intervaloDeDisparo = 2.0f; // Intervalo entre os disparos
    // public float tempoDeVidaDoTiro = 25.0f; // Tempo de vida dos tiros
    public float raioDeDetecção = 5.0f; // Raio de detecção do jogador
    public float velocidadeDoTiro = 10.0f; // Velocidade dos tiros
    public LayerMask obstaculosLayer; // Camada de obstáculos

    private float tempoParaProximoDisparo;
    private bool seguirJogador;

    public float friccao = 0.2f;

    public Rigidbody2D rb;   

    bullet bullet;

    VidaDoJogador VidaDoJogador;

    public float speed = 10f;

    void Start()
    {
        tempoParaProximoDisparo = 0f;
        seguirJogador = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.mass = 10.0f; // Ajuste a massa conforme necessário
        bullet = FindObjectOfType<bullet>();
        VidaDoJogador = FindObjectOfType<VidaDoJogador>();


        // Certifique-se de que você tenha configurado os prefabs de tiro na lista "bulletPrefabs" no Unity Inspector.
    }

    void Update()
    {
        float distanciaAoJogador = Vector2.Distance(transform.position, jogador.position);

        if (distanciaAoJogador <= raioDeDetecção)
        {
            Vector3 direcaoDoJogador = (jogador.position - transform.position).normalized;
            float angulo = Mathf.Atan2(direcaoDoJogador.y, direcaoDoJogador.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angulo);

            if (Time.time >= tempoParaProximoDisparo)
            {
                Disparar();
                tempoParaProximoDisparo = Time.time + 1f / intervaloDeDisparo;
            }
        }
        else
        {
            if (jogador.position.x < transform.position.x)
            {
                velocidadeMovimento = -Mathf.Abs(velocidadeMovimento);
            }
            else
            {
                velocidadeMovimento = Mathf.Abs(velocidadeMovimento);
            }

            RecalcularRota();
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -0.148f);
    }

    void Disparar()
    {
        if (bulletPrefabs.Count > 0)
        {
            
            // GameObject firedBullet = Instantiate(bullet, barrelTip.position, Quaternion.identity);

            // Atribua o valor da variável quantidade ao danoDoBullet do bullet
            // bullet bulletScript = firedBullet.GetComponent<bullet>();

            int randomIndex = Random.Range(0, bulletPrefabs.Count);
            GameObject selectedBullet = bulletPrefabs[randomIndex];
            
            Vector3 direcaoDoTiro = (jogador.position - firePoint.position);

                    // Defina a velocidade da bala com base na direção e na velocidade
            Vector2 bulletVelocity = direcaoDoTiro.normalized * speed;

            GameObject tiro = Instantiate(selectedBullet, firePoint.position, Quaternion.identity);
            Rigidbody2D rbTiro = tiro.GetComponent<Rigidbody2D>();
            rbTiro.velocity = bulletVelocity;
            // VidaDoJogador.ReceberDano(bullet.danoDoBulletEnemy);
            // Debug.Log("Índice aleatório gerado: " + randomIndex);
            Destroy(tiro, 8f);
        }
    }

    void RecalcularRota()
    {
        Vector3 direcao = jogador.position - transform.position;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
        transform.Translate(direcao.normalized * velocidadeMovimento * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (seguirJogador)
        {
            RecalcularRota();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDeDetecção);
    }
}
