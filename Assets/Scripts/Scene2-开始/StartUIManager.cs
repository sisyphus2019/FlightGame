using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour
{
    public void OnPlaneButton()
    {
        SceneManager.LoadScene("Scene3-飞机选择");
    }
    public void OnLevelButton()
    {
        SceneManager.LoadScene("Scene4-模式选择");
    }
    public void OnDiaryButton()
    {
        SceneManager.LoadScene("Scene6-航行日记");
    }
    public void OnStoreButton()
    {
        SceneManager.LoadScene("Scene7-商店");
    }
    public void OnBackButton()
    {
        SceneManager.LoadScene("Scene0-登录");
    }
}
