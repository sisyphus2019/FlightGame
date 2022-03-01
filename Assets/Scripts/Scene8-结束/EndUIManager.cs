using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndUIManager : MonoBehaviour
{
    public Text diaryContent;
    public Text coinsNumber;
    private void Start() {
        coinsNumber.text = GameManager.gameManager.coinsNumber.ToString();
        diaryContent.text = GameManager.gameManager.diaryText;
    }
    public void OnStartButton()
    {
        //返回开始界面
        SceneManager.LoadScene("Scene2-开始");
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
