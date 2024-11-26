using FishNet.Object; // Biblioteca para objetos en red con FishNet.
using UnityEngine;

/// <summary>
/// Lógica para la destrucción de ladrillos en un juego multijugador.
/// </summary>
public class BrickMultiplayer : NetworkBehaviour
{
    /// <summary>
    /// Método llamado desde el servidor para destruir el ladrillo.
    /// RequireOwnership = false permite que cualquier cliente pueda llamar este ServerRpc.
    /// </summary>
    [ServerRpc(RequireOwnership = false)]
    public void DestroyBrick()
    {
        RpcDestroyBrick(); // Llama al ObserversRpc para informar a todos los clientes.
        Destroy(gameObject); // Destruye el ladrillo en el servidor.
    }

    /// <summary>
    /// Método ejecutado en todos los clientes para sincronizar la destrucción.
    /// </summary>
    [ObserversRpc]
    private void RpcDestroyBrick()
    {
        Destroy(gameObject); // Destruye el ladrillo localmente en cada cliente.
    }
}
