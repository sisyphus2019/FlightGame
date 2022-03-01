using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int diceNumber;
    public int moveSpeed;//飞机移动速度
    //public AudioSource BGMAudioSource;
    public Dice dice;//每个场景一个骰子
    public Button diceButton;//每个场景只有一个骰子
    public Text showDice;//显示骰子数量
    public Text showCoins;//显示当前可获得金币数量
    public Slider showTripLength;
    public int tripLength = 0;
    public GameObject selectedPlanePrefab;//选择的飞行器模型
    public GameObject selectedPlane;//通过飞行器模型生成的飞机
    public Plane plane;//飞机上的脚本 用来控制飞机前进
    public int coinsSUM;//这次关卡总共可以获取的coins
    public string diaryText;//这次航行日记
    public CameraFollow cameraFollow;//相机跟随
    // Start is called before the first frame update
    void Start()
    {
        InitGameManagerPart();
        init();
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        showCoins.text = coinsSUM.ToString();
    }
    public void InitGameManagerPart()
    {
        //Debug.Log("checkGameManager");
        GameManager.gameManager.levelManager = this;
    }
    //初始化
    public void init()
    {
        showDice.text = "";
        //生成飞机
        MadePlane();
        //获取控制飞机的脚本
        plane = GameObject.FindGameObjectWithTag("plane").GetComponent<Plane>();
        //相机跟随
        cameraFollow.init();
    }
    //开始游戏
    public void GameStart()
    {
        Debug.Log("航线开始");
        //BGMAudioSource.loop = true;
        //BGMAudioSource.Play();
        NextTurn();
    }
    //生成飞机
    public void MadePlane()
    {
        selectedPlanePrefab = GameManager.gameManager.selectedPlane;
        selectedPlane = Instantiate(selectedPlanePrefab,new Vector3(0,0,0) , Quaternion.identity);
    }
    //管理游戏进度 事件处理
    public void NextTurn()
    {
        diceButton.gameObject.SetActive(true);//骰子显示
    }
    //骰子按钮点击事件
    public void OnDiceButton()
    {
        diceButton.gameObject.SetActive(false);
        dice.OnDice();
    }
    public void ReceiveDiceNumber(int n)
    {
        //接收骰子点数
        if(dice.diceNumber == 0)
        {
            Debug.LogWarning("骰子掷0");
            return;
        }
        diceNumber = n;
        tripLength += diceNumber;
        showTripLength.value = tripLength;

        plane.isMove = true;
        plane.MovePlane(diceNumber);
    }
    public void GameFinish()
    {
        GameManager.gameManager.coinsNumber = coinsSUM;
        GameManager.gameManager.player.coins += coinsSUM;
        GameManager.gameManager.CoinsUpdateTODB();

        GameManager.gameManager.diaryText = diaryText;
        GameManager.gameManager.DiaryInsertTODB();
        //跳转到结束界面
        SceneManager.LoadScene("Scene8-结束");
    }
    public void OnExitButton()
    {
        SceneManager.LoadScene("Scene2-开始");
    }
}
