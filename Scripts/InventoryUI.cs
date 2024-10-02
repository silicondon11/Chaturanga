using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;

    private bool inventoryOpen = false;

    void Update()
    {

        if (Time.timeScale != 0f)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryOpen)
                {
                    inventoryPanel.SetActive(false);
                    inventoryOpen = false;

                    //destroy any remaining itemInfoPrefabs
                }
                else
                {
                    inventoryPanel.SetActive(true);
                    inventoryOpen = true;
                }
            }
        }

        
    }

}
