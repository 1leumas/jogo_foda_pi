using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FlashlightMinigame : MonoBehaviour
{
    public List<Transform> slots;
    public List<GameObject> itemPrefabs;
    public Text itemNameText;
    public int lives = 3;

    private List<GameObject> instantiatedItems = new List<GameObject>();
    private Queue<string> itemQueue = new Queue<string>();
    private string currentItem;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        List<Transform> availableSlots = new List<Transform>(slots);
        List<GameObject> availableItems = new List<GameObject>(itemPrefabs);

        for (int i = 0; i < 5; i++)
        {
            int slotIndex = Random.Range(0, availableSlots.Count);
            Transform chosenSlot = availableSlots[slotIndex];
            availableSlots.RemoveAt(slotIndex);

            int itemIndex = Random.Range(0, availableItems.Count);
            GameObject itemPrefab = availableItems[itemIndex];
            availableItems.RemoveAt(itemIndex);

            GameObject newItem = Instantiate(itemPrefab, chosenSlot.position, Quaternion.identity);
            newItem.transform.SetParent(chosenSlot);
            instantiatedItems.Add(newItem);

            itemQueue.Enqueue(itemPrefab.name);

            newItem.AddComponent<ItemClickable>().Init(this, itemPrefab.name);
        }

        NextItem();
    }

    public void NextItem()
    {
        if (itemQueue.Count > 0)
        {
            currentItem = itemQueue.Dequeue();
            itemNameText.text = "Find: " + currentItem;
        }
        else
        {
            Win();
        }
    }

    public void ClickItem(string itemName, ItemClickable currentItem)
    {
        if (itemName == this.currentItem)
        {
            Destroy(currentItem.gameObject);
            NextItem();
        }
        else
        {
            lives--;
            if (lives <= 0)
            {
                Lose();
            }
        }
    }

    void Win()
    {
        itemNameText.text = "You Win!";
    }

    void Lose()
    {
        itemNameText.text = "You Lose!";
    }
}
