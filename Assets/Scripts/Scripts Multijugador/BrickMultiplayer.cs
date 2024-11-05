using FishNet.Object;
using UnityEngine;

public class BrickMultiplayer : NetworkBehaviour
{
    [ServerRpc(RequireOwnership = false)]
    public void DestroyBrick()
    {
        RpcDestroyBrick();
        Destroy(gameObject);
    }

    [ObserversRpc]
    private void RpcDestroyBrick()
    {
        Destroy(gameObject);
    }
}
