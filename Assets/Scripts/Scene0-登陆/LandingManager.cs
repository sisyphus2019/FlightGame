using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class LandingManager : MonoBehaviour
{
    public static LandingManager landingManager;
    public InputField inputName;
    public InputField inputPassword;
    //存储注册用户信息表名
    string playerTableName = "playerInformation";
    string managerTableName = "managerInformation";
    public string userName;
    string userPassword;
    public int playerID;
    SQLiteHelper sql;
    public GameObject hintWindow;
    void Start()
    {
        landingManager = this;
    }
    void GetMessage()
    {
        //文本框内容传入name/password
        userName = inputName.text;
        userPassword = inputPassword.text;
    }
    public void OnLoginButton()
    {
        GetMessage();
        if(ManagerIsExit())
        {
            if(ManagerPasswordIsRight())
            {
                //管理员名字密码正确 进入管理员界面
                Debug.Log(userName+"管理员登陆成功");
                SceneManager.LoadScene("Scene9-管理员");
            }
            else
            {
                //管理员密码不正确
                hintWindow.transform.GetComponent<HintWindow>().showHint.text = "管理员密码不正确";
                hintWindow.SetActive(true);
                return;
            }
        }
        //如果不是管理员 判断是否为用户
        else
        {
            if(PlayerIsExit())
            {
                //判断用户密码是否正确
                if(PlayerPasswordIsRight())
                {
                    Debug.Log(userName+"玩家登陆成功");
                    SceneManager.LoadScene("Scene2-开始");
                }
                else
                {
                    hintWindow.transform.GetComponent<HintWindow>().showHint.text = "用户密码不正确";
                    hintWindow.SetActive(true);
                    return;
                }
            }
            else
            {
                //用户不存在
                hintWindow.transform.GetComponent<HintWindow>().showHint.text = "用户不存在，请注册";
                hintWindow.SetActive(true);
                return;
            }
        }
    }
    public void OnRegisterButton()
    {
        //跳转到注册界面
        SceneManager.LoadScene("Scene1-注册");
    }
    //判断用户是否存在
    public bool PlayerIsExit()
    {
        //连接数据库
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        //判断数据库中是否已有注册用户
        string queryString = "select " + "playername "+ "from "+ playerTableName;
        SqliteDataReader reader = sql.ExecuteQuery(queryString);
        while(reader.Read())
        {
            string queryResult = reader.GetString(reader.GetOrdinal("playername"));
            if(userName == queryResult)
            {
                //关闭数据库连接
                sql.CloseConnection();
                //Debug.Log("用户存在");
                return true;
            }
        }
        //关闭数据库连接
        sql.CloseConnection();
        //Debug.Log("用户不存在，请注册");
        return false;
    }
    //判断用户密码是否正确
    public bool PlayerPasswordIsRight()
    {
        //连接数据库
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        //判断用户名密码对应是否正确
        //查找输入用户名对应密码
        string queryString = "select " + "playerpassword " + "from " + playerTableName + " where " + "playername " + "= " + "'" + userName + "'";
        SqliteDataReader reader = sql.ExecuteQuery(queryString);
        //获取结果
        reader.Read();
        string queryResult = reader.GetString(reader.GetOrdinal("playerpassword"));
        //判断是否相同
        if(queryResult == userPassword)
        {
            //相同 获取playerID
            queryString = "select " + "playerID " + "from " + playerTableName + " where " + "playername " + "= " + "'" + userName + "'";
            reader = sql.ExecuteQuery(queryString);
            reader.Read();
            playerID = reader.GetInt32(reader.GetOrdinal("playerID"));
            sql.CloseConnection();
            //Debug.Log(playerName+"密码正确");
            return true;
        }
        //若不符合则显示相应提示
        else
        {
            //Debug.Log("密码不正确");
            sql.CloseConnection();
            return false;
        }
        //return false;
    }
    //判断是否为管理员
    public bool ManagerIsExit()
    {
        //连接数据库
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");

        string queryString = "select managername from "+managerTableName;
        SqliteDataReader reader = sql.ExecuteQuery(queryString);
        while(reader.Read())
        {
            string queryResult = reader.GetString(reader.GetOrdinal("managername"));
            //此处文本框接受为存的变量名为userName 此处意为managerName
            if(userName == queryResult)
            {
                sql.CloseConnection();
                return true;
            }
        }
        sql.CloseConnection();
        return false;
    }
    public bool ManagerPasswordIsRight()
    {
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        string queryString = "select managerpassword from "+managerTableName + " where managername = "+"'"+userName+"'";
        SqliteDataReader reader = sql.ExecuteQuery(queryString);
        //获取结果
        reader.Read();
        string queryResult = reader.GetString(reader.GetOrdinal("managerpassword"));
        //判断是否相同 此处playerPassword
        if(queryResult == userPassword)
        {
            sql.CloseConnection();
            //Debug.Log(userName+"管理员密码正确");
            return true;
        }
        //若不符合则显示相应提示
        else
        {
            //Debug.Log(userName+"管理员密码不正确");
            sql.CloseConnection();
            return false;
        }
    }
}
