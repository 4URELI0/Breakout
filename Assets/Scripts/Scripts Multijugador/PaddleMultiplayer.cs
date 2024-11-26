using FishNet.Object; // Biblioteca para objetos en red con FishNet.
using UnityEngine;

/// <summary>
/// L�gica para controlar el paddle usando Fish-Net.
/// </summary>
public class PaddleMultiplayer : NetworkBehaviour
{
    public float speed = 10f; // Velocidad de movimiento del paddle.

    private void Update()
    {
        // Solo el propietario del paddle puede controlarlo.
        if (!IsOwner)
            return;
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        // Movimiento horizontal del paddle basado en la entrada del jugador.
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(move, 0, 0);

        // Limita el paddle al �rea de juego
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -10f, 10f);
        transform.position = pos;

        // Env�a la posici�n del paddle al servidor para sincronizarla.
        SendPaddlePositionToServer(transform.position);
    }

    /// <summary>
    /// Envia la posici�n actual del paddle al servidor.
    /// </summary>
    [ServerRpc]
    private void SendPaddlePositionToServer(Vector3 position)
    {
        RpcUpdatePaddlePosition(position); // Sincroniza la posici�n con los clientes.
    }

    /// <summary>
    /// Sincroniza la posici�n del paddle en todos los clientes.
    /// </summary>
    [ObserversRpc]
    private void RpcUpdatePaddlePosition(Vector3 position)
    {
        transform.position = position; // Actualiza la posici�n en los clientes.
    }
}
