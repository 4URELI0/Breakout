using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    /*Se encargar de destruir la animacion*/
    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
