using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventarRohstoffe : MonoBehaviour
{
    public int holzAnzahl;
    public int steinAnzahl;
    public int eisenAnzahl;

    private void Update()
    {
        if (FindObjectOfType<CheatController>().giveGoods)
        {
            holzAnzahl = 10000;
            steinAnzahl = 10000;
            eisenAnzahl = 10000;
        }
    }
}
