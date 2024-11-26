using FishNet.Object; // Importa la funcionalidad para objetos en red de FishNet.
using UnityEngine;

/// <summary>
/// Controla la lógica de la pelota en un juego multijugador usando FishNet.
/// </summary>
public class BallMultiplayer : NetworkBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Referencia al Rigidbody2D de la pelota.
    [SerializeField] private float initialSpeed = 5f; // Velocidad inicial al lanzar la pelota.
    [SerializeField] private float speed = 5f; // Velocidad base de la pelota.

    private Vector2 currentVelocity; // Almacena la velocidad actual de la pelota.

    private void Awake()
    {
        // Obtiene la referencia del Rigidbody2D en la inicialización.
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        if (IsServerInitialized)
        {
            LaunchBall(); // Solo el servidor lanza la pelota inicialmente.
        }
    }

    private void FixedUpdate()
    {
        if (IsServerInitialized)
        {
            // Actualiza la velocidad y posición de la pelota.
            currentVelocity = rb.velocity;

            // Sincroniza estado de la pelota con los clientes.
            RpcSyncBallState(transform.position, rb.velocity);

            // Verifica si la pelota sale de los límites del mapa.
            if (transform.position.y < -10f)
            {
                ResetBall(); // Reinicia la posición de la pelota.
            }
        }
    }

    /// <summary>
    /// Sincroniza la posición y velocidad de la pelota en todos los clientes.
    /// </summary>
    [ObserversRpc]
    private void RpcSyncBallState(Vector3 position, Vector2 velocity)
    {
        if (!IsServerInitialized)
        {
            transform.position = position; // Actualiza posición.
            rb.velocity = velocity; // Actualiza velocidad.
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsServerInitialized)
        {
            if (collision.transform.CompareTag("Player"))
            {
                // Rebote especial al chocar con el paddle del jugador.
                float xDifference = transform.position.x - collision.transform.position.x;
                Vector2 direction = new Vector2(xDifference, 1).normalized;
                rb.velocity = direction * speed;
            }
            else if (collision.transform.CompareTag("Brick"))
            {
                // Lógica para destruir ladrillos al colisionar.
                collision.gameObject.GetComponent<BrickMultiplayer>()?.DestroyBrick();

                // Rebota en el ladrillo.
                Vector2 reflectDir = Vector2.Reflect(currentVelocity, collision.GetContact(0).normal);
                rb.velocity = reflectDir;
            }
            else if (collision.transform.CompareTag("LimiteMuerte"))
            {
                ResetBall(); // Reinicia la pelota si toca el límite de muerte.
            }
            else
            {
                // Rebota en bordes o superficies genéricas.
                Vector2 reflectDir = Vector2.Reflect(currentVelocity, collision.GetContact(0).normal);
                rb.velocity = reflectDir;
            }
        }
    }

    /// <summary>
    /// Lanza la pelota desde una posición inicial con una velocidad predefinida.
    /// </summary>
    private void LaunchBall()
    {
        rb.velocity = new Vector2(initialSpeed, initialSpeed); // Velocidad inicial.
        transform.SetParent(null); // Desvincula la pelota del paddle.
    }

    /// <summary>
    /// Resetea la posición de la pelota al paddle del jugador.
    /// </summary>
    private void ResetBall()
    {
        rb.velocity = Vector2.zero; // Detiene la pelota temporalmente.
        Transform paddle = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra el paddle.
        transform.SetParent(paddle); // Vincula la pelota al paddle.
        transform.position = paddle.position + Vector3.up * 0.3f; // Ajusta posición.

        LaunchBall(); // Lanza la pelota nuevamente.
    }
}
