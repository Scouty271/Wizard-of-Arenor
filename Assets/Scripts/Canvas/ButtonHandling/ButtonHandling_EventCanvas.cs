using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandling_EventCanvas : MonoBehaviour
{
    public Player player;

    public GUI_Container guiContainer;

    public Button buttonPickUp;
    public Button buttonTravelToVaremar;
    public Button buttonWorkBench;
    public Button buttonCreateUnit;

    private void Update()
    {
        if (player.canPickUp)
            buttonPickUp.gameObject.SetActive(true);
        else
            buttonPickUp.gameObject.SetActive(false);


        if (player.canOpenWorkbench)
            buttonWorkBench.gameObject.SetActive(true);
        else
            buttonWorkBench.gameObject.SetActive(false);
    }

    public void OnClickButtonPickup()
    {
        player.doPickUp = true;
    }

    public void OnClickOpenZone()
    {
        var profession = player.collision.GetComponent<Zone>().profession;

        //Primary
        if (profession == Zone.Profession.woodcutter)
            openZonePrimary(guiContainer.canvasWoodcutter);

        if (profession == Zone.Profession.mason)
            openZonePrimary(guiContainer.canvasMason);


        //Secondary
        if (profession == Zone.Profession.alchemist)
            openZoneSecondary(guiContainer.canvasAlchemist);

        if (profession == Zone.Profession.altarShrine)
            openZoneSecondary(guiContainer.canvasAltarShrine);
    }

    private void openZonePrimary(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);

        canvas.GetComponent<PrimaryProductionCanvas>().refreshUnitNumbers(FindObjectOfType<PlayerTriggerHandling>().enteredZonegroup);
    }

    private void openZoneSecondary(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);

        canvas.GetComponent<SecondaryProductionCanvas>().refreshUnitNumbers(FindObjectOfType<PlayerTriggerHandling>().enteredZonegroup);
        canvas.GetComponent<SecondaryProductionCanvas>().refreshJobButtons();
    }
}
