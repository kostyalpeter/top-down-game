using System.Collections.Generic;
using UnityEngine;

public class InventoryContoller : MonoBehaviour
{
    private ItemDictionary itemDictionary;
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    void Awake()
    {
        Instance = this;
    }
    public static InventoryContoller Instance { get; internal set; }

    [System.Obsolete]
    void Start()
    {
        inventoryPanel.SetActive(false);

        SetupInventory();
    }

    [System.Obsolete]
    void SetupInventory()
    {
        if (inventoryPanel.transform.childCount > 0)
        {
            for (int i = inventoryPanel.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(inventoryPanel.transform.GetChild(i).gameObject);
            }
        }

        itemDictionary = FindObjectOfType<ItemDictionary>();

        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        Item ItemToAdd = itemPrefab.GetComponent<Item>();
        if (ItemToAdd == null) return false;

        foreach (Transform slotTranform in inventoryPanel.transform)
        {
            Slot slot = slotTranform.GetComponent<Slot>();
            if (slot != null && slot.currentItem != null)
            {
                Item slotItem = slot.currentItem.GetComponent<Item>();
                if (slotItem != null && slotItem.ID == ItemToAdd.ID)
                {
                    slotItem.AddToStack(1);
                    return true;
                }
            }
        }

        foreach (Transform slotTranform in inventoryPanel.transform)
        {
            Slot slot = slotTranform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newitem = Instantiate(itemPrefab, slot.transform);
                newitem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newitem;
                return true;
            }
        }

        return false;
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                invData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex(), quantity = item.quantity });

            }
        }
        return invData;
    }

    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        foreach (InventorySaveData data in inventorySaveData)
        {
            if (data.slotIndex < slotCount)
            {
                Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                    Item itemComponent = item.GetComponent<Item>();
                    if (itemComponent != null && data.quantity > 1)
                    {
                        itemComponent.quantity = data.quantity;
                        var updateMethod = itemComponent.GetType().GetMethod("UpdateQuantityDisplay", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                        if (updateMethod != null)
                        {
                            updateMethod.Invoke(itemComponent, null);
                        }
                    }
                    slot.currentItem = item;
                }
            }
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    public void LevelUp()
    {
        slotCount += 1;
        ExpandInventory();
    }

    private void ExpandInventory()
    {
        int currentSlots = inventoryPanel.transform.childCount;
        int slotsToAdd = slotCount - currentSlots;

        for (int i = 0; i < slotsToAdd; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }
    }
}
