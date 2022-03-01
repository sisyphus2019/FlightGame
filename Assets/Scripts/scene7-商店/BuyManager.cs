using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    public int cost;
    public int coins;
    public int ID;
    public int Type;
    public GameObject hintWindow;
    public void OnYesButton()
    {
        GetCoins();
        if(costCoins(cost))
        {
            UpdateCoins();
        }
        this.gameObject.SetActive(false);
        return;

    }
    public void OnNoButton()
    {
        this.gameObject.SetActive(false);
    }
    public void GetCoins()
    {
        coins = GameManager.gameManager.player.coins;
    }
    public bool costCoins(int cost)
    {
        if(coins >= cost)
        {
            coins = coins - cost;
            GameManager.gameManager.playerTablesNeedUpdate = true;
            GameManager.gameManager.insertType = Type;
            GameManager.gameManager.insertID = ID;
            hintWindow.transform.GetComponent<HintWindow>().showHint.text = "解锁成功";
            hintWindow.SetActive(true);
            return true;
        }
        else
        {
            hintWindow.transform.GetComponent<HintWindow>().showHint.text = "金币不足";
            hintWindow.SetActive(true);
            return false;
        }
    }
    public void UpdateCoins()
    {
        GameManager.gameManager.player.coins = coins;
        GameManager.gameManager.CoinsUpdateTODB();
    }
}
