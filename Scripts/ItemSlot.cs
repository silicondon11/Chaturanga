using System;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ItemSlot : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemObject item;
    public GameObject[] validTargets;
    public string unreachableAreaMessage = "Unit deployed in unreachable area";
    public Image itemInfoPanelPrefab;
    private Image itemInfoPanel;
    private GameObject distanceInfo;

    private RectTransform rectTransform;
    bool validTargetHit = false;
    private bool isDistance = false;
    private bool isSuspended;

    // Unit moving variables
    private int pCount;
    private float unitThreshold = 40f;

    private GameObject unitHolder;

    private Inventory inventoryScript;
    public GameObject inventory;
    public Sprite itemNumberImage;
    private RectTransform INIRectTransform;
    private TextMeshProUGUI textBox;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        inventoryScript = inventory.GetComponent<Inventory>();

    }

    public void AssignItem(ItemObject item, int itemCount)
    {
        this.item = item;
        GetComponent<UnityEngine.UI.Image>().sprite = item.itemImage;

        if (itemCount > 1)
        {
            GameObject INIgameObject = new GameObject("ItemNumberImage");
            INIgameObject.transform.SetParent(transform);
            INIgameObject.transform.localPosition = UnityEngine.Vector3.zero;

            Image ini = INIgameObject.AddComponent<Image>();
            ini.sprite = itemNumberImage;

            INIRectTransform = ini.GetComponent<RectTransform>();
            INIRectTransform.anchorMin = new UnityEngine.Vector2(1f, 1f);
            INIRectTransform.anchorMax = new UnityEngine.Vector2(1f, 1f);
            INIRectTransform.pivot = new UnityEngine.Vector2(1f, 1f);
            INIRectTransform.sizeDelta = new UnityEngine.Vector2(15f, 15f);

            GameObject textBoxGO = new GameObject("TextBox");
            textBoxGO.transform.SetParent(ini.transform);
            textBoxGO.transform.localPosition = UnityEngine.Vector3.zero;
            textBox = textBoxGO.AddComponent<TextMeshProUGUI>();
            string itemCountString = itemCount.ToString();
            textBox.text = itemCountString;
            textBox.alignment = TextAlignmentOptions.Center;
            //textBox.font = Resources.Load<TMP_FontAsset>("YourFontAssetName");
            textBox.fontSize = 9f;
            textBox.rectTransform.sizeDelta = INIRectTransform.sizeDelta;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (Time.timeScale != 0)
        {
            if (item != null)
            {
                // Set the position of the item icon to the mouse position
                rectTransform.position = Input.mousePosition;
            }
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Time.timeScale != 0)
        {
            if (item != null)
            {
                // Raycast from the mouse position to check if it hits any valid targets
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (item.itemType == ItemType.Personnel)
                    {
                        foreach (GameObject target in validTargets)
                        {
                            if (UnityEngine.Vector3.Distance(hit.collider.gameObject.transform.position, target.transform.position) <= unitThreshold)
                            {
                                unitHolder = Instantiate(item.itemPrefab, target.transform.position + new UnityEngine.Vector3(0, 30, 0), UnityEngine.Quaternion.Euler(90, 0, 0));

                                ItemObjectScript itemObject = unitHolder.GetComponent<ItemObjectScript>();
                                if (!itemObject)
                                {
                                    itemObject = unitHolder.AddComponent<ItemObjectScript>();
                                }
                                itemObject.item = item;

                                UnityEngine.Debug.LogError("Please select a destination for this unit");

                                ItemSlot itemSlot = GetComponent<ItemSlot>();


                                StartCoroutine(inventoryScript.DeleteItemSlot(itemSlot, unitHolder));

                                validTargetHit = true;
                                break;
                            }
                        }
                    }
                    else if (item.itemType == ItemType.Strategies)
                    {

                    }
                    else if (item.itemType == ItemType.Resources)
                    {

                    }


                    if (!validTargetHit)
                    {
                        UnityEngine.Debug.LogError(unreachableAreaMessage);
                    }
                }

                else
                {
                    UnityEngine.Debug.LogError(unreachableAreaMessage);
                }

                // Reset the position of the item icon
                rectTransform.anchoredPosition = UnityEngine.Vector2.zero;
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Time.timeScale != 0)
        {
            if (item != null)
            {
                if (!isDistance)
                {
                    // Display item info panel with the name of the item
                    Canvas canvas = FindObjectOfType<Canvas>();
                    if (canvas != null)
                    {
                        // Display item info panel with the name of the item
                        itemInfoPanel = Instantiate(itemInfoPanelPrefab, canvas.transform);

                        //set tag of item so on inventory hide, any object with this tag can be found and deleted

                        RectTransform itemInfoRectTransform = itemInfoPanel.GetComponent<RectTransform>();
                        itemInfoRectTransform.position = rectTransform.position - new UnityEngine.Vector3(220f, 0f, 0f);
                    }
                }

            }
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemInfoPanel != null)
        {
            // Destroy item info panel when the mouse exits the item slot
            Destroy(itemInfoPanel);
        }
        if (distanceInfo != null)
        {
            Destroy(distanceInfo);
        }
    }

    


}

