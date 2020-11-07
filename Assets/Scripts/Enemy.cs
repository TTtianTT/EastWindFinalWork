using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    public int hp = 150;  //敌人初始血量
    private int totalHp;    //敌人总血量
    public GameObject explosionEffect;//敌人销毁特效
    public Slider hpSlier;  //敌人血量条
    private Transform[] positions;
    private int index = 0;

    private List<AStarNode> pos;//存放路径

    //public bool[,] mapStatus = new bool[15, 15];//用来保存地图状态的数组
    //public GameObject[,] open;
    //public GameObject[,] close;


    // Start is called before the first frame update

    void Awake()
    {
        Vector3 startPosition = new Vector3(0, 3, 0);
        Vector3 endPosition = new Vector3(56, 3, 56);
        pos = new List<AStarNode>();
        pos = AStarManager.getInstance().FindPath(startPosition, endPosition);
    }
    void Start()
    {
        totalHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    void Move()
    {

        float step = speed * Time.deltaTime;

        if (index > pos.Count - 1) return;
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, pos[index].postion, step);
        if (Vector3.Distance(pos[index].postion, transform.position) < 0.02f)
        {
            index++;
        }
        if (index > pos.Count - 1)
        {
            Debug.Log("到达终点了");
            ReachDestination();
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
        if (hp <= 0) return;
        hp -= damage;
        hpSlier.value = (float)hp / totalHp;//敌人受到攻击之后，对血量条做处理
        if(hp<=0)   //敌人没血之后做处理
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
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

    //void InitMap()
    //{
    //    for(int i=1;i<=224;i++)//将位置数组填充
    //    {
    //        var gameobj = GameObject.Find($"GrassCube ({i})");
    //        Vector3 vec = gameobj.transform.position;
    //        int x = Convert.ToInt32(vec.x);
    //        int z = Convert.ToInt32(vec.z);
    //        //float x = vec.x;
    //        //float z = vec.z;
    //        this.mapStatus[x / 4, z / 4] = false;
    //        Debug.Log(this.mapStatus[x / 4, z / 4]);
    //    }
    //}
}
