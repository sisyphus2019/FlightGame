using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnjoyInterWindow : MonoBehaviour
{
    public int coin = 1;
    public Text hint;
    public string GetQuestion()
    {
        string question = " “ "+hint.text+" ” ";
        return question;
    }
    public string GetAnswer()
    {
        return "";
    }
    public void OnButton()
    {
        string question = GetQuestion();
        //string answer = GetAnswer();
        //Debug.LogWarning("Enjoy:"+question);

        //制作航行日记
        GameManager.gameManager.levelManager.diaryText += GetQuestion()+"\n";

        GameManager.gameManager.levelManager.coinsSUM += coin;
        //Debug.Log("Coins:"+GameManager.gameManager.levelManager.coinsSUM);
        GameManager.gameManager.levelManager.plane.currentCube.CloseInterWindow();
        GameManager.gameManager.levelManager.NextTurn();
    }
}
