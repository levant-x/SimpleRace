using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabStorage : MonoBehaviour
{
    public static PrefabStorage instance;
    public GameObject[] prefabs;

    int[] spawnWeights;
    int weightSum;


    void Start()
    {
        instance = this;
    }


    public GameObject GetRandomPrefab()
    {
        CalculateMean();

        float chance = Random.Range(0, weightSum);
        int index = 0;
        for (; index < spawnWeights.Length; index++)
        {
            chance -= spawnWeights[index];
            if (chance <= 0) break;
        }
        return prefabs[index];
    }


    private void CalculateMean()
    {
        CollectFrequencies();
        weightSum = 0;
        foreach (var freq in spawnWeights) weightSum += freq;
    }

    private void CollectFrequencies()
    {
        spawnWeights = new int[prefabs.Length];
        for (int i = 0; i < prefabs.Length; i++)
        {
            var freq = prefabs[i].GetComponent<ItemFrequency>();
            spawnWeights[i] = freq.dropFrequency;
        }
    }
}
