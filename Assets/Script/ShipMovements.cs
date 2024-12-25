using System;
using UnityEngine;

public class ShipMovements : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f; // Velocidad de movimiento
    private Rigidbody2D rb; // Rigidbody2D para mover el objeto
    private Collider2D shipCollider;
    public Camera OrthographicCamera;

    bool moveUp, moveDown, moveLeft, moveRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D del objeto
        shipCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        InputWSAD(); // Detectar las entradas de las teclas
    }

    private void FixedUpdate()
    {
        MovementShip(); // Mover la nave en FixedUpdate para física
    }

    void MovementShip()
    {
        Vector2 move = Vector2.zero;

        if (moveUp) move.y += 1f;
        if (moveDown) move.y -= 1f;
        if (moveLeft) move.x -= 1f;
        if (moveRight) move.x += 1f;

        // Mover el Rigidbody2D
        if (InsideBox(rb.position, shipCollider, OrthographicCamera))
        {
            rb.MovePosition(rb.position + move.normalized * moveSpeed * Time.fixedDeltaTime); // Normalizar el vector para no mover más rápido en diagonal
           
        }
    }

    void InputWSAD()
    {
        moveUp = Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.D);
    }

    bool InsideBox(Vector2 position, Collider2D shipCollider, Camera OrthographicCamera)
    {
        Vector3 cameraPos = OrthographicCamera.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, OrthographicCamera.transform.position.z));

        float shipOffsetX = shipCollider.bounds.size.x / 2;
        float shipOffsetY = shipCollider.bounds.size.y / 2;

        if (rb.transform.position.x + shipOffsetX > cameraPos.x)
        {
            rb.MovePosition( new Vector2 (cameraPos.x - shipOffsetX, rb.transform.position.y));
            return false;
        }

        else if(rb.transform.position.x - shipOffsetX < -cameraPos.x)
        {
            rb.MovePosition(new Vector2(-cameraPos.x + shipOffsetX, rb.transform.position.y));
            return false;
        }
        
        if (rb.transform.position.y + shipOffsetY > cameraPos.y)
        {
            rb.MovePosition( new Vector2(rb.transform.position.x, cameraPos.y - shipOffsetY));
            return false;
        }

        else if(rb.transform.position.y - shipOffsetY < -cameraPos.y)
        {
            rb.MovePosition(new Vector2(rb.transform.position.x, -cameraPos.y + shipOffsetY));
            return false;
        }

            return true;
    }
}

