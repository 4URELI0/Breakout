using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class MenuOpciones : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    public void changeVolume(float volumen)
    {
        audioMixer.SetFloat("Volumen", volumen);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}