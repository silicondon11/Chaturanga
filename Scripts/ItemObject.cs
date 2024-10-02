using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemObject : ScriptableObject
{
    //Item info
    public int itemCode;
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public GameObject itemPrefab;

    //Personnel stats
    public int health;
    public int morale;
    public int energy;
    public int courage;
    public int strength;
    public int travelSpeed;
    public int chargeSpeed;

    public int loyalty;
    public int experience;
    public int order;


    public string itemDescription = "stuff";

    public virtual string GetItemDescription()
    {
        return itemDescription;
    }
}

