using FishNet.Object; // Biblioteca para objetos en red con FishNet.
using UnityEngine;

/// <summary>
/// L�gica para la destrucci�n de ladrillos en un juego multijugador.
/// </summary>
public class BrickMultiplayer : NetworkBehaviour
{
    /// <summary>
    /// M�todo llamado desde el servidor para destruir el ladrillo.
    /// RequireOwnership = false permite que cualquier cliente pueda llamar este ServerRpc.
    /// </summary>
    [ServerRpc(RequireOwnership = false)]
    public void DestroyBrick()
    {
        RpcDestroyBrick(); // Llama al ObserversRpc para informar a todos los clientes.
        Destroy(gameObject); // Destruye el ladrillo en el servidor.
    }

    /// <summary>
    /// M�todo ejecutado en todos los clientes para sincronizar la destrucci�n.
    /// </summary>
    [ObserversRpc]
    private void RpcDestroyBrick()
    {
        Destroy(gameObject); // Destruye el ladrillo localmente en cada cliente.
    }
}
