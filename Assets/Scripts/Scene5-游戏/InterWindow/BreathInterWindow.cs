using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathInterWindow : MonoBehaviour
{
    public int coin = 4;
    public string hint = "您进行了一次深呼吸...";
    public string GetQuestion()
    {
        string question = hint;
        return question;
    }
    public string GetAnswer()
    {
        return "";
    }
    public void OnButton()
    {
        string question = GetQuestion();

        //制作航行日记
        GameManager.gameManager.levelManager.diaryText += GetQuestion()+"\n";

        GameManager.gameManager.levelManager.coinsSUM += coin;
        //Debug.Log("Coins:"+GameManager.gameManager.levelManager.coinsSUM);

        GameManager.gameManager.levelManager.plane.currentCube.CloseInterWindow();
        GameManager.gameManager.levelManager.NextTurn();
    }
}
