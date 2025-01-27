using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventCanvas : MonoBehaviour
{
    public InventarRohstoffe inventarRohstoffe;

    public Text holzAnzahlText;
    public Text steinAnzahlText;
    public Text eisenAnzahlText;

    public Text healthAnzahlText;
    public Text manaAnzahlText;
    public Text skillPointAnzahlText;

    private void Awake()
    {
        refreshAttributeText();
        refreshGoodText();
    }

    private void Start()
    {
        refreshAttributeText();
        refreshGoodText();
    }

    public void refreshGoodText()
    {
        holzAnzahlText.text = inventarRohstoffe.holzAnzahl.ToString();
        steinAnzahlText.text = inventarRohstoffe.steinAnzahl.ToString();
        eisenAnzahlText.text = inventarRohstoffe.eisenAnzahl.ToString();
    }

    public void refreshAttributeText()
    {
        healthAnzahlText.text = FindObjectOfType<Player>().health.ToString();
        manaAnzahlText.text = FindObjectOfType<Player>().mana.ToString();
        skillPointAnzahlText.text = FindObjectOfType<Player>().skillPoints.ToString();
    }
}
