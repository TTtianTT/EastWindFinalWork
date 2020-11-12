using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class mysql : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //查询数据库操作，显示历史成绩
        string constructorString = "datasource=localhost;database=steamdb;user=root1;pwd=123456;charset=utf8";
        MySqlConnection conn = new MySqlConnection(constructorString);
        try
        {
            conn.Open();
            Debug.Log("已经建立连接");
            string sql = "select * from towerdefense";
            using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, conn))
            {
                using (DataSet ds = new DataSet())
                {
                    dataAdapter.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        foreach (object field in row.ItemArray)
                        {
                            Debug.Log(field + "\t");
                        }
                    }
                }
            }
        }
        catch(MySqlException ex)
        {
            Debug.Log(ex.Message);
        }
        finally
        {
            conn.Close();
            Debug.Log("close connection");
        }
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
