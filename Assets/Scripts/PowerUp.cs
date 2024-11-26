using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private IPowerUpEffect _effect;

    // Método para asignar el efecto basado en el índice del prefab
    public void SetEffect(int index)
    {
        if (index == 0)
        {
            _effect = new IncreaseSpeedEffect();

        }
        else if (index == 1)
        {
            _effect = new IncreaseSizeEffect();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var paddle = collision.GetComponent<Paddle>();
            if (paddle != null && _effect != null)
            {
                paddle.ApplyEffect(_effect);
                PowerUpPool.Instance.ReturnToPool(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.position += Vector3.down * 5f * Time.deltaTime;
        if (transform.position.y < -10)
        {
            PowerUpPool.Instance.ReturnToPool(gameObject);
        }
    }
}
