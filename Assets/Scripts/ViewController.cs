using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{

    public float speed = 50;
    public float mouseSpeed = 1000;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h * speed, mouse * mouseSpeed, v * speed) * Time.deltaTime, Space.World);
    }
}
