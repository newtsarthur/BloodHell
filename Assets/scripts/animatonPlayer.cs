using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class animatPlayer : MonoBehaviour
{
[SerializeField]
private Animator animator;
move move;

void Start() {
    move = FindObjectOfType<move>();
    // animator = FindObjectOfType<Animator>();

}
private void Awake()
{
    animator = GetComponent<Animator>();
}
void Update()
{
    moveSpriteplayer();
}

public void moveSpriteplayer()
{
    move.horizontal = Input.GetAxisRaw("Horizontal");
    move.vertical = Input.GetAxisRaw("Vertical");

    // Verifica a direção horizontal e ajusta a escala e o parâmetro do Animator
    if (move.horizontal > 0)
    {
        transform.localScale = new Vector3(1, 1, 1); // Virar para a direita
        animator.SetBool("IsFacingRight", true); // Define o parâmetro para verdadeiro
        // move.particulasRight.Play();
    }
    else if (move.horizontal < 0)
    {
        transform.localScale = new Vector3(-1, 1, 1); // Virar para a esquerda
        animator.SetBool("IsFacingRight", false); // Define o parâmetro para falso
        // move.particulasLeft.Play();

    }
}
}