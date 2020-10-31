using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour
{
    [HideInspector]
    public GameObject turretGo;//保存当前Cube

    //创建炮塔
    public void BuildTurret(GameObject turretPrefab);
}
