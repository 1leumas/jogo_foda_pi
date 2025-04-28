using UnityEngine;

public class ItemClickable : MonoBehaviour
{
    private FlashlightMinigame manager;
    private string itemName;

    public void Init(FlashlightMinigame manager, string itemName)
    {
        this.manager = manager;
        this.itemName = itemName;
    }

    private void OnMouseDown()
    {
        manager.ClickItem(itemName, this);
    }
}
