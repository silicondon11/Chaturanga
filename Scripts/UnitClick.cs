using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Threading;

public class UnitClick : MonoBehaviour
{
    private Coroutine DMCoroutine;

    private DistanceManager distanceManager;

    private void OnMouseDown()
    {

        GameObject item = gameObject;

        StartCoroutine(UnitClickCoroutine(item));

    }

    private IEnumerator UnitClickCoroutine(GameObject item)
    {
        GameObject distanceManagerObject = GameObject.Find("DistanceMgr");
        DistanceManager distanceManager = distanceManagerObject.GetComponent<DistanceManager>();

        if (DMCoroutine != null)
        {
            StopCoroutine(DMCoroutine);
            yield return new WaitForSeconds(0.1f);
        }
        DMCoroutine = StartCoroutine(distanceManager.DistanceManagerCoroutine(item));

        yield return null;
    }

}
