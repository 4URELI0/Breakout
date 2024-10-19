using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Es una función que facilita el acceso a clases y funciones relacionada con la gestion de escena
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject winnerPanel;
    [SerializeField] GameObject[] livesImg;
    [SerializeField] Text gameTimeText;
    [SerializeField] AudioClip buttonPress;
    /*Métodos que usaremos para llamar desde GameManager*/
    public void ActivateLosePanel()//Misma función que ActivateWinnerPanel pero de derrota
    {
        losePanel.SetActive(true);  
    }
    public void ActivateWinnerPanel(float gameTime)//Su cargo sera mostrar la pantalla de winner que por defecto esta desactivada pero si cumple la condición la pone en true
    {
        winnerPanel.SetActive(true);
        gameTimeText.text = "GameTime:" + gameTime + Math.Floor(gameTime) + "s";//transformara el numero que nos daba en float a entero 
    }
    public void ResetCurrentScene()//Se encargar de reiniciar la escena del juego
    {
        SceneManager.LoadScene("Game");//Accedemos a la clase SceneManager para ejecutar el metodo LoadScene y nos pide como parámetro el nombre de la escena que deseamos cargar
        Debug.Log("Reiniciar el juego");
        FindObjectOfType<AudioController>().PlaySfx(buttonPress);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Menu");
    }
    public void UpdateUILives(byte currentLives)
    {
        for (int i = 0; i < livesImg.Length; i++)
        {
            if (i >= currentLives)//Usamos una sentencia if para preguntar si nuestra cantidad actual de vida es mayor o igual a nuestro índice
            {
                livesImg[i].SetActive(false);//Si es verdadera ocultamos el elemento 
            }

        }
    }
}