using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    //GameObject gameManagerObj;
    GameManager gameManager;
    //[SerializeField] GameObject[] powerUpPrefabs;
    

    private void Start()
    {
        
        gameManager = FindObjectOfType<GameManager>();//Es util por solo existirá un componente de este tipo en todo el juego
        if (gameManager != null)
        {
            gameManager.BricksOnLevel++;//Nos aseguramos que este usando la propiedad
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameManager != null)//Comprobar que exista, en caso que si restara a BricksOnLevel
        {
            gameManager.BricksOnLevel--;//comentario linea 30
        }
        /*if (gameManager.bigSize == false && gameManager.superBall == false)
        {


         //Numero aleatorio entre el 0 - 99
         int numeroRandom = Random.Range(0, 100);
         if (numeroRandom < 50)//Hay un 50% que de posibilidad que aparezca un powerUp
         {
                //Crear un power Up
                int randomPowerUp = Random.Range(0, powerUpPrefabs.Length);//Le pasamos la longitud del arreglo como numero mas grande posible 
                Instantiate(powerUpPrefabs[randomPowerUp], transform.position, Quaternion.identity);
         }
        }*/
        Destroy(gameObject);
        

    }
}
