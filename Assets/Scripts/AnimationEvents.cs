using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    /*Se encargar de destruir la animaci�n al chocar con un bloque*/
    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}