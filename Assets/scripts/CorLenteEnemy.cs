using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorLenteEnemy : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    // public ColorLifeEnemy ColorLifeEnemy;

    public ColorLifeEnemy[] colorlife;


    public Color corAleatoria;
    // public bullet[] bullet;


    void Start()
    {
        // ColorLifeEnemy = FindObjectOfType<ColorLifeEnemy>();
        // colorlife = FindObjectOfType<ColorLifeEnemy>();
        // bullet = FindObjectOfType<bullet>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        AtribuirCorAleatoria();

    }

    void AtribuirCorAleatoria()
    {
        Color corAleatoria = new Color(Random.value, Random.value, Random.value);
        spriteRenderer.color = corAleatoria;
        // ColorLifeEnemy.corAleatoria2 = corAleatoria;
        colorlife[0].corAleatoria2 = corAleatoria;
        // bullet[0].corAleatoria = corAleatoria;

        // foreach (var inimigo in bullet)
        // {
        //     inimigo.corAleatoria = corAleatoria;
        // }
    }
}

