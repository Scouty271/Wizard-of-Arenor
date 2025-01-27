using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PrimaryProductionCanvas : MonoBehaviour
{
    public Text textWorkerCapacity;

    public Text textAvaliableUnits;

    public ZoneGroup currZoneGroup;

    public int counterAvaliableUnits;

    public void refreshUnitNumbers(ZoneGroup enteredZG)
    {
        currZoneGroup = enteredZG;

        foreach (Zone zone in enteredZG.childZones)
        {
            enteredZG.maxCapacity++;
        }
        enteredZG.maxCapacity /= 4;


        counterAvaliableUnits = FindObjectOfType<UnitController>().getAvaliableUnits();

        textAvaliableUnits.text = counterAvaliableUnits.ToString();
        textWorkerCapacity.text = enteredZG.capacity.ToString() + " / " + enteredZG.maxCapacity.ToString();
    }

    public void refreshUnitNumbers()
    {
        counterAvaliableUnits = FindObjectOfType<UnitController>().getAvaliableUnits();

        textAvaliableUnits.text = counterAvaliableUnits.ToString();
        textWorkerCapacity.text = currZoneGroup.capacity.ToString() + " / " + currZoneGroup.maxCapacity.ToString();
    }
}
