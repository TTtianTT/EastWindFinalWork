using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager
{
    private static AStarManager instance;

    private AStarManager() { }
    public static AStarManager Instance
    {
        get
        {
            if (instance == null)
                instance = new AStarManager();
            return instance;
        }
    }
    public static AStarManager getInstance()
    {
        if (instance == null)
            instance = new AStarManager();
        return instance;
    }
    private int mapW;
    private int mapH;

    public AStarNode[,] nodes;
    private List<AStarNode> openList = new List<AStarNode>();
    private List<AStarNode> closeList = new List<AStarNode>();

    public void InitMapInfo(int w, int h)
    {
        this.mapW = w;
        this.mapH = h;

        nodes = new AStarNode[w, h];
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                AStarNode node = new AStarNode(false, new Vector3(4 * i, 3, 4 * j), i, j);
                nodes[i, j] = node;
            }
        }
        //AStarNode node1 = new AStarNode(true, new Vector3(4, 3, 0), 1, 0);
        //nodes[1, 0] = node1;
        //AStarNode node2 = new AStarNode(true, new Vector3(8, 3, 8), 2, 2);
        //nodes[2, 2] = node2;
        //AStarNode node3 = new AStarNode(true, new Vector3(16, 3, 16), 4, 4);
        //nodes[4, 4] = node3;
        //AStarNode node4 = new AStarNode(true, new Vector3(4, 3, 4), 1, 1);
        //nodes[1, 1] = node4;
        //AStarNode node5 = new AStarNode(true, new Vector3(4, 3, 8), 1, 2);
        //nodes[1, 2] = node5;
        //AStarNode node6 = new AStarNode(true, new Vector3(4, 3, 0), 1, 0);

    }

    public List<AStarNode> FindPath(Vector3 startPosition, Vector3 endPosition)
    {
        InitMapInfo(15, 15);
        //坐标转换
        Vector2 startPos = new Vector2(startPosition.x / 4, startPosition.z / 4);
        Vector2 endPos = new Vector2(endPosition.x / 4, endPosition.z / 4);

        //if(startPos.x <0 || startPos.x)  判断合法与否，可以省略先
        //return null;
        AStarNode start = nodes[(int)startPos.x, (int)startPos.y];
        AStarNode end = nodes[(int)endPos.x, (int)endPos.y];
        //if(start.isObstacle == true ||)
        closeList.Clear();
        openList.Clear();

        start.parent = null;
        start.hCost = 0;
        start.gCost = 0;
        closeList.Add(start);

        while (true)
        {
            //上
            FindNearlyNodeToOpenList(start.x, start.y - 1, 1, start, end);
            //左
            FindNearlyNodeToOpenList(start.x - 1, start.y, 1, start, end);
            //右
            FindNearlyNodeToOpenList(start.x + 1, start.y, 1, start, end);
            //下
            FindNearlyNodeToOpenList(start.x, start.y + 1, 1, start, end);

            if (openList.Count == 0)
            {
                return null;//是死路
            }
            //openList.Sort(SortOpenList);
            openList.Sort((p1, p2) => p1.fCost - p2.fCost);

            closeList.Add(openList[0]);

            start = openList[0];
            openList.RemoveAt(0);
            if (start == end)
            {
                List<AStarNode> path = new List<AStarNode>();
                path.Add(end);
                while (end.parent != null)
                {
                    path.Add(end.parent);
                    end = end.parent;
                }
                path.Reverse();

                return path;
            }
        }


    }

    private int SortOpenList(AStarNode a, AStarNode b)
    {
        if (a.fCost > b.fCost)
            return 1;
        else
            return -1;
    }
    private void FindNearlyNodeToOpenList(int x, int y, int g, AStarNode father, AStarNode end)
    {
        if (x < 0 || x >= mapW || y < 0 || y >= mapH) return;
        AStarNode node = nodes[x, y];
        if (node == null || node.isObstacle == true || closeList.Contains(node) || openList.Contains(node)) return;
        node.parent = father;
        node.gCost = (int)(father.gCost + g);
        node.hCost = Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
        openList.Add(node);
    }
}
