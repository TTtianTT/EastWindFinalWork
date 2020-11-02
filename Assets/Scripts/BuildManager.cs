using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class BuildManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    //表示当前选择的炮台（要建造的炮台）
    private TurretData selectedTurretData;

    public Text moneyText;       //显示金额

    public Animator moneyAnimator;

    private int money = 1000;    //初始金额，根据最终定价进行修改

    void ChangeMoney(int change=0)
    {
        money += change;
        moneyText.text = "¥" + money;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //开发炮台的建造
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //捕获鼠标点击发射的激光
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));//获取集中的方块
                if (isCollider)
                {             //首先判断被点中的方块上是否为空
                    MapCube mapCube = hit.collider.GetComponent < MapCube > ();
                    if (selectedTurretData !=null && mapCube.turretGo == null)
                    {
                        //钱足够，可以创建
                        if (money > selectedTurretData.cost)
                        {
                            ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData.turretPrefab);
                        }
                        else
                        {
                            //提示钱不够
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else if(mapCube.turretGo != null)  //方块上面已经有炮塔
                    {
                        //升级处理

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

    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }
}

