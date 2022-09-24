using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stage : MonoBehaviour
{
    public int GetEnemiesCount()
    {
        EnemySpawnPoint[] enemies = GetComponentsInChildren<EnemySpawnPoint>();
        return enemies.Length;
    }
}
