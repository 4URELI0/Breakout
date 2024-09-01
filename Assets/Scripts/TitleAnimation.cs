using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    string fraseTitulo = "BREAKOUT";
    public Text texto;
    void Start()
    {
        StartCoroutine(TituloAnim());
    }
    IEnumerator TituloAnim()
    {
        foreach(char caracter in fraseTitulo)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
