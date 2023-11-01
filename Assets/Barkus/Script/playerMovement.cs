using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 5f; // velocidad de movimiento del jugador

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Inicialmente, el sprite mira a la izquierda
        // spriteRenderer.flipX = true;
    }

    void Update()
    {
        // Entrada del teclado
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical);
        transform.Translate(movement * speed * Time.deltaTime);

        // Cambia la orientación del sprite según el movimiento del teclado
        if (movement.x < 0)
        {
            spriteRenderer.flipX = false; // Mirando a la izquierda
        }
        else if (movement.x > 0)
        {
            spriteRenderer.flipX = true; // Mirando a la derecha
        }

        // Entrada táctil
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                // Convierte la posición del toque en el mundo en una posición 3D en el plano Z=0
                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(touch.position);
                clickPosition.z = 0f;

                // Mueve al jugador hacia la posición del toque
                StartCoroutine(MoveToClickPosition(clickPosition));
            }
        }
    }

    // Corrutina para mover suavemente al jugador hacia una posición
    IEnumerator MoveToClickPosition(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Cambia la orientación del sprite mientras se mueve
            if (targetPosition.x < transform.position.x)
            {
                spriteRenderer.flipX = false; // Mirando a la izquierda
            }
            else if (targetPosition.x > transform.position.x)
            {
                spriteRenderer.flipX = true; // Mirando a la derecha
            }

            yield return null;
        }
    }
}
