using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public GameObject NodeWall;
    public GameObject Node;
    private GameObject WallRange, PathRange;

    public int length=15;
    public int width=15;
    public APoint[,] map;
    public static List<APoint> pathPoints;
    private List<GameObject> pathObj;

    public Transform start;
    public Transform end;

    public Map()
    {//构造函数
        this.map = new APoint[length, width];
        for(int x = 0;x<length;x++)
        {
            for(int y = 0;y<width;y++)
            {
                Vector3 vector = new Vector3(x * 4, 0.5f, y * 4);
                APoint point = new APoint(false,vector,x,y);
            }
        }
        pathPoints = new List<APoint>();
        this.start.position =new Vector3(0, 0.5f, 0);
        this.end.position = new Vector3(56, 0.5f, 56);
    }

    public APoint getItem(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x) /4;
        int y = Mathf.RoundToInt(position.y) /4;
        x = Mathf.Clamp(x, 0, width - 1);
        y = Mathf.Clamp(y, 0, length - 1);
        return map[x, y];
    }
    public List<APoint> getNeighborhood(APoint node)
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
                if (x < width && x >= 0 && y < length && y >= 0)
                    list.Add(map[x, y]);
            }
        }
        return list;
    }
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
}
