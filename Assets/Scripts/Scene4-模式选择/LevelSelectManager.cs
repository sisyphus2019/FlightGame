using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    //public static LevelSelectManager levelSelectManager;
    public int[] unlockedLevelID;
    public AudioSource audioSource;
    public AudioClip fly;
    public AudioClip sunset;
    public AudioClip longdrive;
    public AudioClip selectedMusic;
    public string sceneName = "Fly";
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        //levelSelectManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void init()
    {
        //判断unlockedLevelID里的id 然后设置toggle是否interactable
    }
    public void OnFlySelected(bool isOn)
    {
        if(isOn)
        {
            selectedMusic = fly;
            audioSource.clip = selectedMusic;
            audioSource.Play();
            sceneName = "Fly";
        }
    }
    public void OnSunsetSelected(bool isOn)
    {
        if(isOn)
        {
            selectedMusic = sunset;
            audioSource.clip = selectedMusic;
            audioSource.Play();
            sceneName = "Sunset";
        }
    }
    public void OnLongDriveSelected(bool isOn)
    {
        if(isOn)
        {
            selectedMusic = longdrive;
            audioSource.clip = selectedMusic;
            audioSource.Play();
            sceneName = "LongDrive";
        }
    }
    public void OnButton()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OnBackButton()
    {
        SceneManager.LoadScene("Scene2-开始");
    }
}
