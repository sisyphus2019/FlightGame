using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUIManager : MonoBehaviour
{
    public List<Toggle> toggles;
    public List<int> unlockedLevels;
    //bool isUpdate = false;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUnlockedData();
        SendDataToScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameManager.playerTablesIsUpdate)
        {
            UpdateUnlockedData();
            SendDataToScene();
        }
    }
    void UpdateUnlockedData()
    {
        unlockedLevels = GameManager.gameManager.player.unlockedLevels;
    }
    void SendDataToScene()
    {
        for(int i=0;i<toggles.Count;i++)
        {
            toggles[i].interactable = false;
        }
        for(int j=0;j<unlockedLevels.Count;j++)
        {
            toggles[unlockedLevels[j]-1].interactable = true;
        }
    }
}
