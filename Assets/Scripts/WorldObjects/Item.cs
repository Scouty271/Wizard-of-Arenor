using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum ItemTypes
    {
        hohlkraut,
        eberdorn,
        stone,
        coal,
        iron,
        wood,
        gold,
        healingPotion,
        soulstone
    }
    public ItemTypes itemType;

    public float price;
    public float size;
    public float weight;
}
