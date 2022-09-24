using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static Action<Vector3> onSetPlayerMovePoint;
    public static Action onEnemyDeath;
    [SerializeField] private GameObject[] enemiesPrefabs;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform enemiesPool;
    private Stage[] stages;
    private PlayerPosition[] playerPositions;
    private int deadEnemyCount;
    private int currentPlayerMovePoint;
    private void Awake()
    {
        UI.onGameStarted += SetPointToMove;
        playerPositions = GetComponentsInChildren<PlayerPosition>();
        stages = GetComponentsInChildren<Stage>();
        SpawnEnemy();
        SpawnPlayer();
        onEnemyDeath += CountDeadEnemies;
    }
    private void OnDisable()
    {
        onEnemyDeath -= CountDeadEnemies;
        UI.onGameStarted -= SetPointToMove;
    }
    private void SpawnPlayer()
    {
        GameObject player=Instantiate(playerPrefab);
        player.transform.position = playerPositions[0].transform.position;
    }
    private void SpawnEnemy()
    {
        foreach(Stage stage in stages)
        {
            EnemySpawnPoint[] enemiesSpawnPoints = stage.GetComponentsInChildren<EnemySpawnPoint>();
            for(int i=0;i<enemiesSpawnPoints.Length;i++)
            {
                GameObject spawnObject = enemiesPrefabs[UnityEngine.Random.Range(0, enemiesPrefabs.Length)];
                Vector3 spawnPosition = new Vector3 (enemiesSpawnPoints[i].transform.position.x,
                    enemiesSpawnPoints[i].transform.position.y+0.5f*spawnObject.gameObject.transform.localScale.y,enemiesSpawnPoints[i].transform.position.z);
                GameObject newObject = Instantiate(spawnObject, spawnPosition, Quaternion.identity, enemiesPool);
            }
        }    
    }
    private void CountDeadEnemies()
    {
        deadEnemyCount++;
        Debug.Log(stages[currentPlayerMovePoint+1].GetEnemiesCount());
        if (deadEnemyCount==stages[currentPlayerMovePoint].GetEnemiesCount())
        {
            SetPointToMove();
            deadEnemyCount = 0;
        }
    }
    private void SetPointToMove()
    {
        currentPlayerMovePoint++;
        onSetPlayerMovePoint?.Invoke(playerPositions[currentPlayerMovePoint].transform.position);
    }
}
