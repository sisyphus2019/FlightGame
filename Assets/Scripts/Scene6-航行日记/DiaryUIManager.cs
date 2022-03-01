using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DiaryUIManager : MonoBehaviour
{
    private SQLiteHelper sql;
    public GameObject hintWindow;
    public GameObject diaryPage;
    public List<Diary> diariesContent;
    public List<GameObject> diaries;
    //记录翻到哪一页了
    public int pageNumber=0;
    // Start is called before the first frame update
    void Start()
    {
        GetDiaries();
        MadeDiaryPages();
        WriteDiaryPages();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //初始化 连接数据库 将日记相关信息保存
    public void GetDiaries()
    {
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");

        string tableName = "Diary_"+GameManager.gameManager.player.playerID;
        string queryString = "select * from "+tableName;
        SqliteDataReader reader = sql.ExecuteQuery(queryString);
        while(reader.Read())
        {
            Diary tempDiary = new Diary();
            tempDiary.diaryID = reader.GetInt32(reader.GetOrdinal("diaryID"));
            tempDiary.diaryContent = reader.GetString(reader.GetOrdinal("diaryContent"));
            tempDiary.diaryTime = reader.GetString(reader.GetOrdinal("diaryTime"));
            diariesContent.Add(tempDiary);
        }
        sql.CloseConnection();
    }
    //初始化 根据日记数量生成日记弹窗
    public void MadeDiaryPages()
    {
        for(int i=0;i<diariesContent.Count;i++)
        {
            GameObject oneDiaryPage = Instantiate(diaryPage,new Vector3(0,0,0) , Quaternion.identity);
            oneDiaryPage.SetActive(false);
            diaries.Add(oneDiaryPage);
        }
    }
    //初始化 根据日记内容初始化所有的日记页面
    public void WriteDiaryPages()
    {
        for(int i=0;i<diaries.Count;i++)
        {
            //获取每一页的脚本
            DiaryPage page = diaries[i].GetComponent<DiaryPage>();
            page.ID.text = "航线日记"+diariesContent[i].diaryID;
            page.Content.text = diariesContent[i].diaryContent;
            page.Time.text = diariesContent[i].diaryTime;
        }
    }
    //打开日记按钮
    public void OnDiaryOpenButton()
    {
        this.gameObject.SetActive(false);
        if(diaries.Count>0)
        {
            diaries[0].SetActive(true);
        }
        else
        {
            hintWindow.transform.GetComponent<HintWindow>().showHint.text = "请进行一次航行后查看哦";
            hintWindow.SetActive(true);
        }
    }
    //返回按钮
    public void OnBackButton()
    {
        SceneManager.LoadScene("Scene2-开始");
    }
}
