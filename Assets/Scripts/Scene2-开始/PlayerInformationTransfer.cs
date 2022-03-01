using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class PlayerInformationTransfer : MonoBehaviour
{
    SQLiteHelper sql;
    public static PlayerInformationTransfer playerInformationTransfer;
    public Player player;
    string tableName = "playerinformation";
    // Start is called before the first frame update
    void Start()
    {
        playerInformationTransfer = this;

        player.playerID = LandingManager.landingManager.playerID;
        player.playerName = LandingManager.landingManager.userName;

        UpdatePlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdatePlayerData()
    {
        //获取player物品信息（可能会变化的）
        player.coins = GetCoins();
        player.unlockedPlanes = GetUnlockedPlane();
        player.unlockedLevels = GetUnlockedLevel();
        
        GameManager.gameManager.UpdatePlayerData(player);
    }
    //获取数据库中coins数量
    public int GetCoins()
    {
        //连接数据库
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        string queryString = "select playercoins from "+tableName+ " where playerid = "+player.playerID;
        SqliteDataReader reader = sql.ExecuteQuery(queryString);
        reader.Read();
        int coins = reader.GetInt32(reader.GetOrdinal("playercoins"));
        //Debug.Log("coins"+coins);
        sql.CloseConnection();
        return coins;
    }
    //关卡结束后 update coins增加
    //商店中解锁新模式 update coins减少
    public void CoinsUpdateTODB(int n)
    {
        //获取gamemanager的player数据里的coins
        //连接数据库 更新player表里的coins
        //log更新成功
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        string queryString = "update "+tableName+" set playercoins = "+n+" where playername = "+"'"+player.playerName+"'";
        sql.ExecuteQuery(queryString);
        sql.CloseConnection();
    }
    //关卡结束后 向数据库中insert diary记录
    public void DiaryInsertTODB(string diaryText)
    {
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        string tableName = "diary_"+player.playerID;
        string nowTime = System.DateTime.Now.ToString();
        string queryString = "insert into "+tableName+" values(null,"+"'"+diaryText+"','"+nowTime+"')";
        sql.ExecuteQuery(queryString);
        sql.CloseConnection();
    }
    //商店中解锁新模式 insert 玩家表更新
    public void PlayerTablesInsert(int insertType,int insertID)
    {
        if(insertType == 0)
        //解锁新飞行器
        {
            string tableName = "Plane_"+player.playerID;
            int planeID = insertID;
            sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
            string planeName = "";
            if(planeID == 3)
            {
                planeName = "smallPlaneGreen";
            }
            if(planeID == 5)
            {
                planeName = "BigPlaneGold";
            }
            if(planeID == 6)
            {
                planeName = "BigPlaneSilver";
            }
            if(planeID == 7)
            {
                planeName = "HelicopterBlue";
            }
            if(planeID == 8)
            {
                planeName = "HelicopterRed";
            }
            if(planeID == 9)
            {
                planeName = "HelicopterWhite";
            }
            string queryString = "insert into "+tableName + " values ("+insertID+",'"+planeName+"')";
            //Debug.Log(queryString);
            sql.ExecuteQuery(queryString);
            sql.CloseConnection();
        }
        if(insertType == 1)
        //解锁新模式
        {
            string tableName = "Level_"+player.playerID;
            int levelID = insertID;
            sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
            string levelName = "";
            if(levelID == 2)
            {
                levelName = "Sunset";
            }
            if(levelID == 3)
            {
                levelName = "LongDrive";
            }
            string queryString = "insert into "+tableName + " values ("+insertID+",'"+levelName+"')";
            //Debug.Log(queryString);
            sql.ExecuteQuery(queryString);
            sql.CloseConnection();
        }
        UpdatePlayerData();
    }
    //获取玩家plane表里的已解锁planeid
    public List<int> GetUnlockedPlane()
    {
        List<int> planeID = new List<int>();
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        string queryString = "select planeID from Plane_"+player.playerID;
        SqliteDataReader reader = sql.ExecuteQuery(queryString);
        while(reader.Read())
        {
            planeID.Add(reader.GetInt32(reader.GetOrdinal("planeID")));
        }
        sql.CloseConnection();
        return planeID;
    }
    //获取玩家Level表里的已解锁levelid
    public List<int> GetUnlockedLevel()
    {
        List<int> levelID = new List<int>();
        sql = new SQLiteHelper("data source = /Users/liuruchen/Desktop/GameData/Flight.db");
        string queryString = "select levelID from Level_"+player.playerID;
        SqliteDataReader reader = sql.ExecuteQuery(queryString);
        while(reader.Read())
        {
            levelID.Add(reader.GetInt32(reader.GetOrdinal("levelID")));
        }
        sql.CloseConnection();
        return levelID;
    }

}
