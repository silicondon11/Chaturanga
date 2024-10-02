using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DistInfoDelayScript : MonoBehaviour
{
    public IEnumerator SpawnAndDestroyCoroutine(GameObject distanceInfoPrefab, UnityEngine.Vector3 spawnPosition, UnityEngine.Quaternion rotation)
    {
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("DistanceInfo");
        if (spawnedObjects.Length > 0)
        {
            yield break;
        }

        GameObject spawnedObject = Instantiate(distanceInfoPrefab, spawnPosition, rotation);
        spawnedObject.tag = "DistanceInfo";

        

        yield return new WaitForSeconds(1.5f);

        Destroy(spawnedObject);
    }
}
