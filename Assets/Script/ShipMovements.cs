using UnityEngine;

public class ShipMovements : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f; // Velocidad de movimiento
    private Rigidbody2D rb; // Rigidbody2D para mover el objeto

    bool moveUp, moveDown, moveLeft, moveRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D del objeto
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
        rb.MovePosition(rb.position + move.normalized * moveSpeed * Time.fixedDeltaTime); // Normalizar el vector para no mover más rápido en diagonal
    }

    void InputWSAD()
    {
        moveUp = Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.D);
    }
}

