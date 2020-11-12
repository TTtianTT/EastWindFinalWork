using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeGameData : MonoBehaviour
{
    public static void SaveScore()
    {
        DateTime date = DateTime.Now;
        string constructorString = "datasource=localhost;database=steamdb;user=root1;pwd=123456;charset=utf8";
        
        using (MySqlConnection conn = new MySqlConnection(constructorString))
        {
            using (MySqlCommand cmd = new MySqlCommand
            ("INSERT INTO towerdefense(name,date,time) VALUES(@name,@date,@time)", conn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Name", "TLJava");
                cmd.Parameters.AddWithValue("@date", date);
                //cmd.Parameters.AddWithValue("time", time);
                cmd.ExecuteNonQuery();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
