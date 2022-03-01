using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreUIManager : MonoBehaviour
{
    public void OnPlaneButton()
    {
        SceneManager.LoadScene("UnlockPlane");
    }
    public void OnLevelButton()
    {
        SceneManager.LoadScene("UnlockLevel");
    }
    public void OnStartButton()
    {
        SceneManager.LoadScene("Scene2-开始");
    }
}
