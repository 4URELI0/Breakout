using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource efectosSource;
    [SerializeField] private AudioSource musicaSource;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        } 
        else
        {
            instance = this;
            Debug.Log("Instancia de AudioManager iniciada");
            DontDestroyOnLoad(this);
        }
    }

    public void ReproducirMusica(AudioClip musica)
    {
        musicaSource.Stop(); //Detiene cualquier música previa
        musicaSource.PlayOneShot(musica);
    }


    public void ReproducirSonido(AudioClip sonido)
    {
        efectosSource.PlayOneShot(sonido);
    }
}
