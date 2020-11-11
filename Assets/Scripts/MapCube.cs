using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turretGo;//保存当前Cube
    public TurretData turretData;
    [HideInInspector]
    public bool isUpgraded = false;//是否升级

    public GameObject buildEffect;//建造特效

    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();

    }
    //升级
    public void UpgradeTurret()
    {
        if (isUpgraded == true) return;

        Destroy(turretGo);
        isUpgraded = true;
        turretGo = GameObject.Instantiate(turretData.turretUpgradePrefab, transform.position, Quaternion.identity);

    }
    public void DestroyTurret()
    {
        Destroy(turretGo);
        isUpgraded = false;
        turretGo = null;
        turretData = null;
    }
    //创建炮塔

    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isUpgraded = false;
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
     //   Destroy(effect, 1);//一秒之后，让特效消失
    }


    private void OnMouseEnter()
    {
        if (turretGo == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            renderer.material.color = Color.green;
        }
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

}
