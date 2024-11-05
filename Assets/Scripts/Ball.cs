using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody2D;
    Vector2 moveDirection;
    Vector2 currentVelocity;
    [SerializeField] float speed = 3f;
    GameManager gameManager;//Acceso al gameManager desde el script
    
    /*Sonidos*/
    [SerializeField] AudioClip paddleBounce;
    [SerializeField] AudioClip bounce;
    [SerializeField] AudioClip loseLife;
    
    void Start()
    {
        //Nombre de la variable declarado en la linea 7,
        //(=)<- se encargar de guardar el resultado de la variable,
        //<El tipo de componente que estamos buscando>,
        //Finalizamos con un () dado que estamos ejecutando un metodo
        rigidBody2D = GetComponent<Rigidbody2D>();
        //Movimiento de la pelota hacia arriba
        gameManager = FindObjectOfType<GameManager>();//Obtenemos el componente  y es único
    }
    void FixedUpdate()//Los tiempos delta son constantes
    {
        currentVelocity = rigidBody2D.velocity;   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("La bola colisiono con: " + collision.transform.name);//collision.transform.name <- Nos permite saber con que objeto colisiono la bola
        moveDirection = Vector2.Reflect(currentVelocity, collision.GetContact(0).normal);
        rigidBody2D.velocity = moveDirection;
        /*Colisión para el sistema de derrota del juego*/
        if (collision.transform.CompareTag("LimiteMuerte"))//Verifica si la bola colisiono con al limite de abajo o por si tag LimiteMuerte y si es verdadero salta el mensaje
        {
            Debug.Log("Colisión con el limite de abajo");
            FindObjectOfType<AudioController>().PlaySfx(loseLife);
            if (gameManager != null)//Lo utilizamos con if != null para verificar que exista un componente gameManager, es una seguridad
            {
             gameManager.PlayerLives--;
            }
        }
        if (collision.transform.CompareTag("Player"))
        {
            FindObjectOfType<AudioController>().PlaySfx(paddleBounce);
            Debug.Log("verificación de clip");
        }
        if (collision.transform.CompareTag("Brick"))
        {
            FindObjectOfType<AudioController>().PlaySfx(bounce);
            Debug.Log("Clip de brick");
        }
    }
    public void LaunchBall()
    {
     transform.SetParent(null);//Utilizamos este código para decir que la bola ya no es hijo del paddle en el momento de ser lanzada; Surgía el problema de que la bola seguía al paddle cuando es lanzada
     rigidBody2D.velocity = Vector2.up * speed;//No utilizamos el deltaTime por que, por que el motor de física tiene tiempos delta definido y constantes que no dependen de la velocidad de la computadora 
    }
    public void ResetBall()
    {
        rigidBody2D.velocity = Vector3.zero;//Vamos a eliminar la velocidad de la bola
        Transform paddle = GameObject.Find("Paddle").transform;
        transform.SetParent(paddle);//Le hacemos hijo del paddle nuevamente
        Vector2 ballPosition = paddle.position;//Guardaremos la posición del paddle 
        ballPosition.y += 0.3f;//Aumentarle un poco la posición de la bola
        transform.position = ballPosition;//Le asignamos la posición que hicimos en la ballPosition a la bola
        gameManager.BallOnPlay = false;//Le decimo al GameManager que la bola no esta en el juego para para que permita hacer otro lanzamiento
    }
}