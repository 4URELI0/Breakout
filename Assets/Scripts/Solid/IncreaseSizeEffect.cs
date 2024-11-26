using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

