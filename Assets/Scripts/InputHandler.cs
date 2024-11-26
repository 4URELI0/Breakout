using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Paddle jugador;

    private ICommandable comandoMovimiento;

    public void Update()
    {
        //Chequeando Movimiento
        if (Input.GetKey(KeyCode.A))//Verifica si se presiono la tecla D para mover el paddle hacia la derecha
        {
            comandoMovimiento = new ComandoIzquierda(jugador);
            comandoMovimiento.EjecutarComando();
        }
        else if (Input.GetKey(KeyCode.D))//Verifica si se presiono la tecla A para mover el paddle hacia la izquierda y los limite del rango del paddle
        {
            comandoMovimiento = new ComandoDerecha(jugador);
            comandoMovimiento.EjecutarComando();
        }
       
        if (Input.GetMouseButtonDown(0))
        {
            comandoMovimiento = new ComandoClic(jugador);
            comandoMovimiento.EjecutarComando();
        }
    }
}

//Creando la interfaz ICommand
public interface ICommandable
{
    public void EjecutarComando();
}

//Creando las clases que implementen la interfaz
public class ComandoIzquierda : ICommandable
{
    private Paddle jugador;
    //Constructor
    public ComandoIzquierda(Paddle jugador)
    {
        this.jugador = jugador;
    }

    public void EjecutarComando()
    {
        //Chequeando Movimiento
        if (jugador.transform.position.x < jugador.xLimit)
        {
            jugador.transform.position += jugador.speed * Time.deltaTime * Vector3.left;
        }
    }
}

public class ComandoDerecha : ICommandable
{
    private Paddle jugador;
    //Constructor
    public ComandoDerecha(Paddle jugador)
    {
        this.jugador = jugador;
    }

    public void EjecutarComando()
    {
        if (jugador.transform.position.x < jugador.xLimit)
        {
            jugador.transform.position += jugador.speed * Time.deltaTime * Vector3.right;
        }
    }
}

public class ComandoClic : ICommandable
{
    private Paddle jugador;

    //Constructor
    public ComandoClic(Paddle jugador)
    {
        this.jugador = jugador;
    }

    public void EjecutarComando()
    {
        jugador.LanzarPelota();
    }
}