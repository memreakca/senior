using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPack : MonoBehaviour
{
    public Transform[] spawnPositions;
    public GameObject mushroomPrefab;

    private void Start()
    {
        SpawnMushrooms();
    }

    public void SpawnMushrooms()
    {
        for (int i =0; i<spawnPositions.Length; i++)
        {
            Instantiate(mushroomPrefab, spawnPositions[i].position,Quaternion.identity);
        }
    }
}
