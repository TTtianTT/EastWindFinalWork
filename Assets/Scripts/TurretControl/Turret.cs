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
    public Transform turretbody;    //炮塔转向追踪敌人的部分
         
    void Start()
    {
        timer = attackRateTime;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(enemys.Count>0&&timer>=attackRateTime)
        { 
            timer = 0;
            Attack();
        }

        //有敌人时，炮塔指向第一个敌人（y轴方向一致）
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = turretbody.position.x;
            turretbody.LookAt(targetPosition);
        }
    }

    //炮塔攻击目标
    void Attack()
    {
        if(enemys[0]==null)  //最前面的敌人血量为0之后，更新敌人列表
        {
            UpdateEnemy();
        }
        if(enemys.Count>0)  //敌人数量大于0，攻击
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);

        }
        else
        {
            timer = attackRateTime;
        }
    }

    //更新敌人列表，移除血量为0的敌人
    void UpdateEnemy()
    {
        //enemys.RemoveAll(null);
        List<int> emptyIndex = new List<int>();//保存所有血量为0的敌人
        for(int index = 0;index <enemys.Count; index++)
        {
            if(enemys[index]==null)
            {
                emptyIndex.Add(index);
            }
        }

        for(int i=0;i<emptyIndex.Count;i++)
        {//根据索引移除
            enemys.RemoveAt(emptyIndex[i] - i);
        }
    }
}
