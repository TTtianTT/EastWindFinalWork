using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    public int hp = 150;  //敌人初始血量
    public GameObject explosionEffect;//敌人销毁特效
    private Transform[] positions;
    private int index = 0;

    public bool[,] mapStatus;//用来保存地图状态的数组
    public GameObject[,] open;
    public GameObject[,] close;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        if (hp <= 0) return;
        hp -= damage;
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
}
