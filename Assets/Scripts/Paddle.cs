using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] public float speed = 9f;
    GameManager gameManager;//Obtener referencia al GameManager
    [SerializeField] public float xLimit = 7.29f;//Limites que el paddle puede manejar
    [SerializeField] public float xLimitWhenBig = 6f;
    [SerializeField] public float speedPU = 50.5f;
    [SerializeField] public byte timeBigSize = 10;//Para modificar el tiempo en la ventana inspector la velocidad del powerUp
    [SerializeField] public byte timeBigSpeed = 10;//Para modificar el tiempo en la ventana inspector la velocidad del power ups

    private List<IPowerUpEffect> activeEffects = new List<IPowerUpEffect>();


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

    void Update()
    {
    }

    public void MovIzquierda()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;
    }
    public void MovDerecha()
    {
        transform.position += speed * Time.deltaTime * Vector3.right;
    }

    public void LanzarPelota()
    {
        /* La razon por cual revisamos si el valor de la propiedad es falso es por que cada una tiene una condición diferente */
        if (gameManager.BallOnPlay == false)//Pasara a false cada vez que la bola se detenga y regresara verdadero cuando sea lanzada de nuevo
        {
            gameManager.BallOnPlay = true; //A)Lee el input del usuario y le dice a GameManager en que momento lanzar la bola 
        }
        if (gameManager.GameStarted == false)//Se ejecutara una vez al inicio del juego
        {
            gameManager.GameStarted = true;
        }
    }

    public void ApplyEffect(IPowerUpEffect effect)
    {
        effect.Apply(this);
        activeEffects.Add(effect);

        // Opcional: Eliminar el efecto después de cierto tiempo
        StartCoroutine(RemoveEffectAfterTime(effect, 10f));
    }

    private IEnumerator RemoveEffectAfterTime(IPowerUpEffect effect, float duration)
    {
        yield return new WaitForSeconds(duration);
        effect.Remove(this);
        activeEffects.Remove(effect);
    }

}