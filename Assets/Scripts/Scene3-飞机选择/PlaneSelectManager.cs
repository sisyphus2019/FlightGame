using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneSelectManager : MonoBehaviour
{
    public GameObject bigPlane_Blue;
    public GameObject bigPlane_Gold;
    public GameObject bigPlane_Silver;
    public GameObject smallPlane_Red;
    public GameObject smallPlane_Grass;
    public GameObject smallPlane_Green;
    public GameObject helicopter_Blue;
    public GameObject helicopter_Red;
    public GameObject helicopter_White;
    //表示当前选择的飞机皮肤
    public GameObject selectedPlane;
    public int testNumber = 2;
    private void Start() {
    }
    public void OnBP_BlueSelected(bool isOn)
    {
        if(isOn)
        {
            selectedPlane = bigPlane_Blue;
        }
    }
    public void OnBP_GoldSelected(bool isOn)
    {
        if(isOn)
        {
            selectedPlane = bigPlane_Gold;
        }

    }
    public void OnBP_SilverSelected(bool isOn)
    {
        if(isOn)
        {
            selectedPlane = bigPlane_Silver;
        }
    }
    public void OnSP_RedSelected(bool isOn)
    {
        if(isOn)
        {
            selectedPlane = smallPlane_Red;
        }
    }
    public void OnSP_GrassSelected(bool isOn)
    {
        if(isOn)
        {
            selectedPlane = smallPlane_Grass;
        }
    }
    public void OnSP_GreenSelected(bool isOn)
    {
        if(isOn)
        {
            selectedPlane = smallPlane_Green;
        }
    }
    public void OnHelicopter_BlueSelected(bool isOn)
    {
        if(isOn)
        {
            selectedPlane = helicopter_Blue;
        }
    }
    public void OnHelicopter_RedSelected(bool isOn)
    {
        if(isOn)
        {
            selectedPlane = helicopter_Red;
        }
    }
    public void OnHelicopter_WhiteSelected(bool isOn)
    {
        if(isOn)
        {
            selectedPlane = helicopter_White;
        }
    }
    public void OnStartButton()
    {
        // GameManager.gameManager.selectedPlane = selectedPlane;
        // GameManager.gameManager.testNumber = testNumber;
        GameManager.gameManager.GetSelectedPlane(selectedPlane);
        //确认按钮 返回开始界面Scene2
        SceneManager.LoadScene("Scene2-开始");
    }
    public void OnBackButton()
    {
        SceneManager.LoadScene("Scene2-开始");
    }
}
