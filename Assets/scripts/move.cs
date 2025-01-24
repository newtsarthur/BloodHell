using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class move : MonoBehaviour
{
[SerializeField]
private float speed = 1.2f;
private Rigidbody2D rb;
private Vector2 moveDirection;

public float horizontal;
public float vertical;

public float sphereRadius = 3.0f; // Raio da esfera
private Vector3 sphereCenter; // Centro da esfera

[SerializeField] public ParticleSystem particulasRight;
[SerializeField] public ParticleSystem particulasLeft;
[SerializeField] public ParticleSystem particulasTop;
[SerializeField] public ParticleSystem particulasBottom;

GerenciadorDeCartas gerenciadorDeCartas;

public float atributoDoJogador = 0;
public string[] atributos;
public string nomeDoAtributoAdd;
void Start()
{
    sphereCenter = transform.position; // Defina o centro da esfera como a posição inicial do jogador
    gerenciadorDeCartas = FindObjectOfType<GerenciadorDeCartas>();

}

private void Awake()
{
    rb = GetComponent<Rigidbody2D>();
}

private void Update()
{
    if(!gerenciadorDeCartas.movimentoTravado) {
        moveplayer();
                // Atualize a posição da esfera para seguir o mouse ao longo da circunferência da esfera
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = mousePosition - sphereCenter;
            offset = offset.normalized * sphereRadius;
            sphereCenter = transform.position + offset;
            particulasRight.gameObject.SetActive(true);
            particulasTop.gameObject.SetActive(true);
            particulasBottom.gameObject.SetActive(true);
            particulasLeft.gameObject.SetActive(true);
    }
    else{
        particulasRight.gameObject.SetActive(false);
        particulasTop.gameObject.SetActive(false);
        particulasBottom.gameObject.SetActive(false);
        particulasLeft.gameObject.SetActive(false);
    }
    // Debug.Log(atributoDoJogador);
}

public void moveplayer()
{
    horizontal = Input.GetAxisRaw("Horizontal");
    vertical = Input.GetAxisRaw("Vertical");
    // Normaliza o vetor de direção para manter a velocidade constante
    if (horizontal != 0 && vertical != 0)
    {
        float magnitude = Mathf.Sqrt(2f) / 2f; // Valor para normalizar diagonais
        moveDirection = new Vector2(horizontal * magnitude, vertical * magnitude);

    }
    else
    {
        moveDirection = new Vector2(horizontal, vertical);
    }
    



}

private void FixedUpdate()
{
    Vector3 movePosition = (speed * Time.fixedDeltaTime * moveDirection.normalized) + rb.position;
    rb.MovePosition(movePosition);

    if(horizontal > 0){
        particulasRight.Play();
    } else {
        particulasRight.Stop();
    }

    if(horizontal < 0){
        particulasLeft.Play();
    } else {
        particulasLeft.Stop();
    }

    if(vertical > 0){
        particulasTop.Play();
    } else {
        particulasTop.Stop();
    }

    if(vertical < 0){
        particulasBottom.Play();
    } else {
        particulasBottom.Stop();
    }
}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(sphereCenter, sphereRadius);

        // Calcule a posição do mouse na circunferência da esfera
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDirection = mousePosition - sphereCenter;
        mouseDirection = mouseDirection.normalized * sphereRadius;
        Vector3 mouseOnSphere = sphereCenter + mouseDirection;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(mouseOnSphere, 0.1f);
    }

    public void addAtributos(string nomeDoAtributo, float valorDoAtributo)
    {
        // Verifica qual atributo foi selecionado e adiciona o valor correspondente
        if (nomeDoAtributo == atributos[0])  // Exemplo: 'Força'
        {
            atributoDoJogador += valorDoAtributo;
            Debug.Log($"Atributo {nomeDoAtributo} aumentado em {valorDoAtributo}. Novo valor: {atributoDoJogador}");
        }
        else if (nomeDoAtributo == atributos[1])  // Exemplo: 'Velocidade'
        {
            // Adiciona o valor ao atributo de velocidade
            // A lógica seria similar para outros atributos.
            // Implemente conforme seus atributos específicos
        }
        else if (nomeDoAtributo == atributos[2])  // Exemplo: 'Inteligência'
        {
            // Lógica para aumentar o atributo de Inteligência
        }
        else
        {
            Debug.LogWarning($"Atributo {nomeDoAtributo} não reconhecido!");
        }

        // Exemplo de como você pode aplicar um efeito baseado nos atributos (opcional)
        if (atributoDoJogador > 50)
        {
            // Alguma mudança, como alterar a velocidade do jogador ou outras mecânicas
            speed += 0.5f; // Aumenta a velocidade do jogador se o atributo de força for muito alto
            Debug.Log("Velocidade aumentada!");
        }
    }



}