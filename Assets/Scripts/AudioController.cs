using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource[] sfxChannel;

    private void Start()
    {
        StartCoroutine("CleanAudioChannel");
    }
    public void PlaySfx(AudioClip clip)
    {
        /*Agregamos un look for que recorre todo el arreglo de AudioSource*/
        for (int i = 0; i < sfxChannel.Length; i++)
        {
            if (sfxChannel[i].clip == null)//Busca un canal de audio que este libre para asignarle un clip, en caso de ser verdadero significa que el audio esta le asigna el clip y en play
            {
                sfxChannel[i].clip = clip;
                sfxChannel[i].Play();
                StartCoroutine(CleanAudioChannel(clip.length,i));//Antes de ejecutar el break, llamamos a la corrutina que limpia el canal, como parámetro tiene la duración del clip como el canal a limpiar como que es en el indice i
                break;//Le asignamos un break, por que una vez encontrado el canal de audio, no tiene sentido que siga buscando
            }
        }
    }//El problema es que nunca termina de limpiar los audio una vez ocupado y terminado, no lo elimina de su campo, ósea solo seremos capaz de reproducir 6 audios y después quedaran obsoletos 
    
    //Agregaremos un nuevo parámetro, mediante este parámetro recibirá el tiempo de espera para liberar el canal que sera equivalente a la duración del clip
    //La corrutina necesita saber cual es el proximo canal que tiene que liberar que es channel
    //Enviamos la longitud del clip y el índice del canal a liberar
    IEnumerator CleanAudioChannel(float length, int channel)
    {
        //Inmediato
        yield return new WaitForSeconds(length);
        sfxChannel[channel].clip = null;//Limpia el canal indicado al asignarle el valor nulo que contiene el clip del audio  
    }
}
