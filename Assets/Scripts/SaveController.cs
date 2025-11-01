using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryContoller inventoryContoller;

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "savefile.json");
        inventoryContoller = FindObjectOfType<InventoryContoller>();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            inventorySaveData = inventoryContoller.GetInventoryItems()
        };
    }

    public void Loadgame()
    {
        if (File.Exists(saveLocation))
        {
            string json = File.ReadAllText(saveLocation);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            inventoryContoller.SetInventoryItems(saveData.inventorySaveData);
        }
        else
        {
            SaveGame();
        }
    }
}


