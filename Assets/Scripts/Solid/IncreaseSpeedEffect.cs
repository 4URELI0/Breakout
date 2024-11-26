using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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