using FishNet.Object;
using FishNet.Object.Prediction;
using UnityEngine;

public class BallMultiplayer : NetworkBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float speed = 5f;

    private Vector2 currentVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        if (IsServerInitialized)
        {
            LaunchBall(); // Solo el servidor lanza la bola inicialmente
        }
    }

    private void FixedUpdate()
    {
        if (IsServerInitialized)
        {
            // Actualiza la velocidad y posición de la bola
            currentVelocity = rb.velocity;
            RpcSyncBallState(transform.position, rb.velocity);

            // Revisa si la bola está fuera de los límites y reiníciala
            if (transform.position.y < -10f)
            {
                ResetBall();
            }
        }
    }

    [ObserversRpc]
    private void RpcSyncBallState(Vector3 position, Vector2 velocity)
    {
        // Actualiza la posición y velocidad de la pelota en los clientes
        if (!IsServerInitialized)
        {
            transform.position = position;
            rb.velocity = velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsServerInitialized)
        {
            if (collision.transform.CompareTag("Player"))
            {
                // Rebota la pelota en el paddle del jugador, ajustando el ángulo según la posición de la colisión
                float xDifference = transform.position.x - collision.transform.position.x;
                Vector2 direction = new Vector2(xDifference, 1).normalized;
                rb.velocity = direction * speed;
            }
            else if (collision.transform.CompareTag("Brick"))
            {
                // Lógica de destrucción de ladrillos
                var brick = collision.gameObject.GetComponent<BrickMultiplayer>();
                if (brick != null)
                {
                    brick.DestroyBrick();
                }

                // Rebota en el ladrillo
                Vector2 reflectDir = Vector2.Reflect(currentVelocity, collision.GetContact(0).normal);
                rb.velocity = reflectDir;
            }
            else if (collision.transform.CompareTag("LimiteMuerte"))
            {
                ResetBall();
            }

            else
            {
                // Rebota en cualquier otra superficie (bordes del mapa)
                Vector2 reflectDir = Vector2.Reflect(currentVelocity, collision.GetContact(0).normal);
                rb.velocity = reflectDir;
            }
        }
    }

    private void LaunchBall()
    {
        // Lanza la pelota en una dirección inicial en el servidor
        rb.velocity = new Vector2(initialSpeed, initialSpeed);
        transform.SetParent(null);
    }

    private void ResetBall()
    {
        // Resetea la pelota al paddle cuando sale del mapa
        rb.velocity = Vector2.zero;
        Transform paddle = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.SetParent(paddle);
        Vector2 ballPosition = paddle.position;
        ballPosition.y += 0.3f;
        transform.position = ballPosition;

        // Lanza la bola nuevamente después de posicionarla
        LaunchBall();
    }
}
