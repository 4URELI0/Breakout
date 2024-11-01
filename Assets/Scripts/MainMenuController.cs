using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("Se presiono el boton");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
}