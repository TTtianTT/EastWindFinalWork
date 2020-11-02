using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
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
        
    }
}
