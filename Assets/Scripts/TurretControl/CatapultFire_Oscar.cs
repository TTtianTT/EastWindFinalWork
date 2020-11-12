using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class CatapultFire_Oscar : MonoBehaviour
{
    public List<GameObject> enemys;
    Animator animator;
    public float attackRateTime = 1; //1秒攻击一次
    private float timer = 0;

    void Start()
    {
        enemys = new List<GameObject>();
        animator = GetComponent<Animator>();
        timer = attackRateTime;
    }

    void Update()
    {
        //if (enemys.count > 0)
        //{
        //    animator.settrigger("fire");
        //    animator.settrigger("reset");
        //}

        timer += Time.deltaTime;
        if (enemys.Count > 0 && timer >= attackRateTime)
        {
            timer = 0;
            Attack();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

    void Attack()
    {
        if (enemys[0] == null)  //最前面的敌人血量为0之后，更新敌人列表
        {
            UpdateEnemy();
        }
        if (enemys.Count > 0)  //敌人数量大于0，攻击
        {
            animator.SetTrigger("Fire");
            animator.SetTrigger("Reset");

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
        for (int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }

        for (int i = 0; i < emptyIndex.Count; i++)
        {//根据索引移除
            enemys.RemoveAt(emptyIndex[i] - i);
        }
    }


}
