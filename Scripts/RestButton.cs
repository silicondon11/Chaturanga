using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public class RestButton : MonoBehaviour
{
    public bool restFlag;

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
            coroutine = StartCoroutine(RestCoroutine());
        }
        restFlag = true;
        dist.unitMenuFlag = true;//put in every button script
    }

    private IEnumerator RestCoroutine()
    {
        UnityEngine.Vector3 loc = dist.itemLoc;
        //increase wood and set a time delay, while the visual meter is increasing

        UnityEngine.Vector3 timerLoc = loc + new UnityEngine.Vector3(0f, 25f, 0f);
        GameObject timer = Instantiate(dist.timerPrefab, timerLoc, UnityEngine.Quaternion.identity);

        yield return null;
    }


}
