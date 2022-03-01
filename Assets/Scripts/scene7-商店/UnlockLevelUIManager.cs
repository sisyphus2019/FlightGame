using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnlockLevelUIManager : MonoBehaviour
{
    public GameObject sunsetAffirmWindow;
    public GameObject longDriveAffirmWindow;
    public List<int> unlockedLevels;
    public List<Button> buttons;
    public List<GameObject> panels;

    private void Start() {
        for(int i=0;i<buttons.Count;i++)
        {
            buttons[i].interactable = true;
            panels[i].SetActive(false);
        }
        sunsetAffirmWindow.SetActive(false);
        longDriveAffirmWindow.SetActive(false);

        UpdateUnlockedData();
        SendDataToScene();
    }
    private void Update() {
        if(GameManager.gameManager.playerTablesIsUpdate)
        {
            UpdateUnlockedData();
            SendDataToScene();
        }
    }
    public void OnSunsetButton()
    {
        sunsetAffirmWindow.SetActive(true);
    }
    public void OnLongDriveButton()
    {
        longDriveAffirmWindow.SetActive(true);
    }
    public void OnBackButton()
    {
        SceneManager.LoadScene("Scene7-商店");
    }
    public void OnStartButton()
    {
        SceneManager.LoadScene("Scene2-开始");
    }
    void UpdateUnlockedData()
    {
        unlockedLevels = GameManager.gameManager.player.unlockedLevels;
    }
    void SendDataToScene()
    {
        for(int j=0;j<unlockedLevels.Count;j++)
        {
            buttons[unlockedLevels[j]-1].interactable = false;
            //显示已解锁
            panels[unlockedLevels[j]-1].SetActive(true);
        }
    }
}
