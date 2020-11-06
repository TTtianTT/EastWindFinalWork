using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    private Transform[] positions;
    private int index = 0;

    public bool[,] mapStatus = new bool[15, 15];//用来保存地图状态的数组
    public GameObject[,] open;
    public GameObject[,] close;


    // Start is called before the first frame update
    void Start()
    {
        InitMap();
    }

    // Update is called once per frame
    void Update()
    {
        //TestVar();
        Move();
    }


    public void Move()  //敌人移动
    {
        if(transform.position.x<56)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            if (transform.position.z < 56)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                return;
            }
        }
    }

    void ReachDestination()//抵达终点
    {
        GameObject.Destroy(this.gameObject); 
    }

    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

    public void TakeDamage(int damage)
    {
        
    }
    void FindMap()
    {
        var gameobj = GameObject.Find("GrassCube (115)");
        Vector3 vec = gameobj.transform.position;
        if (gameobj == null)
        {
            Debug.Log("没找到");
        }
        else
        {
            Debug.Log("找到了");
            Debug.Log(vec.x);
            Debug.Log(vec.z);//成功
        }
    }

    void InitMap()
    {
        for(int i=1;i<=224;i++)//将位置数组填充
        {
            var gameobj = GameObject.Find($"GrassCube ({i})");
            Vector3 vec = gameobj.transform.position;
            int x = Convert.ToInt32(vec.x);
            int z = Convert.ToInt32(vec.z);
            //float x = vec.x;
            //float z = vec.z;
            this.mapStatus[x / 4, z / 4] = false;
            Debug.Log(this.mapStatus[x / 4, z / 4]);
        }
    }
}
