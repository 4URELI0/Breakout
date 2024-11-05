using FishNet.Object;
using UnityEngine;

public class PaddleMultiplayer : NetworkBehaviour
{
    public float speed = 10f;

    private void Update()
    {
        // Solo el jugador local puede mover su paddle
        if (!IsOwner)
            return;

        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(move, 0, 0);

        // Limita el paddle al área de juego
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -10f, 10f); // Ajusta estos límites según tu área de juego
        transform.position = pos;
    }

    [ServerRpc]
    private void SendPaddlePositionToServer(Vector3 position)
    {
        RpcUpdatePaddlePosition(position);
    }

    [ObserversRpc]
    private void RpcUpdatePaddlePosition(Vector3 position)
    {
        transform.position = position;
    }
}
