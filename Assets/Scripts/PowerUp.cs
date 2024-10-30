using System.Linq.Expressions;
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
        if (gameObject.activeInHierarchy)
        {
            transform.position += speed * Time.deltaTime * Vector3.down;//Agregamos un movimiento hacia abajo para los power ups
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PowerUpPool.Instance.ReturnToPool(gameObject);
            Debug.Log("Colision trigger del script power up");
        }
    }
    /*El power up se desactivara si esta fuera de la camara*/
    private void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy)
        {
            PowerUpPool.Instance.ReturnToPool(gameObject);
        }
    }
}