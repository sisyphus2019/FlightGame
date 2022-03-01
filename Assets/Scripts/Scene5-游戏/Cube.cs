using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Cube nextCube;//下一个结点
    public bool isPlane;//方块上是否停留飞机
    public int cubeType = 0;//方块类型 0为正常方块 1为交互方块 2为终点方块
    public int interType = 0;//交互方块 0为问题方块 1为选择方块 2为品读方块 3为呼吸方块
    public string music;//存储播放音效名称
    public GameObject interWindow;
    private void Start() {
        init();
    }
    private void Update() {
        if(isPlane == true)
        {
            interWindow.SetActive(true);
        }
    }

    public void init()
    {
        isPlane = false;
        if(interWindow != null)
        {
            interWindow.SetActive(false);
        }
    }
    public void PlaneStay()
    {
        isPlane = true;
    }

    public void planeLeft()
    {
        isPlane = false;
    }

    public void CloseInterWindow()
    {
        //Debug.Log("Cube-CLW");
        isPlane = false;
        interWindow.SetActive(false);
    }
}
