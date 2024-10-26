using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    public enum PowerUpType
    {
        IncreaseSize,//Aumenta el tamaño del paddle
        IncreaseSpeed//Aumenta la velocidad del paddle
    }
    public PowerUpType powerUpType;//Actuara como selector del tipo de power-up desde la ventana inspector
    private void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.down;//Agregamos un movimiento hacia abajo para los power ups
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LimiteMuerte"))
        {
            PowerUpPool.Instance.AddToPool(gameObject);
        }
    }
}