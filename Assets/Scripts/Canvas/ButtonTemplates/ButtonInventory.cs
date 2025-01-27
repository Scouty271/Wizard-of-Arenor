using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInventory : MonoBehaviour
{
    public Item item;

    public string standartButtonName;

    public Text price;
    public Text size;
    public Text weight;

    //public ButtonExecute executeButton;

    private void Start()
    {
        switch (item.itemType)
        {
            case Item.ItemTypes.hohlkraut:
                standartButtonName = "Hohlkraut";
                break;
            case Item.ItemTypes.eberdorn:
                standartButtonName = "Eberdorn";
                break;
            case Item.ItemTypes.stone:
                standartButtonName = "Stein";
                break;
            case Item.ItemTypes.coal:
                standartButtonName = "Kohle";
                break;
            case Item.ItemTypes.iron:
                standartButtonName = "Eisen";
                break;
            case Item.ItemTypes.wood:
                standartButtonName = "Holz";
                break;
            case Item.ItemTypes.gold:
                standartButtonName = "Gold";
                break;
            case Item.ItemTypes.healingPotion:
                standartButtonName = "Heiltrank";
                break;
            case Item.ItemTypes.soulstone:
                standartButtonName = "Seelenstein";
                break;
            default:
                break;
        }

        GetComponentInChildren<Text>().text = standartButtonName;
    }

    private void setItemData(Item item)
    {
        price.text += item.price.ToString();
        size.text += item.size.ToString();
        weight.text += item.weight.ToString();
    }

    public void OnClick()
    {
        var buttonInventoryCanvas = FindObjectOfType<ButtonHandling_InventoryCanvas>();

        setStandartText();
        setItemData(item);

        switch (item.itemType)
        {
            case Item.ItemTypes.healingPotion:
                buttonInventoryCanvas.itemToExecute = this.item;
                buttonInventoryCanvas.executeButton.gameObject.SetActive(true);
                break;
                default:
                buttonInventoryCanvas.executeButton.gameObject.SetActive(false);
                break;
        }
    }

    public void executeItem()
    {
        switch (item.itemType)
        {
            case Item.ItemTypes.healingPotion:
                FindObjectOfType<Player>().health += 10;
                break;
        }
    }

    private void setStandartText()
    {
        price.text = "Preis: ";
        size.text = "Größe: ";
        weight.text = "Gewicht: ";
    }
}
