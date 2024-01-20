using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneEnemySpawner : MonoBehaviour
{
    public GameObject stoneEnemyPrefab;
    public float timeBetweenSpawns = 5f;
    public bool isEnemyAlive;

    private void Start()
    {
        SpawnStoneEnemy();
        StartCoroutine(SpawnStoneEnemyWithDelay());
    }

    IEnumerator SpawnStoneEnemyWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);

            if (!isEnemyAlive)
            {
                SpawnStoneEnemy();
            }
        }
    }
    void SpawnStoneEnemy()
    {
        if (!isEnemyAlive)
        {
            var stnEnemy = Instantiate(stoneEnemyPrefab,transform.position,Quaternion.identity);
            stnEnemy.GetComponent<stone_enemy_patrol>().patrolPoint = transform;
            isEnemyAlive = true;  

            stone_enemy_sc stoneEnemySc = stnEnemy.GetComponent<stone_enemy_sc>();
            stoneEnemySc.OnEnemyDeath.AddListener(OnEnemyDeathCallback);
        }

    }
    void OnEnemyDeathCallback()
    {
        isEnemyAlive = false;
    }
}
