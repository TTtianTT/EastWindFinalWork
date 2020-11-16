using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeShow1 : MonoBehaviour
{
    public Text timeShowText;
    private float spendTime;
    private int hour;
    private int minute;
    public int second;
    private int milliSecond;
    // Start is called before the first frame update
    void Start()
    {
        timeShowText = GetComponent<Text>();  //获取Text组件
    }

    // Update is called once per frame
    void Update()
    {
        spendTime += Time.deltaTime;
        //将时间转换成时分秒毫秒
        hour = (int)spendTime / 3600;
        minute = (int)(spendTime - hour * 3600) / 60;
        second = (int)(spendTime - hour * 3600 - minute * 60);
        //milliSecond = (int)(spendTime - (int)spendTime * 1000);

        timeShowText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",        //:{3:D3}
            hour, minute, second);             //, milliSecond

        //显示时分秒时间
        #if UNITY_EDITOR
        print("spendTime" + spendTime);
        #endif
    }
}
