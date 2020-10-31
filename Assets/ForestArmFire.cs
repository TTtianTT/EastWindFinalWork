using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ForestArmFire : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger("Fire");

    }

    // Update is called once per frame
    void Update()
    {
        //InvokeRepeating("SetArm", 2, 3);
        //Invoke("SetArm", 5);
        //Invoke("ResetArm", 5);
        //InvokeRepeating("ResetArm", 2, 3);
        //StartCoroutine(SetArm());
        //StartCoroutine(ResetArm());
    }

    IEnumerator SetArm()
    {
        
        yield return new WaitForSeconds(3);
        transform.Rotate(123f, 0, 0);

    }

    IEnumerator ResetArm()
    {
        
        yield return new WaitForSeconds(3);
        transform.Rotate(-123, 0, 0);
    }

    public void Fire()
    {
        Debug.Log("Fire!!!!!!");
    }
}
