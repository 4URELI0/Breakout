using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Paddle jugador;

    private ICommandable comandoIzquierda;
    private ICommandable comandoDerecha;
    private ICommandable comandoClic;

    public void Start()
    {
        comandoIzquierda = new ComandoIzquierda(jugador);
        comandoDerecha = new ComandoDerecha(jugador);
        comandoClic = new ComandoClic(jugador);
    }

    public void Update()
    {
        //Chequeando Movimiento
        if (Input.GetKey(KeyCode.A) && transform.position.x < jugador.xLimit)//Verifica si se presiono la tecla D para mover el paddle hacia la derecha
        {
            comandoIzquierda.EjecutarComando();
        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x > -jugador.xLimit)//Verifica si se presiono la tecla A para mover el paddle hacia la izquierda y los limite del rango del paddle
        {
            comandoDerecha.EjecutarComando();
        }

        //Chequeando clic
        if (Input.GetMouseButtonDown(0))
        {
            comandoClic.EjecutarComando();
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
        jugador.MovIzquierda();
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
        jugador.MovDerecha();
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