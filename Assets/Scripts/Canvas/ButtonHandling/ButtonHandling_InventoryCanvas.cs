using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHandling_InventoryCanvas : MonoBehaviour
{
    public GUI_Container guiContainer;

    public Item itemToExecute;

    public Button executeButton;

    public void OnClickButtonBack()
    {
        guiContainer.canvasMain.gameObject.SetActive(true);
        guiContainer.canvasEvent.gameObject.SetActive(true);
        guiContainer.canvasInventory.gameObject.SetActive(false);
    }

    public void onClickButtonExecute()
    {
        switch (itemToExecute.itemType)
        {
            case Item.ItemTypes.healingPotion:
                FindObjectOfType<Player>().healHealthpoints(10);

                FindObjectOfType<Inventory>().items.Remove(itemToExecute);
                FindObjectOfType<InventoryCanvas>().refreshInventory();
                FindObjectOfType<EventCanvas>().refreshAttributeText();
                FindObjectOfType<EventCanvas_HealthBar>().slider.value = FindObjectOfType<Player>().health;
                FindObjectOfType<ButtonHandling_InventoryCanvas>().executeButton.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
