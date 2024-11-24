using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioClip musicaMenu;
    [SerializeField] private AudioClip musicaJuego;
    [SerializeField] private AudioClip musicaOpciones;

    public void Start()
    {
        //Reproduciendo música
        AudioManager.instance.ReproducirMusica(musicaMenu);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("Se presiono el boton");
        AudioManager.instance.ReproducirMusica(musicaJuego);
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
        AudioManager.instance.ReproducirMusica(musicaOpciones);
    }
    public void StartGameMultiplayer()
    {
        SceneManager.LoadScene("GameMP");
    }
}