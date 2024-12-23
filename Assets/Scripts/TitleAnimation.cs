using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TitleAnimation : MonoBehaviour
{
    private string titlePhrase = "BREAKOUT";
    public Text textBr;
    void Start()
    {
        StartCoroutine(TitleAnim());
    }
    IEnumerator TitleAnim()
    {
        foreach (char caracter in titlePhrase)
        {
            textBr.text = textBr.text + caracter;
            yield return new WaitForSeconds(0.5f);
        }
    }
}