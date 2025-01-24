using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D defaultCursor; // O sprite do cursor padrão
    public Texture2D clickCursor; // O sprite do cursor quando ocorre um clique
    public Vector2 hotspot = Vector2.zero; // Ponto quente do cursor

    void Start()
    {
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Verifica se houve um clique do botão esquerdo do mouse
        {
            Cursor.SetCursor(clickCursor, hotspot, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0)) // Volta para o cursor padrão após o clique
        {
            Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
        }
    }
}
