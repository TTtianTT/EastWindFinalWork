using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APoint
{
    // 是否是障碍物
    public bool isObstacle { get; set; }
    // 位置
    public Vector3 postion { get; set; }
    // 格子坐标
    public int x { get; set; }
    public int y { get; set; }

    // 与起点的长度
    public int gCost { set; get; }
    // 与目标点的长度
    public int hCost { get; set; }

    // 总的路径长度
    public int fCost
    {
        get { return gCost + hCost; }
    }
    // 父节点
    public APoint parent { get; set; }

    public APoint(bool isObstacle, Vector3 postion, int x, int y)
    {
        this.isObstacle = isObstacle;
        this.postion = postion;
        this.x = x;
        this.y = y;
    }
}
