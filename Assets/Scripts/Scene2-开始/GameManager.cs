using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int coinsNumber;
    public Player player;//接受playerID
    public LevelManager levelManager;
    public GameObject selectedPlane;
    //游戏结束后 金币增 商店解锁 金币减
    //public bool coinIsUpdate = false;
    public bool playerTablesNeedUpdate = false;
    public int insertType;
    public int insertID;
    //商店解锁 数据库改变 玩家表更新
    public bool playerTablesIsUpdate = false;
    public string diaryText = "";
    public List<Diary> diaries;
    // Start is called before the first frame update
    void Start()
    {
        //gameManager = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(playerTablesNeedUpdate)
        {
            //Debug.Log("game-need");
            PlayerInformationTransfer.playerInformationTransfer.PlayerTablesInsert(insertType,insertID);
            playerTablesNeedUpdate = false;
            playerTablesIsUpdate = true;
            //playerTablesIsUpdate = false;
        }
    }
    private void Awake() {
        //此脚本永不销毁 且每次进入初始场景时进行判断 若存在重复则销毁
        if(gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this);
        }
        else if(this != gameManager)
        {
            //Debug.Log("Reason?");
            Destroy(gameObject);
        }
    }
    public static GameManager instance{
        get
        {
            if(gameManager == null)
            {
                gameManager = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(gameManager.gameObject);
            }
            return gameManager;
        }
    }
    //gamemanager -> playerinformationtransfer
    public void CoinsUpdateTODB()
    {
        PlayerInformationTransfer.playerInformationTransfer.CoinsUpdateTODB(player.coins);
    }
    public void DiaryInsertTODB()
    {
        PlayerInformationTransfer.playerInformationTransfer.DiaryInsertTODB(diaryText);
    }
    //playerinfromationtransfer -> gamemanager
    public void UpdatePlayerData(Player player)
    {
        //获取PlayerInformationTansfer中的player信息并存储在player数据结构中
        this.player = player;
    }
    //planeselectManager -> gamemanager
    public void GetSelectedPlane(GameObject selectedPlane)
    {
        this.selectedPlane = selectedPlane;
    }
    //levelselectManager -> gameManager scenemanager
    public void GetSelectedLevel()
    {

    }
    //playerinformationtransfer -> gamemanager
    public void AddDiariesData(Diary diary)
    {
        diaries.Add(diary);
    }
}
