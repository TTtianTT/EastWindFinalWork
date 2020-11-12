using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mysql : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string constructorString = "datasource=localhost;database=steamdb;user=root1;pwd=123456;charset=utf8";
        MySqlConnection conn = new MySqlConnection(constructorString);
        try
        {
            conn.Open();
            Debug.Log("已经建立连接");
            string sql = "select * from towerdefense";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {

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
