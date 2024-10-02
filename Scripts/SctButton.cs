using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SctButton : MonoBehaviour
{
    public bool sctFlag;

    private Coroutine coroutine = null;

    private GameObject distanceMgr;

    private DistanceManager dist;


    void Start()
    {
        distanceMgr = GameObject.Find("DistanceMgr");

        dist = distanceMgr.GetComponent<DistanceManager>();
    }

    private void OnMouseDown()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(SctButtonCoroutine());
        }
        sctFlag = true;
        dist.unitMenuFlag = true;//put in every button script
    }

    private IEnumerator SctButtonCoroutine()
    {
        UnityEngine.Vector3 loc = dist.itemLoc;
        CampObject camp = dist.FindNearbyCamps(loc);

        //Code for setting mines, digging trenches, or building bridges

        yield return null;
    }
}
