using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    
    public enum PowerUpType
    {
     IncreaseSize//Aumenta el tamaño del paddle
    }
    public PowerUpType powerUpType;//Actuara como selector del tipo de power-up desde la ventana inspector

    private void Start()
    {
        Destroy(gameObject,10);
    }
    private void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.down;//Agregamos un movimiento hacia abajo 
    }
}
