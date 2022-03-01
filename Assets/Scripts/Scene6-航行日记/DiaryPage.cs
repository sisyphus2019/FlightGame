using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryPage : MonoBehaviour
{
    public Text ID;
    public Text Content;
    public Text Time;
    public DiaryUIManager diaryUIManager;
    private void Awake()
    {
        diaryUIManager = GameObject.FindGameObjectWithTag("DiaryUIManager").GetComponent<DiaryUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     //diarypage 上一页按钮 下一页按钮
    public void OnNextPageButton()
    {
        diaryUIManager.pageNumber++;
        if(diaryUIManager.pageNumber < diaryUIManager.diaries.Count)
        {
            diaryUIManager.hintWindow.SetActive(false);
            this.gameObject.SetActive(false);
            diaryUIManager.diaries[diaryUIManager.pageNumber].gameObject.SetActive(true);
        }
        else
        {
            diaryUIManager.hintWindow.transform.GetComponent<HintWindow>().showHint.text = "日记已到最后一页啦～";
            diaryUIManager.hintWindow.SetActive(true);
        }
    }
    public void OnPreviousPageButton()
    {
        diaryUIManager.pageNumber--;
        if(diaryUIManager.pageNumber < 0)
        {
            diaryUIManager.hintWindow.transform.GetComponent<HintWindow>().showHint.text = "日记已到第一页啦～";
            diaryUIManager.hintWindow.SetActive(true);
        }
        else
        {
            diaryUIManager.hintWindow.SetActive(false);
            this.gameObject.SetActive(false);
            diaryUIManager.diaries[diaryUIManager.pageNumber].gameObject.SetActive(true);
        }
    }
    //退出日记按钮
    public void OnCloseDiaryButton()
    {
        diaryUIManager.pageNumber = 0;
        this.gameObject.SetActive(false);
    }
}
