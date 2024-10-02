using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DistanceManager : MonoBehaviour
{
    public List<LineRenderer> lineRenderers;
    private LineRenderer lineRenderer;
    private List<UnityEngine.Vector3[]> positionsArrays;
    private UnityEngine.Vector3[] positionsArray;
    public List<CampObject> camps;

    public GameObject timerPrefab;

    public GameObject unitMenu = null;
    public GameObject unitMenuButtons;
    public GameObject specialButtonEng;
    public GameObject specialButtonSct;
    public GameObject specialButtonMsg;
    public UnityEngine.Vector3 itemLoc;
    public bool unitMenuFlag = false;
    private GameObject specialButton;

    private DistInfoDelayScript distInfoDelayScript;
    private UnitMover unitMover;
    private HighlighterMover highlighterMover;
    private int moveSpeed = 50;
    public GameObject highlighter;
    private int highlighterSpeed = 1000;

    public GameObject sphere;
    private float threshold = 40f;
    public GameObject distanceInfoPrefab;

    public Image statChangeBoxPrefab;
    private int boxCount = 0;

    private bool isMouseClicked = false;
    private bool isReadyForMouseClick = false;
    private Coroutine mouseClickCoroutine = null;


    void Start()
    {
        distInfoDelayScript = GetComponent<DistInfoDelayScript>();
        unitMover = GetComponent<UnitMover>();
        highlighterMover = GetComponent<HighlighterMover>();

        // fill positions array, fix add road coordinates logic
        positionsArrays = new List<UnityEngine.Vector3[]>(lineRenderers.Count);

        for (int i = 0; i < lineRenderers.Count; i++)
        {
            lineRenderer = lineRenderers[i];
            positionsArray = new UnityEngine.Vector3[lineRenderer.positionCount];

            lineRenderer.GetPositions(positionsArray);
            positionsArrays.Add(positionsArray);

            UnityEngine.Vector3 worldPosition = LineRendererUtility.GetLineRendererWorldPosition(lineRenderer);

            for (int j = 0; j < positionsArray.Length; j++)
            {
                positionsArray[j] = positionsArray[j] + worldPosition + new UnityEngine.Vector3(0, 30, 0);
            }
        }

        // Stop the coroutine if it's already running
        
    }

    public IEnumerator DistanceManagerCoroutine(GameObject item)
    {
        
        if (sphere == null)
        {
            UnityEngine.Debug.LogError("Sphere GameObject is not assigned.");
            
        }

        bool exitLoop = false;
        float timer = 0f;
        float maxDuration = 30f;
        bool highlighterFlag = true;
        int highlighterCount = 10;
        itemLoc = item.transform.position;


        while (!exitLoop && timer < maxDuration)
        {
            highlighterFlag = true;
            for (int j = 0; j < positionsArrays.Count; j++)
            {
                UnityEngine.Vector3[] positionsArray = positionsArrays[j];

                for (int i = 0; i < positionsArray.Length; i++)
                {
                    
                    // Check for intersection with the sphere using a threshold of error
                    if (UnityEngine.Vector3.Distance(positionsArray[i], sphere.transform.position) <= threshold)
                    {
                        if (UnityEngine.Vector3.Distance(positionsArray[i], item.transform.position) <= threshold)
                        {

                            if (unitMenu == null)
                            {
                                UnityEngine.Vector3 spawnPosition = item.transform.position + new UnityEngine.Vector3(0f, 25f, 60f);
                                unitMenu = Instantiate(unitMenuButtons, spawnPosition, UnityEngine.Quaternion.identity);


                                UnityEngine.Debug.LogError(item.name);
                                //Special buttons
                                if (item.name == "Engineers(Clone)")
                                {
                                    if (specialButton != null)
                                    {
                                        Destroy(specialButton);
                                    }
                                    UnityEngine.Vector3 rotVector = new UnityEngine.Vector3(0f, 0f, 135f);
                                    UnityEngine.Quaternion rot = UnityEngine.Quaternion.Euler(rotVector);
                                    UnityEngine.Vector3 specPosition = spawnPosition + new UnityEngine.Vector3(0f, 15f, -62f);
                                    specialButton = Instantiate(specialButtonEng, specPosition, rot, unitMenu.transform);
                                }
                                else if (item.name == "Scouts(Clone)")
                                {
                                    if (specialButton != null)
                                    {
                                        Destroy(specialButton);
                                    }
                                    UnityEngine.Vector3 rotVector = new UnityEngine.Vector3(0f, 0f, 135f);
                                    UnityEngine.Quaternion rot = UnityEngine.Quaternion.Euler(rotVector);
                                    UnityEngine.Vector3 specPosition = spawnPosition + new UnityEngine.Vector3(0f, 15f, -62f);
                                    specialButton = Instantiate(specialButtonSct, specPosition, rot, unitMenu.transform);
                                }
                                else if (item.name == "Messengers(Clone)")
                                {
                                    if (specialButton != null)
                                    {
                                        Destroy(specialButton);
                                    }
                                    UnityEngine.Vector3 rotVector = new UnityEngine.Vector3(0f, 0f, 135f);
                                    UnityEngine.Quaternion rot = UnityEngine.Quaternion.Euler(rotVector);
                                    UnityEngine.Vector3 specPosition = spawnPosition + new UnityEngine.Vector3(0f, 15f, -62f);
                                    specialButton = Instantiate(specialButtonMsg, specPosition, rot, unitMenu.transform);
                                }
                            }
                            

                            yield return null;

                        }
                        else
                        {
                            Destroy(unitMenu);
                            // Calculate the spawn position above the intersected point
                            UnityEngine.Vector3 spawnPosition = positionsArray[i] + new UnityEngine.Vector3(0f, 40f, 0f);

                            // Instantiate the distanceInfoPrefab at the spawn position
                            StartCoroutine(distInfoDelayScript.SpawnAndDestroyCoroutine(distanceInfoPrefab, spawnPosition, UnityEngine.Quaternion.identity));


                            if (highlighterFlag && highlighterCount >= 10)
                            {
                                UnityEngine.Vector3[] highlighterArray = positionsArrays[0];
                                UnityEngine.Vector3 highlighterPosition = highlighterArray[0];
                                GameObject hilite = Instantiate(highlighter, highlighterPosition, UnityEngine.Quaternion.identity);
                                StartCoroutine(highlighterMover.MoveHighlighterCoroutine(hilite, positionsArrays, i, j, highlighterSpeed));
                                highlighterFlag = false;
                                highlighterCount = 0;
                            }
                            highlighterCount += 1;

                            

                            if (mouseClickCoroutine == null)
                            {
                                isReadyForMouseClick = true;
                                mouseClickCoroutine = StartCoroutine(WaitForMouseClick());
                            }
                            

                            if (isMouseClicked)
                            {
                                // change this to take fastest route and not skip across lines
                                StartCoroutine(unitMover.MoveUnitCoroutine(item, positionsArrays, i, j, moveSpeed));
                                exitLoop = true;
                                isMouseClicked = false;
                                isReadyForMouseClick = false;
                                break;
                            }

                            //yield return new WaitForSeconds(0.5f);
                            yield return null;
                        }
                        
                    }

                }
                timer += Time.deltaTime;

                yield return null;
            }

            if (unitMenuFlag == true)
            {
                UMBManager(item);
                unitMenuFlag = false;
                exitLoop = true;
                Destroy(unitMenu);
                break;
            }

            yield return null;
        }

        // Iterate through the positions array
        

        yield return null;

    }

    private IEnumerator WaitForMouseClick()
    {
        while (!isMouseClicked)
        {
            if (isReadyForMouseClick && Input.GetMouseButtonDown(0))
            {
                isMouseClicked = true;
                mouseClickCoroutine = null;
                UnityEngine.Debug.LogError("Ye");
            }

            yield return null;
        }
    }

    private void UMBManager(GameObject item)
    {
        Transform collectTransform = unitMenu.transform.GetChild(0);
        CollectButton collectScript = collectTransform.GetComponent<CollectButton>();

        Transform restTransform = unitMenu.transform.GetChild(1);
        RestButton restScript = restTransform.GetComponent<RestButton>();

        Transform sharpenTransform = unitMenu.transform.GetChild(2);
        SharpenButton sharpenScript = sharpenTransform.GetComponent<SharpenButton>();


        //rest of button flags

        ItemObjectScript itemObjectScript = item.GetComponent<ItemObjectScript>();
        ItemObject itemObject = itemObjectScript.item;

        if (collectScript.collectFlag == true)
        {
            CampObject camp = FindNearbyCamps(itemLoc);
            //logic for collection success
            camp.wood += 5;
            camp.ore += 5;

            itemObject.energy -= 25;

            StartCoroutine(DisplayCampStatChange("Wood", 5, camp));
            StartCoroutine(DisplayCampStatChange("Ore", 5, camp));

            StartCoroutine(DisplayCampStatChange("Energy", -25, null, itemObject));

            collectScript.collectFlag = false;
        }

        if (restScript.restFlag == true)
        {
            CampObject camp = FindNearbyCamps(itemLoc);
            camp.wood -= 10;
            camp.food -= 10;

            itemObject.energy += 15;
            itemObject.health += 5;
            itemObject.morale += 5;

            restScript.restFlag = false;
        }

        if (sharpenScript.sharpenFlag == true)
        {
            CampObject camp = FindNearbyCamps(itemLoc);
            camp.ore -= 10;

            itemObject.energy -= 20;

            sharpenScript.sharpenFlag = false;
        }


        //rest of button flag checks

    }

    public CampObject FindNearbyCamps(UnityEngine.Vector3 point)
    {
        CampObject closCampObject = null;
        float minDistance = float.PositiveInfinity;

        foreach (var co in camps)
        {
            float distance = UnityEngine.Vector3.Distance(co.loc, point);
            if (distance < minDistance)
            {
                closCampObject = co;
                minDistance = distance;
            }
        }

        UnityEngine.Debug.LogError(minDistance);
        if (minDistance <= 10f)
        {
            //stuff
        }
        else
        {
            CampObject camp = ScriptableObject.CreateInstance<CampObject>();
            camp.loc = point;
            AddCamp(camp);
            closCampObject = camp;

        }

        return closCampObject;
    }

    public void AddCamp(CampObject camp)
    {
        if (camp != null)
        {
            camps.Add(camp);
        }
    }

    public IEnumerator DisplayCampStatChange(string stat, int change, CampObject camp, ItemObject item = null)
    {
        boxCount += 1;
        Canvas canvas = FindObjectOfType<Canvas>();
        Image statChangeBox = Instantiate(statChangeBoxPrefab, canvas.transform);

        TMP_Text[] texts = statChangeBox.gameObject.GetComponentsInChildren<TMP_Text>();
        if (camp == null)
        {
            texts[0].text = item.itemName;
            texts[1].text = stat;
            
        }
        else if (item == null)
        {
            texts[0].text = "Camp";
            
        }
        texts[1].text = stat;
        if (change < 0)
        {
            texts[2].text = change.ToString();
            texts[2].color = Color.red;
        }
        else
        {
            texts[2].text = "+" + change.ToString();
            texts[2].color = Color.green;
        }

        //add in code that changes cololour based on camp/stat, pos/neg

        RectTransform statChangeBoxRectTransform = statChangeBox.GetComponent<RectTransform>();
        statChangeBoxRectTransform.position = new UnityEngine.Vector3(110f, 400f - (float)55*boxCount, 0f);

        yield return new WaitForSeconds(3f);

        Destroy(statChangeBox.gameObject);
        boxCount -= 1;
        yield return null;
    }

}
