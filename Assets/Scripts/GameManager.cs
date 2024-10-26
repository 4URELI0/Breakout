using UnityEngine;

/*Game Manager nos ayudara a tener el control del estado nuestro juego, cuantos bloques quedan para saber el momento de la victoria,
 * cuando la bola rebote debajo del jugador va a indicar que perdió una vida y si esta llega a 0 saltara la pantalla Game over*/
public class GameManager : MonoBehaviour
{
    [SerializeField] float gameTime;

    public bool bigSize;//Movemos el bigSize al GameManager para tener registro sobre el y lo mismo con bigSpeed
    public bool bigSpeed;


    [SerializeField] byte bricksOnLevel;//Va a llevar registros sobre los bloques, eliminamos public ya que no afectara a la variable desde otro script   


    public byte BricksOnLevel
    {
        get => bricksOnLevel;
        //Vamos a abrir un nuevo scope para lograr algo mas interesante
        //Usamos la propiedad y revisamos si la cantidad de bloque es igual a 0, si es verdadero ejecuta la victoria
        set
        {
            bricksOnLevel = value;
            if (bricksOnLevel == 0)
            {
                Debug.Log("MISSION PASSED!");
                Destroy(GameObject.Find("Ball"));//Va a buscar el objeto "Ball" que es nuestra pelota y la va a destruir si es que se cumple la sentencia de que no quede ningún bloque                                
                //MOSTRAR PANTALLA DE VICTORIA
                gameTime = Time.time * gameTime;
                FindObjectOfType<UIController>().ActivateWinnerPanel(gameTime);
                //TODO MEDIR TIEMPO DE JUEGO
                gameTime = Time.time - gameTime;//Calculamos el tiempo transcurrido con una resta del tiempo actual del juego - gameTime, que corresponde al punto que lanzamos la bola por primera vez
            }
        }
    }

    [SerializeField] byte playerLives = 3;//Vidas del jugador
    public byte PlayerLives
    {
        get => playerLives;
        set
        {//Propiedad que afecte la vida del jugador y detectar cuando este se queda sin vida
            playerLives = value;
            if (playerLives == 0)
            {
                Debug.Log("MISSION FAILED");
                Destroy(GameObject.Find("Ball"));//Destrucción de la bola
                //MOSTRAR PANTALLA DERROTA
                FindObjectOfType<UIController>().ActivateLosePanel();
            }
            else
            {
                FindAnyObjectByType<Ball>().ResetBall();//Si el jugador perdió una vida pero no llego a 0 va a reiniciar a la bola
            }
            FindObjectOfType<UIController>().UpdateUILives(playerLives);
        }
    }

    [SerializeField] bool gameStarted;//Para indicar que el juego se inicio
    public bool GameStarted
    {
        get => gameStarted;
        set
        {
            gameStarted = value;
            gameTime = Time.time;//Nos ayudara para medir el tiempo 
        }
    }

    [SerializeField] bool ballOnPlay;//Para saber cuando la bola esta en el juego
    public bool BallOnPlay
    {
        get => ballOnPlay;
        set
        {
            ballOnPlay = value;
            if (ballOnPlay == true)
            {
                Debug.Log("Lanzar la bola");
                FindObjectOfType<Ball>().LaunchBall();//Buscamos al componente de la bola y ejecutamos la metodo LaunchBall
            }//B) Luego GameManager le dice al script ball que ejecute el metodo que hace el lanzamiento de la bola
        }
    }
}