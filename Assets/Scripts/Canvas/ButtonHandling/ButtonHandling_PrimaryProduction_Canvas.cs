using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandling_PrimaryProduction_Canvas : MonoBehaviour
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
        gameObject.SetActive(false);
    }

    public void onClickButtonIncreaseUnit()
    {
        if (GetComponent<PrimaryProductionCanvas>().currZoneGroup.capacity < GetComponent<PrimaryProductionCanvas>().currZoneGroup.maxCapacity)
        {
            switch (zoneProfession)
            {
                case Zone.Profession.none:
                    break;
                case Zone.Profession.woodcutter:
                    unitController.assignWorker(Zone.Profession.woodcutter);
                    break;
                case Zone.Profession.mason:
                    unitController.assignWorker(Zone.Profession.mason);
                    break;
                default:
                    break;
            }
            GetComponent<PrimaryProductionCanvas>().refreshUnitNumbers();
        }
    }

    public void onClickButtonDecreaseUnit()
    {
        if (GetComponent<PrimaryProductionCanvas>().currZoneGroup.capacity > 0)
        {
            switch (zoneProfession)
            {
                case Zone.Profession.none:
                    break;
                case Zone.Profession.woodcutter:
                    unitController.revokeWorker(Zone.Profession.woodcutter);
                    break;
                case Zone.Profession.mason:
                    unitController.revokeWorker(Zone.Profession.mason);
                    break;
                default:
                    break;
            }

            GetComponent<PrimaryProductionCanvas>().refreshUnitNumbers();
        }
    }
}
