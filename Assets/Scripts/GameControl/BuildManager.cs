using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BuildManager : MonoBehaviour
{
    public TurretData Turret1Data;
    public TurretData Turret2Data;
    public TurretData Turret3Data;

    //表示当前选择的炮台（要建造的炮台）
    private TurretData selectedTurretData;
    //表示当前选择的炮台（场景中的游戏物体）
    private GameObject selectedTurretGo;

    private Animator upgradeCanvasAnimator;

    public GameObject upgradeCanvas;

    public Button buttonUpgrade;

    public Text moneyText;       //显示金额

    public Animator moneyAnimator;  //钱的特效

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
                            mapCube.BuildTurret(selectedTurretData.turretPrefab);  //使用建造特效
                        }
                        else
                        {
                            //TOOOO提示钱不够
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }

                    else if (mapCube.turretGo != null)
                        {
                            //TOOO升级处理
                            if (mapCube.turretGo == selectedTurretGo && upgradeCanvas.activeInHierarchy)
                            {
                                StartCoroutine(HideUpgradeUI());
                            }
                            else
                            {
                                ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                            }
                            selectedTurretGo = mapCube.turretGo;
                        }
                        

                    else if(mapCube.turretGo != null)  //方块上面已经有炮塔
                    {
                        //升级处理

                    }

                }
            }
        }
    }

    public void OnTurret1Selected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = Turret1Data;
        }
    }

    public void OnTurret2Selected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = Turret2Data;
        }
    }

    public void OnTurret3Selected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = Turret3Data;
        }
    }


void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
{
    StopCoroutine("HideUpgradeUI");
    upgradeCanvas.SetActive(false);
    upgradeCanvas.SetActive(true);
    upgradeCanvas.transform.position = pos;
    buttonUpgrade.interactable = !isDisableUpgrade;
}
IEnumerator HideUpgradeUI()
{
    upgradeCanvasAnimator.SetTrigger("Hide");
    //upgradeCanvas.SetActive(false);
    yield return new WaitForSeconds(0.8f);
    upgradeCanvas.SetActive(false);
}

public void OnUpgradeButtonDown()
{
    //tooo
}
public void OnDestroyButtonDown()
{
    //tooo
}
}

