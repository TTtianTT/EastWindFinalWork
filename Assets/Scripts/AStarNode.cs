using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode
{
    // 是否是障碍物
    public bool isObstacle { get; set; }
    // 位置
    public Vector3 postion
    {
        get
        {
            return new Vector3(4 * x, 3, 4 * y);
        }
    }
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
    public AStarNode parent { get; set; }

    public AStarNode(bool isObstacle, Vector3 postion, int x, int y)
    {
        this.isObstacle = isObstacle;
        this.x = x;
        this.y = y;
    }
}


