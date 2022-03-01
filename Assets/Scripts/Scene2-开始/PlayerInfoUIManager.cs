using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerInfoUIManager : MonoBehaviour
{
    public Text showCoins;
    public Text showName;
    private void Start() {
        showName.text = GameManager.gameManager.player.playerName;
    }
    private void Update() {
        updateCoinsUI();
    }
    public int GetCoins()
    {
        int coins = GameManager.gameManager.player.coins;
        return coins;
    }
    
    public void updateCoinsUI()
    {
        showCoins.text = GetCoins().ToString();
    }
}
