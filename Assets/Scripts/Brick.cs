using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Brick : MonoBehaviour
{
    //GameObject gameManagerObj;
    GameManager gameManager;
    [SerializeField] GameObject explosionPrefab;//Para la animacion de la explosion
    [SerializeField] GameObject[] powerUpPrefabs;//Guardar los power up en el script
    
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
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);//Crear la animacion de la explosion con cada bloque destruido
        if (gameManager != null)//Comprobar que exista, en caso que si restara a BricksOnLevel
        {
            gameManager.BricksOnLevel--;//comentario linea 30
        }
        if (gameManager.bigSize == false && gameManager.bigSpeed == false) 
        {
            //Numeros aleatorio
            int numeroRandom = Random.Range(0, 100);
            if (numeroRandom < 40)//Es como tener un 40% de probabilidad que aparesca un power up
            {
                /*Crear un powerUp al destruir un bloque*/
                int powerUpRandom = Random.Range(0, powerUpPrefabs.Length);
                Instantiate(powerUpPrefabs[powerUpRandom], transform.position, Quaternion.identity);

            }
        }
        Destroy(gameObject);
    }
}