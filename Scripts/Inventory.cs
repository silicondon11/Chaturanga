using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<ItemObject> items = new List<ItemObject>();

    private List<ItemObject> personnelItems = new List<ItemObject>();
    private List<ItemObject> strategyItems = new List<ItemObject>();
    private List<ItemObject> resourceItems = new List<ItemObject>();

    public int inventorySize = 20;
    public GameObject itemSlotPrefab;
    public Transform contentPanel;
    private List<GameObject> itemSlots = new List<GameObject>();

    public Button personnelTabButton;
    public Button strategyTabButton;
    public Button resourceTabButton;

    public GameObject distanceMgr;
    private DistanceManager distanceManager;

    private ItemObject prevItem;

    void Start()
    {

        distanceManager = distanceMgr.GetComponent<DistanceManager>();

        personnelTabButton.onClick.AddListener(SwitchToPersonnelTab);
        strategyTabButton.onClick.AddListener(SwitchToStrategiesTab);
        resourceTabButton.onClick.AddListener(SwitchToResourcesTab);

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemType == ItemType.Personnel)
            {
                personnelItems.Add(items[i]);
            }

            else if (items[i].itemType == ItemType.Strategies)
            {
                strategyItems.Add(items[i]);
            }

            else if (items[i].itemType == ItemType.Resources)
            {
                resourceItems.Add(items[i]);
            }
        }

        // Initialize the inventory UI to display the Personnel tab
        SwitchToPersonnelTab();
    }

    public IEnumerator DeleteItemSlot(ItemSlot itemSlot, GameObject unitHolder)
    {
        // Find the index of the itemSlot in the itemSlots list
        int index = itemSlots.IndexOf(itemSlot.gameObject);

        if (index != -1)
        {
            // Remove the itemSlot from the itemSlots list
            itemSlots.RemoveAt(index);

            // Remove the associated item from the items list
            items.RemoveAt(index);

            if (itemSlot.item.itemType == ItemType.Personnel)
            {
                // Remove the item from the personnelItems list
                personnelItems.Remove(itemSlot.item);

                SwitchToPersonnelTab();

                yield return StartCoroutine(distanceManager.DistanceManagerCoroutine(unitHolder));
            }
            else if (itemSlot.item.itemType == ItemType.Strategies)
            {
                // Remove the item from the strategyItems list
                strategyItems.Remove(itemSlot.item);
            }
            else if (itemSlot.item.itemType == ItemType.Resources)
            {
                // Remove the item from the resourceItems list
                resourceItems.Remove(itemSlot.item);
            }

            // Destroy the itemSlot object
            Destroy(itemSlot.gameObject);
        }

        // Wait for the end of the frame
        yield return new WaitForEndOfFrame();
    }


    public void SwitchToPersonnelTab()
    {
        if (Time.timeScale != 0) {
            // Set the Personnel tab button to active and the others to inactive
            personnelTabButton.interactable = false;
            strategyTabButton.interactable = true;
            resourceTabButton.interactable = true;

            // Display the Personnel category of items in the inventory UI
            DisplayItems(personnelItems);
        }
        
    }

    public void SwitchToStrategiesTab()
    {
        if (Time.timeScale != 0)
        {
            // Set the Strategies tab button to active and the others to inactive
            personnelTabButton.interactable = true;
            strategyTabButton.interactable = false;
            resourceTabButton.interactable = true;

            // Display the Strategies category of items in the inventory UI
            DisplayItems(strategyItems);
        }
    }

    public void SwitchToResourcesTab()
    {
        if (Time.timeScale != 0)
        {
            // Set the Resources tab button to active and the others to inactive
            personnelTabButton.interactable = true;
            strategyTabButton.interactable = true;
            resourceTabButton.interactable = false;

            // Display the Resources category of items in the inventory UI
            DisplayItems(resourceItems);
        }
    }

    public void DisplayItems(List<ItemObject> itemsToDisplay)
    {
        // Clear the existing item slots from the inventory UI
        foreach (GameObject itemSlot in itemSlots)
        {
            Destroy(itemSlot);
        }
        itemSlots.Clear();

        int itemCount = 0;
        // Instantiate a new item slot for each item in the selected category
        foreach (ItemObject item in itemsToDisplay)
        {
            if (prevItem == null)
            {
                prevItem = item;
                itemCount++;
                continue; // Skip the first item
            }

            if (item.itemCode == prevItem.itemCode)
            {
                itemCount += 1;
            }
            else
            {
                if (itemCount > 0)
                {
                    GameObject newItemSlot = Instantiate(itemSlotPrefab, contentPanel);
                    newItemSlot.GetComponent<ItemSlot>().AssignItem(prevItem, itemCount);
                    itemSlots.Add(newItemSlot);
                }
                
                prevItem = item;
                itemCount = 1;
            }
        }

        if (prevItem != null && itemCount > 0)
        {
            GameObject newItemSlot = Instantiate(itemSlotPrefab, contentPanel);
            newItemSlot.GetComponent<ItemSlot>().AssignItem(prevItem, itemCount);
            itemSlots.Add(newItemSlot);
        }
    }

}


