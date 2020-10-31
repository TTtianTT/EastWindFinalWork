using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EagleFly : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //InvokeRepeating("FlyAround", 0.1F, 2F);
        FlyAround();
            
        
    }



    public void FlyAround()
    {
        transform.RotateAround(new Vector3(3f, 0f, 0f), new Vector3(0f, 1f, 0f), 2f);
    }
   
}
