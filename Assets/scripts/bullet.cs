using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public string camadaParaIgnorar = "grana"; // Nome da camada a ser ignorada
    public float tempoDeIgnorarColisao = 0.5f; // Tempo de duração da ignorar colisão
    public ParticleSystem[] explosaoPrefab; // Prefab da explosão de partículas
    public LayerMask camadasAlvo; // Camadas com as quais o bullet deve colidir
    public LayerMask camadasEnemy; // Camadas com as quais o bullet deve colidir
    public LayerMask camadasPlayer; // Camadas com as quais o bullet deve colidir


    private float tempoDecorrido = 0.0f;
    private int camadaParaIgnorarIndex;

    public int danoDoBulletEnemy = 10;
    public int danoDoBulletPlayer = 10;

    private ParticleSystem explosao; // Declare a variável fora do método

    VidaDoJogador VidaDoJogador;

    VidaDoInimigo VidaDoInimigo;

    // public CorLenteEnemy CorLenteEnemy;

    // public Color corAleatoria;

    public GameObject[] sanguePrefab;
    private List<GameObject> sangueNoChao = new List<GameObject>();

    private float tempoDesdeInstanciacaoSangue = 0f;


    private void Start()
    {
        // Obtenha o índice da camada a ser ignorada
        camadaParaIgnorarIndex = LayerMask.NameToLayer(camadaParaIgnorar);
        VidaDoJogador = FindObjectOfType<VidaDoJogador>();
        VidaDoInimigo = FindObjectOfType<VidaDoInimigo>();

        // CorLenteEnemy = FindObjectOfType<CorLenteEnemy>();

    }

    private void Update()
    {
        // tempoDesdeInstanciacaoSangue += Time.deltaTime;
        if (tempoDecorrido > 0)
        {
            // Temporariamente ignore a colisão com a camada especificada
            // Physics2D.IgnoreLayerCollision(gameObject.layer, camadaParaIgnorarIndex, true);
            tempoDecorrido -= Time.deltaTime;
        }
        else
        {
            // Reative a colisão após o período de tempo especificado
            Physics2D.IgnoreLayerCollision(gameObject.layer, camadaParaIgnorarIndex, false);
        }

        // Verifica se o bullet colidiu diretamente com as camadas alvo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.1f, camadasAlvo);
        if (hit.collider != null)
        {
            // Lidar com a colisão com as camadas alvo
            ExplodirColor();
        }

        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, transform.up, 0.1f, camadasEnemy);
        if (hitEnemy.collider != null)
        {
            // Lidar com a colisão com as camadas alvo
            ExplodirEnemy();

            // Verifique se o objeto atingido é um inimigo
            VidaDoInimigo vidaInimigo = hitEnemy.collider.GetComponent<VidaDoInimigo>();

            if (vidaInimigo != null)
            {
                vidaInimigo.ReceberDano(danoDoBulletPlayer);
            }
        }

        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, transform.up, 0.1f, camadasPlayer);
        if (hitPlayer.collider != null)
        {
            // Lidar com a colisão com as camadas alvo
            Explodir();
            // StartCoroutine(DestruirExplosaoAposTempo(1f));
            VidaDoJogador.ReceberDano(danoDoBulletEnemy);
            // if (sangueNoChao.Count >= 3)
            // {
            //     DestroySangueMaisAntigo();
            // }
        }

    }

private void Explodir()
{
    // Obtenha a posição atual do objeto bullet
    Vector3 bulletPosition = transform.position;
    bulletPosition.z = 0f;

    // Escolha aleatoriamente um prefab de sangue da matriz
    int randomIndex = Random.Range(0, sanguePrefab.Length);
    GameObject sangue = Instantiate(sanguePrefab[randomIndex], bulletPosition, Quaternion.identity);

    // Congele a posição Z em 0.13
    Vector3 sanguePosition = sangue.transform.position;
    sanguePosition.z = 0.13f;
    sangue.transform.position = sanguePosition;

    // Adicione o sangue à lista de sangue no chão
    sangueNoChao.Add(sangue);

    ParticleSystem explosao = Instantiate(explosaoPrefab[0], bulletPosition, Quaternion.identity);
    explosao.Play();
    // Destruir o objeto bullet
        
    Destroy(gameObject);

    // Destruir a explosão de partículas após o período de tempo
    Destroy(explosao.gameObject, explosao.main.duration);

    // Destruir o sangue após 3 segundos
    Destroy(sangue, 6f);

    // if (sangueNoChao.Count >= 3)
    // {
    //     // Destroy(sangue, 6f);
    //     DestroySangueMaisAntigo();
    // }
    // if (sangueNoChao.Count >= 3)
    // {
    //     DestroySangueMaisAntigo();
    // }
}



    private IEnumerator DestruirSangueAposTempo(GameObject sangue, float tempo)
    {
        yield return new WaitForSeconds(tempo);
        sangueNoChao.Remove(sangue);
        Destroy(sangue);
    }

        private void ExplodirEnemy()
    {
    // Obtenha a posição atual do objeto bullet
    Vector3 bulletPosition = transform.position;
    bulletPosition.z = 0f;

    // Escolha aleatoriamente um prefab de sangue da matriz
    int randomIndex = Random.Range(0, sanguePrefab.Length);
    GameObject sangue = Instantiate(sanguePrefab[randomIndex], bulletPosition, Quaternion.identity);

    // Congele a posição Z em 0.13
    Vector3 sanguePosition = sangue.transform.position;
    sanguePosition.z = 0.13f;
    sangue.transform.position = sanguePosition;

    // Adicione o sangue à lista de sangue no chão
    sangueNoChao.Add(sangue);

    ParticleSystem explosao = Instantiate(explosaoPrefab[0], bulletPosition, Quaternion.identity);
    explosao.Play();
    // Destruir o objeto bullet
        
    Destroy(gameObject);

    // Destruir a explosão de partículas após o período de tempo
    Destroy(explosao.gameObject, explosao.main.duration);

    // Destruir o sangue após 3 segundos
    Destroy(sangue, 6f);

    // if (sangueNoChao.Count >= 3)
    // {
    //     // Destroy(sangue, 6f);
    //     DestroySangueMaisAntigo();
    // }
    // if (sangueNoChao.Count >= 3)
    // {
    //     DestroySangueMaisAntigo();
    // }
    }

    // private IEnumerator DestruirExplosaoAposTempo(float tempo)
    // {
    //     yield return new WaitForSeconds(tempo);
    //     Explodir();
    // }

    // private void Explodir()
    // {
    //     // Obtenha a posição atual do objeto bullet
    //     Vector3 bulletPosition = transform.position;
        
    //     // Defina a posição Z para zero
    //     bulletPosition.z = 0f;

    //     // Instanciar a explosão de partículas na posição atualizada
    //     ParticleSystem explosao = Instantiate(explosaoPrefab[0], bulletPosition, Quaternion.identity);
    //     explosao.Play();
    //     // Destruir o objeto bullet
    //     Destroy(gameObject);

    //     // Destruir a explosão de partículas após o período de tempo
    //     Destroy(explosao.gameObject, explosao.main.duration);
    // }
        private void ExplodirColor()
    {
        // Obtenha a posição atual do objeto bullet
        Vector3 bulletPosition = transform.position;
        
        // Defina a posição Z para zero
        bulletPosition.z = 0f;

        // Instanciar a explosão de partículas na posição atualizada
        ParticleSystem explosao = Instantiate(explosaoPrefab[1], bulletPosition, Quaternion.identity);
        explosao.Play();
        // Destruir o objeto bullet
        Destroy(gameObject);

        // Destruir a explosão de partículas após o período de tempo
        Destroy(explosao.gameObject, explosao.main.duration);
    }

// private void DestroySangueMaisAntigo()
// {
//     if (sangueNoChao.Count > 0)
//     {
//         GameObject sangueMaisAntigo = sangueNoChao[0];
//         sangueNoChao.RemoveAt(0);
//         Destroy(sangueMaisAntigo);
//     }
// }


}
