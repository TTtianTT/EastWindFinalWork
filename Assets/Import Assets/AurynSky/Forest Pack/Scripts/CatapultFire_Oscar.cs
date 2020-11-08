using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class CatapultFire_Oscar : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        if(enemys.Count>0)
        {
            animator.SetTrigger("Fire");
            animator.SetTrigger("Reset");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }

     void OnTriggerExit(Collider col)
    {
        if(col.tag=="Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

}
