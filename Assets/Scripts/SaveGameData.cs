using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeGameData : MonoBehaviour
{
    public void SaveScore(string statue)
    {
        DateTime date = DateTime.Now;
        //TimeShow timeShow
        TimeShow1 timeshow = new TimeShow1();
        int time = (int)timeshow.second; 
        string constructorString = "datasource=127.0.0.1;database=steamdb;user=root1;pwd=123456;charset=utf8";
        
        using (MySqlConnection conn = new MySqlConnection(constructorString))
        {
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand
                ("INSERT INTO towerdefense(name,date,time,statue) VALUES(@name,@date,@time,@statue)", conn))
                {
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Name", "TLJava");
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("time", time);
                    cmd.Parameters.AddWithValue("statue", statue);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                Debug.Log(ex);
            }
            finally
            {
                conn.Close();
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
