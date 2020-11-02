using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //利用触发器检测是否有敌人进入攻击范围，发动攻击
    //只攻击最先进入范围的敌人，如果前面的敌人死了，则攻击下一个
    public List<GameObject> enemys = new List<GameObject>();//存放进入攻击范围的敌人
    void OnTriggerEnter(Collider col) //敌人进入攻击范围
    {
        if(col.tag == "Enemy")//利用标签判断是否是敌人
        {
            enemys.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)//敌人退出攻击范围
    {
        if(col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

    public float attackRateTime = 1; //1秒攻击一次
    private float timer = 0;
    public GameObject bulletPrefab;  //子弹
    public Transform firePosition;
         
    void Start()
    {
        timer = attackRateTime;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(enemys.Count>0&&timer>=attackRateTime)
        { 
            timer -= attackRateTime;
            Attack();
        }
    }

    void Attack()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
        bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
    }
}
