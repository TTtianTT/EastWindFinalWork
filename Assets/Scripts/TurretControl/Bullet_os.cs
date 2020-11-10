using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_os : MonoBehaviour
{

    public int damage = 50;
    public float speed = 20;
    private Transform target;
    public GameObject explosionEffectPrefab; //子弹碰上敌人的特效

    private float distanceArriveTarget = 1.2f;  //检测子弹和敌人的距离

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }

        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);


        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distanceArriveTarget)
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
            Die();
            col.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    //敌人被消灭之后，子弹自行销毁
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1);    //1秒之后销毁对象
        Destroy(this.gameObject);
    }

}
