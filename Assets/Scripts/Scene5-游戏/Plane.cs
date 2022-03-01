using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public bool isMove;
    public Cube currentCube;
    public List<Cube> road;//前进路线
    public Coroutine IEmove;//协程

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void init()
    {
        isMove = false;
        currentCube = GameObject.FindGameObjectWithTag("startCube").GetComponent<Cube>();
    }
    public void MovePlane(int diceNumber)//飞机根据骰子点数进行移动
    {
        //Debug.Log("飞机前进");
        GetRoad(diceNumber);
        StartMovePlane();//飞机开始移动
    }

    public void GetRoad(int diceNumber)//飞机前进线路计算
    {
        //Debug.Log("GetRoad");
        if(currentCube == null)
        {
            Debug.Log("当前所在方块不存在！");
            return;
        }
        road.Clear();//前进道路原有数据清空
        Cube startCube = currentCube.nextCube;
        for(int i=0;i<diceNumber;i++)
        {
            if(startCube == null)
            {
                break;
            }
            //前进路线
            road.Add(startCube);
            if(startCube.cubeType == 2)//到达终点
            {
                break;//跳出for循环 road计算结束
            }
            startCube = startCube.nextCube;
        }//前进路线添加完毕
    }
    //飞机开始移动
    public void StartMovePlane()
    {
        //Debug.Log("Plane-StartMovePlane");
        IEnumerator ie = MovingPlane();
        IEmove =StartCoroutine(ie);
    }

    //移动飞机协程
    IEnumerator MovingPlane()
    {
        //Debug.Log("飞机开始移动");
        if(currentCube != null)
        {
            currentCube.planeLeft();
            currentCube = null;
        }
        for(int i=0;i<road.Count;i++)
        {
            float distance = 100f;
            while(distance>0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(road[i].transform.position.x, road[i].transform.position.y + 0.5f, road[i].transform.position.z), GameManager.gameManager.levelManager.moveSpeed * Time.deltaTime);
                distance = Vector3.Distance(transform.position, new Vector3(road[i].transform.position.x, road[i].transform.position.y + 0.5f, road[i].transform.position.z));
                yield return null;//每一帧停顿一次
            }

            currentCube = road[i];
            transform.position = new Vector3(road[i].transform.position.x, road[i].transform.position.y + 0.5f, road[i].transform.position.z);
        }
        FinishMovePlane();

        //处理事件函数
        //AlternativeEvents(currentCube.interType);
    }
    //结束移动函数
    public void FinishMovePlane()
    {
        //Debug.Log("Plane-FinishMovePlane");
        if(currentCube.cubeType==2)
        {
            GameManager.gameManager.levelManager.GameFinish();
        }
        isMove=false;
        IEmove =null;
        currentCube.PlaneStay();
    }
}
