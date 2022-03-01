using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public LevelManager levelManager;
    public float spaceTime;
    public float currentSpaceTime = 0.0f;
    public float diceTime;
    public float currentDiceTime = 0.0f;
    public bool isDice;
    public int diceNumber;
    void Start()
    {
    }

    void Update()
    {
        if(isDice)//如果可以掷骰子
        {
            //Debug.Log("Dice-isDicing...");
            currentDiceTime+=Time.deltaTime;//掷骰子时间开始计时
            currentSpaceTime+=Time.deltaTime;
            if(currentSpaceTime >= spaceTime)
            {
                currentSpaceTime=0;
                diceNumber = Random.Range(1,7);
                levelManager.showDice.text=diceNumber.ToString();
            }
            if(currentDiceTime >= diceTime)
            {
                //Debug.Log("Dice-dicingFinish");
                currentDiceTime=0;
                levelManager.ReceiveDiceNumber(diceNumber);
                diceNumber = 0;
                isDice=false;
            }
        }
    }
    public void OnDice()
    {
        int n = (int)Time.deltaTime;
        Random.InitState(n);

        isDice = true;
    }
}
