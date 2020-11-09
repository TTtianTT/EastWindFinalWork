using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    public float hp = 150;  //敌人初始血量
    private float totalHp;    //敌人总血量
    public GameObject explosionEffect;//敌人销毁特效
    public Slider hpSlier;  //敌人血量条
    private Transform[] positions;
    private int index = 0;

    private List<AStarNode> pos;//存放路径


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
        //ChangeMap();

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
            //Debug.Log("到达终点了");
            ReachDestination();
        }
    }

    void ReachDestination()//抵达终点
    {
        GameManager.Instance.Failed();
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

    public void TakeDamage(float damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        hpSlier.value = (float)hp / totalHp;//敌人受到攻击之后，对血量条做处理
        if (hp <= 0)   //敌人没血之后做处理
        {
            Die();
        }
    }

    void Die()
    {
        //GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        //Destroy(effect, 1.5f);
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
    void ChangeMap()
    {
        AStarNode node1 = new AStarNode(true, new Vector3(4, 3, 0), 1, 0);
        AStarManager.getInstance().nodes[1, 0] = node1;

        //int x = (int)vector.x / 4;
        //int y = (int)vector.y / 4;
        //AStarNode node1 = new AStarNode(true,vector, x, y);
        //AStarManager.getInstance().nodes[x, y] = node1;
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
