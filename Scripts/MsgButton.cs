using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgButton : MonoBehaviour
{
    public bool msgFlag;

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
            coroutine = StartCoroutine(MsgButtonCoroutine());
        }
        msgFlag = true;
        dist.unitMenuFlag = true;//put in every button script
    }

    private IEnumerator MsgButtonCoroutine()
    {
        UnityEngine.Vector3 loc = dist.itemLoc;
        CampObject camp = dist.FindNearbyCamps(loc);

        //Code for sending different types of messages to the enemy command

        yield return null;
    }
}
