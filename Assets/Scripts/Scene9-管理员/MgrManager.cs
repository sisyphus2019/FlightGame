using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class MgrManager : MonoBehaviour
{
    private SQLiteHelper sql;
    public InputField selectTableName;
    public InputField deletePlayerName;
    string _selectTableName;
    string _deletePlayerName;
    public Text showText;

    public bool GetSelectTableName()
    {
        string temp = selectTableName.text;
        if(temp == "")
        {
            Debug.Log("表名不能为空");
            return false;
        }
        _selectTableName = temp;
        return true;
    }
    public bool GetDeletePlayerName()
    {
        string temp = deletePlayerName.text;
        if(temp == "")
        {
            Debug.Log("删除用户名不能为空");
            return false;
        }
        _deletePlayerName = temp;
        return true;
    }
    public void OnGetPlayerIn_foButton()
    {
        selectTableName.text = "playerInformation";
    }
    public void OnGetPlane_Button()
    {
        selectTableName.text = "Plane_";
    }
    public void OnGetLevel_Button()
    {
        selectTableName.text = "Level_";
    }
    public void OnGetDiary_Button()
    {
        selectTableName.text = "Diary_";
    }
    public void OnSelectButton()
    {
        if(GetSelectTableName())
        {
            //连接数据库
            sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
            //显示区域信息
            string showString;
            showString = "";
            //显示表头
            string queryString = "Pragma table_info("+_selectTableName+")";
            SqliteDataReader  reader = sql.ExecuteQuery(queryString);
            int headers = 0;
            while(reader.Read())
            {
                string header = reader.GetString(reader.GetOrdinal("name"));
                headers++;
                showString += header + "\t\t\t";
            }
            showString += "\n"+"--------------------------------------------------\n";
            //showText.text = showString;
            //显示表信息 希望能更加对齐
            queryString = "select * from "+_selectTableName;
            reader = sql.ExecuteQuery(queryString);
            while(reader.Read())
            {
                for(int i=0;i<headers;i++)
                {
                    string value = reader[i].ToString();
                    showString += value + "\t\t\t";
                }
                showString +="\n";
            }
            showText.text = showString;
            //关闭数据库连接
            sql.CloseConnection();
        }
    }
    public void OnDeleteButton()
    {
        if(GetDeletePlayerName())
        {
            sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
            int playerID;
            string queryString = "select playerID from playerinformation where playername = "+"'"+_deletePlayerName+"'";
            SqliteDataReader reader = sql.ExecuteQuery(queryString);
            reader.Read();
            playerID = reader.GetInt32(reader.GetOrdinal("playerID"));
            queryString = "delete from playerinformation where playerID = "+playerID;
            sql.ExecuteQuery(queryString);
            sql.CloseConnection();

            sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
            queryString = "drop table level_"+playerID;
            sql.ExecuteQuery(queryString);
            sql.CloseConnection();

            sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
            queryString = "drop table plane_"+playerID;
            sql.ExecuteQuery(queryString);
            sql.CloseConnection();

            sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
            queryString = "drop table diary_"+playerID;
            sql.ExecuteQuery(queryString);
            sql.CloseConnection();
        }
    }
    public void OnShowAllTablesButton()
    {
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        string showString = "";
        string queryString = "select name from sqlite_master where type='table' order by name ";
        SqliteDataReader reader = sql.ExecuteQuery(queryString);
        while(reader.Read())
        {
            string table = reader.GetString(reader.GetOrdinal("name"));
            showString += table+"\n";
        }
        showText.text = showString;
        sql.CloseConnection();
    }
    public void OnExitButton()
    {
        SceneManager.LoadScene("Scene0-登录");
    }
}
