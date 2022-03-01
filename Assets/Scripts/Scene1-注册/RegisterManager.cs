using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class RegisterManager : MonoBehaviour
{
    public InputField newName;
    public InputField newFirstPassword;
    public InputField newSecondPassword;
    public string playerName;
    public string firstPassword;
    public string secondPassword;
    public int playerID;
    SQLiteHelper sql;
    string tableName = "playerinformation";
    public GameObject hintWindow;
    void GetMessage()
    {
        //获取文本框内信息
        playerName = newName.text;
        firstPassword = newFirstPassword.text;
        secondPassword = newSecondPassword.text;
    }
    public void OnBackButton()
    {
        SceneManager.LoadScene("Scene0-登录");
    }
    public void OnRegisterButton()
    {
        GetMessage();
        if(playerName == "")
        {
            hintWindow.transform.GetComponent<HintWindow>().showHint.text = "用户名不能为空";
            hintWindow.SetActive(true);
            return;
        }
        if(firstPassword == "")
        {
            hintWindow.transform.GetComponent<HintWindow>().showHint.text = "密码不能为空";
            hintWindow.SetActive(true);
            return;
        }
        //判断用户名是否已存在 若已存在相同则返回提示 不同则进行下一步
        if(NameIsOnly())
        {
            //判断输入两次密码是否一致 若不一致则返回提示 若一致则添加新用户
            if(firstPassword == secondPassword)
            {
                InsertNewPlayerInformation();
            }
            else
            {
                hintWindow.transform.GetComponent<HintWindow>().showHint.text = "两次密码不一致";
                hintWindow.SetActive(true);
                return;
            }
        }

        //为新用户创建自己的物品表用于存储解锁信息
        CreatePlayerTables();

        SceneManager.LoadScene("Scene0-登录");
    }
    public bool NameIsOnly()
    {
        //连接数据库
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        //判断数据库中是否存在相同名称
        string queryString = "select " + "playername "+ "from "+ tableName;
        SqliteDataReader reader = sql.ExecuteQuery(queryString);
        while(reader.Read())
        {
            string queryResult = reader.GetString(reader.GetOrdinal("playername"));
            if(playerName == queryResult)
            {
                //关闭数据库连接
                sql.CloseConnection();
                hintWindow.transform.GetComponent<HintWindow>().showHint.text = "用户名已被占用";
                hintWindow.SetActive(true);
                return false;
            }
        }
        //关闭数据库连接
        sql.CloseConnection();
        return true;
    }
    public void InsertNewPlayerInformation()
    {
        //连接数据库
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        //insertinto
        string queryString = "insert into " + tableName + " (playerid,playername,playerpassword,playercoins) "
        + "values (" + "null" + ",'"+playerName+"','"+firstPassword+"',"+30+")";
        Debug.Log(queryString);
        sql.ExecuteQuery(queryString);
        //hintWindow.transform.GetComponent<HintWindow>().showHint.text = "注册成功";
        //hintWindow.SetActive(true);
        Debug.Log(playerName+"注册成功");
        //关闭数据库连接
        sql.CloseConnection();
    }
    void GetPlayerID()
    {
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        //获取playerID
        string queryString = "select " + "playerID " + "from " + tableName + " where " + "playername " + "= " + "'" + playerName + "'";
        SqliteDataReader reader = sql.ExecuteQuery(queryString);
        reader.Read();
        playerID = reader.GetInt32(reader.GetOrdinal("playerID"));

        sql.CloseConnection();
    }
    void CreatePlayerTables()
    {
        GetPlayerID();
        CreatePlayerPlaneTable();
        CreatePlayerLevelTable();
        CreatePlayerDiaryTable();
        Debug.Log("玩家物品表生成完毕");
    }
    void CreatePlayerPlaneTable()
    {
        //Debug.Log("玩家解锁飞机表生成中...");
        //连接数据库
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        string planeTableName = "Plane_"+playerID;
        //创建表
        sql.CreateTable(planeTableName,new string[]{"planeID","planeName"},new string[]{"int","text"});
        //添加默认飞机
        sql.InsertValues(planeTableName,new string[]{"1","smallPlaneRed"});
        sql.InsertValues(planeTableName,new string[]{"2","smallPlaneGrass"});
        sql.InsertValues(planeTableName,new string[]{"4","bigPlaneBlue"});
        //关闭连接
        sql.CloseConnection();
    }
    void CreatePlayerLevelTable()
    {
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        string levelTableName = "Level_"+playerID;
        sql.CreateTable(levelTableName,new string[]{"levelID","levelName"},new string[]{"int","text"});
        //添加默认模式
        sql.InsertValues(levelTableName,new string[]{"1","fly"});
        sql.CloseConnection();
    }
    void CreatePlayerDiaryTable()
    {
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        string diaryTableName = "diary_"+playerID;
        string queryString = "create table "+diaryTableName+" (diaryID integer primary key,diaryContent text,diaryTime text)";
        sql.ExecuteQuery(queryString);
        sql.CloseConnection();
    }
}
