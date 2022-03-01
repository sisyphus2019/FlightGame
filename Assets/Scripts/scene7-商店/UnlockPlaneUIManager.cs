using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnlockPlaneUIManager : MonoBehaviour
{
    public GameObject smallPlaneGreenAffirmWindow;
    public GameObject bigPlaneGoldAffirmWindow;
    public GameObject bigPlaneSilverAffirmWindow;

    public GameObject helicopterBlueAffirmWindow;
    public GameObject helicopterRedAffirmWindow;
    public GameObject helicopterWhiteAffirmWindow;

    public List<int> unlockedPlanes;
    public List<Button> buttons;
    public List<GameObject> panels;

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i<buttons.Count;i++)
        {
            buttons[i].interactable = true;
            panels[i].SetActive(false);
        }
        smallPlaneGreenAffirmWindow.SetActive(false);
        bigPlaneSilverAffirmWindow.SetActive(false);
        bigPlaneGoldAffirmWindow.SetActive(false);
        helicopterBlueAffirmWindow.SetActive(false);
        helicopterRedAffirmWindow.SetActive(false);
        helicopterWhiteAffirmWindow.SetActive(false);

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
    public void OnSmallPlaneGreenButton()
    {
        smallPlaneGreenAffirmWindow.SetActive(true);
    }
    public void OnBigPlaneGoldButton()
    {
        bigPlaneGoldAffirmWindow.SetActive(true);
    }
    public void OnBigPlaneSilverButton()
    {
        bigPlaneSilverAffirmWindow.SetActive(true);
    }
    public void OnHelicopterBlueButton()
    {
        helicopterBlueAffirmWindow.SetActive(true);
    }
    public void OnHelicopterRedButton()
    {
        helicopterRedAffirmWindow.SetActive(true);
    }
    public void OnHelicopterWhiteButton()
    {
        helicopterWhiteAffirmWindow.SetActive(true);
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
        unlockedPlanes = GameManager.gameManager.player.unlockedPlanes;
    }
    void SendDataToScene()
    {
        for(int j=0;j<unlockedPlanes.Count;j++)
        {
            buttons[unlockedPlanes[j]-1].interactable = false;
            panels[unlockedPlanes[j]-1].SetActive(true);
        }
    }
}
