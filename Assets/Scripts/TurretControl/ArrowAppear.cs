using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAppear : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    public Transform arrowPosition;
    public GameObject arrowPrefab;
    public Transform head;
    public float attackRateTime = 1; //1秒攻击一次
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = attackRateTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (enemys.Count > 0 && timer >= attackRateTime)
        {
            timer = 0;
            CreateBullet();
        }
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
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

    void CreateBullet()
    {
        //if (enemys.Count == 0) { return; }
        Debug.Log("enemy的数量为");
        Debug.Log(enemys.Count);
        if (enemys[0] == null)
        {
            UpdateEnemy();
        }
        Debug.Log($"enemy.Count={enemys.Count}");
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(arrowPrefab, arrowPosition.position, arrowPosition.rotation);
            bullet.GetComponent<Arrow1>().SetTarget(enemys[0].transform);
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
        Debug.Log($"{enemys.Count}");
        for (int i = 0; i < emptyIndex.Count; i++)
        {//根据索引移除
            enemys.RemoveAt(emptyIndex[i] - i);
        }
    }

}
