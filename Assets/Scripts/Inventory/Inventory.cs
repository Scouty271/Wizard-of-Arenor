using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public InventarRohstoffe inventoryGoods;

    public void addItem(Item _item)
    {
        _item.gameObject.SetActive(false);

        switch (_item.itemType)
        {
            case Item.ItemTypes.stone:
                inventoryGoods.steinAnzahl += 10;
                break;
            case Item.ItemTypes.iron:
                inventoryGoods.eisenAnzahl += 10;
                break;
            case Item.ItemTypes.wood:
                inventoryGoods.holzAnzahl += 10;
                break;
            default:
                items.Add(Instantiate(_item, gameObject.transform));
                break;
        }

        FindObjectOfType<EventCanvas>().refreshGoodText();
    }
}
