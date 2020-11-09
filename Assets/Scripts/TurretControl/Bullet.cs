using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //控制炮弹的飞行
    public int damage = 50;//子弹的伤害值

    public float speed = 20; //子弹飞行速度

    public GameObject explosionEffectPrefab; //子弹碰上敌人的特效

    private float distanceArriveTarget = 1.2f;  //检测子弹和敌人的距离

    private Transform target;  //移动目标

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    //子弹进行的更新操作
    void Update()
    {
        //目标怪物消失，调用方法销毁子弹
        if(target == null)
        {
            Die();
            return;  //以下代码不再运行
        }

        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Vector3 dir = target.position - transform.position;
        if(dir.magnitude < distanceArriveTarget)
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
    }

    //触发器检测子弹和敌人碰撞
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    //敌人被 消灭之后，子弹自行销毁
    void Die()
    {
        //GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        //Destroy(effect, 1);    //1秒之后销毁对象
        Destroy(this.gameObject);
    }
}
