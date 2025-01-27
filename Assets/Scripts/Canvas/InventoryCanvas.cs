using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvas : MonoBehaviour
{
    public Player player;
    public VerticalLayoutGroup buttonList;

    public ButtonInventory inventoryItemButton;

    public List<ButtonInventory> inventoryItemButtons = new List<ButtonInventory>();

    public void refreshInventory()
    {
        foreach (var button in inventoryItemButtons)
        {
            Destroy(button.gameObject);
        }

        inventoryItemButtons.Clear();

        foreach (var item in FindObjectOfType<Inventory>().items)
        {
            inventoryItemButton.GetComponent<ButtonInventory>().item = item;

            createInventoryButton(item);
        }
    }

    private void createInventoryButton(Item item)
    {
        inventoryItemButton.standartButtonName = item.name;

        inventoryItemButtons.Add(Instantiate(inventoryItemButton, buttonList.transform));
    }
}
