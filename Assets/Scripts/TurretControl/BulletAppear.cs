using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAppear : MonoBehaviour
{
    public Transform bulletPosition;
    public GameObject bulletPrefab;
    public List<GameObject> enemys = new List<GameObject>();
    CatapultFire_Oscar c2 ;
    public Transform head;
    // Start is called before the first frame update
    void Start()
    {
        //c2 = new CatapultFire_Oscar();
        c2 = GameObject.Find("ForestCatapult_Red/ForestCatapultArm").GetComponent<CatapultFire_Oscar>();//获取ForesCatapultArm上面的enemys
    }

    // Update is called once per frame
    void Update()
    {
        c2 = GameObject.Find("ForestCatapult_Red/ForestCatapultArm").GetComponent<CatapultFire_Oscar>();//获取ForesCatapultArm上面的enemys
        enemys = c2.enemys;
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arm")
        {
            // Debug.Log("Tri");
            if (enemys.Count > 0)
            {
                CreateBullet();
            }
            //CreateBullet();

        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Arm")
        {
            //Debug.Log("Arm Exit");
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
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletPosition.position, bulletPosition.rotation);
            bullet.GetComponent<Bullet_os>().SetTarget(enemys[0].transform);
        }
        else
        {
            Debug.Log(enemys.Count);
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
