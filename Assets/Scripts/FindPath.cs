using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath : MonoBehaviour
{
    public int mapWidth = 15;//地图长
    public int mapLength = 15;//地图宽
    public APoint[,] grid;//将地图状态映射到这个数组

    // 过滤墙体所在的层
    public LayerMask WhatLayer;
    //节点半径
    private float NodeRadius = 0.5f;

    public GameObject NodeWall;
    public GameObject Node;

    private GameObject WallRange, PathRange;

    private List<GameObject> open;//open List
    private List<GameObject> close;
    private List<GameObject> pathObj = new List<GameObject>();//存放A*算法搜索出的路径

    //寻路相关
    void Awake()//初始化地图
    {
        grid = new APoint[mapWidth, mapLength];

        // 将墙的信息写入格子中
        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapLength; z++)
            {
                //Vector3 pos = new Vector3(x * 0.5f, y * 0.5f, -0.25f);
                Vector3 detectPosition = new Vector3(x*4,1.1f,z*4);//用来检测地图状态的向量
                // 通过节点中心发射圆形射线，检测当前位置是否可以行走
                bool isObstacle = Physics.CheckSphere(detectPosition, NodeRadius, WhatLayer);
                // 构建一个节点
                grid[x, z] = new APoint(isObstacle, detectPosition, x, z);
                // 如果是墙体，则画出不可行走的区域
                if (isObstacle)
                {
                    GameObject obj = GameObject.Instantiate(NodeWall, detectPosition, Quaternion.identity) as GameObject;
                    obj.transform.SetParent(WallRange.transform);
                }
            }


        }
    }
    // 根据坐标获得一个节点
    public APoint getPoint(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x) /4;//if一个position为(4,y,0)，他在grid当中即为grid[1,0]
        int y = Mathf.RoundToInt(position.y) /4;
        x = Mathf.Clamp(x, 0, mapWidth - 1);//限制向量长度
        y = Mathf.Clamp(y, 0, mapLength - 1);
        return grid[x, y];
    }
    // 取得周围的节点
    public List<APoint> getNeibourhood(APoint node)
    {
        List<APoint> list = new List<APoint>();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                // 如果是自己，则跳过
                if (i == 0 && j == 0)
                    continue;
                int x = node.x + i;
                int y = node.y + j;
                // 判断是否越界，如果没有，加到列表中
                if (x < mapWidth && x >= 0 && y < mapLength && y >= 0)
                    list.Add(grid[x, y]);
            }
        }
        return list;
    }

    // 更新路径
    public void updatePath(List<APoint> lines)
    {
        int curListSize = pathObj.Count;
        for (int i = 0, max = lines.Count; i < max; i++)
        {
            if (i < curListSize)
            {
                pathObj[i].transform.position = lines[i].postion;
                pathObj[i].SetActive(true);
            }
            else
            {
                GameObject obj = GameObject.Instantiate(Node, lines[i].postion, Quaternion.identity) as GameObject;
                obj.transform.SetParent(PathRange.transform);
                pathObj.Add(obj);
            }
        }
        for (int i = lines.Count; i < curListSize; i++)
        {
            pathObj[i].SetActive(false);
        }
    }

    // 获取两个节点之间的距离
    int getDistanceNodes(Grid.APoint a, Grid.APoint b)
    {
        int cntX = Mathf.Abs(a.x - b.x);
        int cntY = Mathf.Abs(a.y - b.y);
        // 判断到底是那个轴相差的距离更远
        if (cntX > cntY)
        {
            return 14 * cntY + 10 * (cntX - cntY);
        }
        else
        {
            return 14 * cntX + 10 * (cntY - cntX);
        }
    }
}
