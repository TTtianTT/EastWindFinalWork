using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystem;

[System.Serializable]
public class BuildManager : MonoBehaviour
{
    public TurretData laserturretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    //表示当前选择的炮台（要建造的炮台）
    private TurretData selectedTurretData;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //开发炮台的建造
                Ray ray = camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent < "MapCube" > ();
                    if (mapCube.turretGo == null)
                    {
                        //可以创建
                        if (money > selectedTurretData.cost)
                        {
                            money -= selectedTurretData.cost;
                            mapCube.BuildTurret(selectedTurretData.TurreetPrefab);
                        }
                        else
                        {
                            //提示钱不够
                        }
                    }
                }
            }
        }
    }

    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }
}

