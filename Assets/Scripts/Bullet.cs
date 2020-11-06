using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //控制炮弹的飞行
    public int damage = 50;//子弹的伤害值

    public float speed = 20; //子弹飞行速度

    private Transform target;  //移动目标

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
