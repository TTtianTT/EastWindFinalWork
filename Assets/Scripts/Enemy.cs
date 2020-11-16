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
        AStarManager.getInstance().UpdateMap();
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
        gameObject.transform.LookAt(pos[index].postion);
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
        //实例化特效
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }

}
