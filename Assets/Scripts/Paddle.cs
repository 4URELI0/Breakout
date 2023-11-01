using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float speed = 9f;
    GameManager gameManager;//Obtener referencia al GameManager
    [SerializeField] float xLimit = 7.28f;//Limites que el paddle puede manejar
    [SerializeField] float xLimitWhenBig = 6f;
    [SerializeField] byte superBallTime = 10;
    

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        MovimientoJugador();       
    }
    void MovimientoJugador()
    {
        //Va a verificar si el usuario presiono una tecla con el if
        if (Input.GetKey(KeyCode.D) && transform.position.x < xLimit)//Verifica si se presiono la tecla D para mover el paddle hacia la derecha
        {
            transform.position += speed * Time.deltaTime * Vector3.right;
        }
        else if (Input.GetKey(KeyCode.A) && transform.position.x > -xLimit)//Verifica si se presiono la tecla A para mover el paddle hacia la izquierda y los limite del rango del paddle
        {
            transform.position += speed * Time.deltaTime * Vector3.left;
        }
        if (Input.GetMouseButtonDown(0))//Usamos GetMouseBottonDown para leer el input que necesitamos, el 0 indica que usamos el botón izquierdo del mouse ya que es un arreglo
        {
            /*La razon por cual revisamos si el valor de la propiedad es falso es por que cada una tiene una condición diferente */
            if (gameManager.BallOnPlay == false)//Pasara a false cada vez que la bola se detenga y regresara verdadero cuando sea lanzada de nuevo
            {
                gameManager.BallOnPlay = true; //A)Lee el input del usuario y le dice a GameManager en que momento lanzar la bola 
            }
            if (gameManager.GameStarted == false)//Se ejecutara una vez al inicio del juego
            {
                gameManager.GameStarted = true;  
            }
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            //Obtenemos el componente PowerUp pero con el objeto de colisión por la información que este contiene
            //Obtenemos su variable powerUpType
            if (collision.GetComponent<PowerUp>().powerUpType == PowerUp.PowerUpType.IncreaseSize)
            {
                gameManager.bigSize = true;
                StartCoroutine(BigSizePower());
            }
            else if(collision.GetComponent<PowerUp>().powerUpType == PowerUp.PowerUpType.SuperBall)
            {
                gameManager.superBall = true;
                StartCoroutine(StopSuperBall());
            }
            Destroy(collision.gameObject);   
        }
    }*/
    /*IEnumerator BigSizePower()//Esta corrutina se encargara de incrementar el tamaño del paddle por 5 segundos y después el paddle volverá a la normalidad 
    {
        float originalXLimit = xLimit;//Guardar el limite original 
        xLimit = xLimitWhenBig;//Indicamos que el nuevo limite sera el valor que tenemos en xLimitWhenBig
        Vector3 newSize = transform.localScale;//Almacena el tamaño actual del paddle
        //Incrementar tamaño
        while (transform.localScale.x < 1.5f)//Tenemos un while que toma la x de newSize y lo aumenta un poco en cada frame
        {
            newSize.x += Time.deltaTime;
            transform.localScale = newSize;//El valor actualizado se le asigna a la escala del paddle
        }
        yield return new WaitForSeconds(10);
        //Reducir tamaño 
        while (transform.localScale.x > 1)
        {
            newSize.x -= Time.deltaTime;
            transform.localScale = newSize;
        }
        gameManager.bigSize = false;//Le asignamos un false para decir que el Paddle ya no posee el powerUp
        xLimit = originalXLimit;//Al regresar el paddle al tamaño original también regresamos al limite original
    }
    IEnumerator StopSuperBall()
    {
        yield return new WaitForSeconds(superBallTime);
        gameManager.superBall = false;
    }*/
}
