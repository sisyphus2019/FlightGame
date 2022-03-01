using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSelect : MonoBehaviour
{
    public Text aText;
    public Text bText;
    public Text cText;
    public string selectedAnswer;
    private void Start() {
        selectedAnswer = aText.text;
    }
    public void OnASelected(bool isOn)
    {
        if(isOn)
        {
            //Debug.Log("okk");
            selectedAnswer = aText.text;
        }
    }
    public void OnBSelected(bool isOn)
    {
        if(isOn)
        {
            selectedAnswer = bText.text;
        }
    }public void OnCSelected(bool isOn)
    {
        if(isOn)
        {
            selectedAnswer = cText.text;
        }
    }
}
