using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableCreate : MonoBehaviour
{
    public GameObject Row_Prefab;//表头预设
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)//添加并修改预设的过程，将创建10行
        {
            GameObject table = GameObject.Find("Canvas/Panel/table");
            GameObject row = GameObject.Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
                                                   //设置预设实例中的各个子物体的文本内容
            /* 
             basc.FindChildGameObject(row.gameObject, "cell").GetComponent<Text>().text = (i + 1) + "";
             //row.transform.FindChild("cell").GetComponent<Text>().text = (i + 1) + "";
             basce.FindChildGameObject(row.gameObject, "cell1").GetComponent<Text>().text = "name" + (i + 1);
             basc.FindChildGameObject(row.gameObject, "cell2").GetComponent<Text>().text = "class" + (i + 1);
             basc.FindChildGameObject(row.gameObject, "cell3").GetComponent<Text>().text = "da" + (i + 1);
             */
            row.transform.Find("Cell0").GetComponent<Text>().text = (i + 1) + "";
            row.transform.Find("Cell1").GetComponent<Text>().text = "name" + (i + 1);
            row.transform.Find("Cell2").GetComponent<Text>().text = "date" + (i + 1);
            row.transform.Find("Cell3").GetComponent<Text>().text = "score" + (i + 1);
            row.transform.Find("Cell4").GetComponent<Text>().text = "statue" + (i + 1);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
