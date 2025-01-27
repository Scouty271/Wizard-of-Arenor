using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandling_SecondaryProduction_Canvas : MonoBehaviour
{
    public GUI_Container guiContainer;

    public Inventory inventory;

    public Zone.Profession zoneProfession;

    private UnitController unitController;

    private void Awake()
    {
        unitController = FindObjectOfType<UnitController>();
    }

    public void onClickButtonBack()
    {
        this.gameObject.SetActive(false);
    }

    public void onClickButtonHealingPotion()
    {
        GetComponent<SecondaryProductionCanvas>().createJob(Job.JobType.createhealingpotion);
        GetComponent<SecondaryProductionCanvas>().refreshJobButtons();
    }

    public void onClickButtonSummonUnit()
    {
        GetComponent<SecondaryProductionCanvas>().createJob(Job.JobType.summonUnit);
        GetComponent<SecondaryProductionCanvas>().refreshJobButtons();
    }

    public void onClickButtonIncreaseUnit()
    {
        if (GetComponent<SecondaryProductionCanvas>().currZoneGroup.capacity < GetComponent<SecondaryProductionCanvas>().currZoneGroup.maxCapacity)
        {
            switch (zoneProfession)
            {
                case Zone.Profession.none:
                    break;
                case Zone.Profession.alchemist:
                    unitController.assignWorker(Zone.Profession.alchemist);
                    break;
                case Zone.Profession.altarShrine:
                    unitController.assignWorker(Zone.Profession.altarShrine);
                    break;
                case Zone.Profession.scientist:
                    break;
                default:
                    break;
            }
        }

        GetComponent<SecondaryProductionCanvas>().refreshUnitNumbers();
    }

    public void onClickButtonDecreaseUnit()
    {
        if (GetComponent<SecondaryProductionCanvas>().currZoneGroup.capacity > 0)
        {
            switch (zoneProfession)
            {
                case Zone.Profession.none:
                    break;
                case Zone.Profession.alchemist:
                    unitController.revokeWorker(Zone.Profession.alchemist);
                    break;
                case Zone.Profession.altarShrine:
                    unitController.revokeWorker(Zone.Profession.altarShrine);
                    break;
                case Zone.Profession.scientist:
                    break;

                default:
                    break;
            }
        }
        GetComponent<SecondaryProductionCanvas>().refreshUnitNumbers();
    }
}
