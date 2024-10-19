using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    private string titlePhrase = "BREAKOUT";
    public Text textBr;
    void Start()
    {
        StartCoroutine(TituloAnim());
    }
    IEnumerator TituloAnim()
    {
        foreach(char caracter in titlePhrase)
        {
            textBr.text = textBr.text + caracter;
            yield return new WaitForSeconds(0.5f);
        }
    }
}