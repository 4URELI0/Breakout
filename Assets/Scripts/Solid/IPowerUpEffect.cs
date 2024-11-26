using UnityEngine;

public interface IPowerUpEffect
{
    void Apply(Paddle paddle);
    void Remove(Paddle paddle); // Para deshacer el efecto si es necesario.
}


public class IncreaseSizeEffect : IPowerUpEffect
{
    private float originalXLimit;

    public void Apply(Paddle paddle)
    {
        originalXLimit = paddle.xLimit;
        paddle.xLimit = 6f; // Ejemplo: nuevo límite
        paddle.transform.localScale = new Vector3(5.5f, paddle.transform.localScale.y, paddle.transform.localScale.z);
    }

    public void Remove(Paddle paddle)
    {
        paddle.xLimit = originalXLimit;
        paddle.transform.localScale = new Vector3(3f, paddle.transform.localScale.y, paddle.transform.localScale.z);
    }
}

public class IncreaseSpeedEffect : IPowerUpEffect
{
    private float originalSpeed;

    public void Apply(Paddle paddle)
    {
        originalSpeed = paddle.speed;
        paddle.speed = 30f; // Incremento en la velocidad
    }

    public void Remove(Paddle paddle)
    {
        paddle.speed = originalSpeed;
    }
}
