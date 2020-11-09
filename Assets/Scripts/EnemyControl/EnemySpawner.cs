using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public static int CountEnemyAlive = 0;
    public Wave[] waves;
    public Transform START;
    public float waveRate = 0.2f;
    private Coroutine coroutine;

    void Start()
    {
        coroutine = StartCoroutine(SpawnEnemy());
    }
    public void Stop()
    {
        StopCoroutine(coroutine);
    }
    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, new Vector3(0,3,0), Quaternion.identity);
                //GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                CountEnemyAlive++;
                //Debug.Log($"生成中：{CountEnemyAlive}");
                if (i != wave.count - 1)
                    yield return new WaitForSeconds(wave.rate);
            }

            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }
            Debug.Log($"{CountEnemyAlive}");
            yield return new WaitForSeconds(waveRate);
        }
        //Debug.Log($"敌人存活的数量{CountEnemyAlive}");
        while (CountEnemyAlive > 0)
        {
            yield return 0;
        }
        GameManager.Instance.Win();
    }

}
