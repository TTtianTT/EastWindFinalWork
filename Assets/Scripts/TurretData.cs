using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretData : MonoBehaviour
{
    public GameObject turretPrefab;
    public int cost;
    public GameObject turretUpgradePrefab;
    public int costUpgradePrefab;
    public TurretType type;
}

public enum TurretType
{
    LaserTurret,
    MissileTurret,
    StandardTurret
}
