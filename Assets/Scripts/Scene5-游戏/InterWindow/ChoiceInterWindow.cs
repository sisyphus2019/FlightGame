﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceInterWindow : MonoBehaviour
{
    public int coin = 2;
    public Text questionField;
    public ChoiceSelect choiceSelect;
    //public string answer;
    public string GetQuestion()
    {
        string question = questionField.text;
        return question;
    }
    public string GetAnswer()
    {
        return choiceSelect.selectedAnswer;
    }
    public void OnButton()
    {
        string question = GetQuestion();
        string answer = GetAnswer();
        //Debug.LogWarning("ChooseQuestion:"+question);
        //Debug.LogWarning("Answer:"+answer);

        //制作航行日记
        GameManager.gameManager.levelManager.diaryText += GetQuestion()+"\n\t"+GetAnswer()+"\n";

        GameManager.gameManager.levelManager.coinsSUM += coin;
        //Debug.Log("Coins:"+GameManager.gameManager.levelManager.coinsSUM);
        GameManager.gameManager.levelManager.plane.currentCube.CloseInterWindow();
        GameManager.gameManager.levelManager.NextTurn();
    }
}
