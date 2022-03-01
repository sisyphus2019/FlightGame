using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneSelectUIManager : MonoBehaviour
{
    public List<Toggle> toggles;
    public List<int> unlockedPlanes;
    // Start is called before the first frame update
    void Start()
    {
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
    void UpdateUnlockedData()
    {
        //获取Gamemanager里的player里的list unlockedplanes
        unlockedPlanes = GameManager.gameManager.player.unlockedPlanes;
    }
    void SendDataToScene()
    {
        //控制Scene中的toggle能否interactable
        //先让所有的都不可
        for(int i=0;i<toggles.Count;i++)
        {
            toggles[i].interactable = false;
        }
        //再让部分可
        for(int j=0;j<unlockedPlanes.Count;j++)
        {
            toggles[unlockedPlanes[j]-1].interactable = true;
        }
    }
}
