using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public List<Unit> units;

    public UnitContainer_Arenor unitContainer;

    private void Start()
    {
        //Temp
        spawnUnit(new Vector3(21, 19, -0.5f), unitContainer.transform);
    }

    public void spawnUnit(Vector3 pos, Transform parent)
    {
        units.Add(Instantiate(unitContainer.unit, pos, Quaternion.identity, parent));
    }

    public int getAvaliableUnits()
    {
        int unitCounter = 0;

        foreach (var unit in units)
            if (unit.profession == Zone.Profession.none)
            {
                unitCounter++;
            }

        return unitCounter;
    }

    public void assignWorker(Zone.Profession profession)
    {
        foreach (var unit in units)
        {
            if (unit.profession == Zone.Profession.none)
            {
                switch (profession)
                {
                    case Zone.Profession.none:
                        break;

                    //Primary
                    case Zone.Profession.woodcutter:
                        FindObjectOfType<PrimaryProductionCanvas>().currZoneGroup.capacity++;
                        unit.assignedZoneGroup = FindObjectOfType<PrimaryProductionCanvas>().currZoneGroup;

                        assignSpecificWorker(unit, Zone.Profession.woodcutter);

                        if (unit.GetComponent<Woodcutter>() == null)
                            unit.gameObject.AddComponent<Woodcutter>();
                        break;

                    case Zone.Profession.mason:
                        FindObjectOfType<PrimaryProductionCanvas>().currZoneGroup.capacity++;
                        unit.assignedZoneGroup = FindObjectOfType<PrimaryProductionCanvas>().currZoneGroup;

                        assignSpecificWorker(unit, Zone.Profession.mason);

                        if (unit.GetComponent<Mason>() == null)
                            unit.gameObject.AddComponent<Mason>();
                        break;

                    //Secondary
                    case Zone.Profession.alchemist:
                        FindObjectOfType<SecondaryProductionCanvas>().currZoneGroup.capacity++;
                        unit.assignedZoneGroup = FindObjectOfType<SecondaryProductionCanvas>().currZoneGroup;

                        assignSpecificWorker(unit, Zone.Profession.alchemist);

                        if (unit.GetComponent<Alchemist>() == null)
                            unit.gameObject.AddComponent<Alchemist>();
                        break;

                    case Zone.Profession.altarShrine:
                        FindObjectOfType<SecondaryProductionCanvas>().currZoneGroup.capacity++;
                        unit.assignedZoneGroup = FindObjectOfType<SecondaryProductionCanvas>().currZoneGroup;

                        assignSpecificWorker(unit, Zone.Profession.altarShrine);

                        if (unit.GetComponent<Summoner>() == null)
                            unit.gameObject.AddComponent<Summoner>();
                        break;

                    default:
                        break;
                }
                break;
            }
        }
    }

    public void revokeWorker(Zone.Profession profession)
    {
        foreach (var unit in units)
        {
            if (unit.profession == profession)
            {
                switch (unit.profession)
                {
                    case Zone.Profession.none:
                        break;

                    case Zone.Profession.woodcutter:
                        unit.GetComponent<Woodcutter>().nearesdTree.inWorkerProcess = false;
                        unit.GetComponent<Woodcutter>().nearesdZone.isWorkerPlaced = false;
                        Destroy(unit.GetComponent<Woodcutter>());
                        break;

                    case Zone.Profession.mason:
                        unit.GetComponent<Mason>().nearesdWall.inWorkerProcess = false;
                        unit.GetComponent<Mason>().nearesdZone.isWorkerPlaced = false;
                        Destroy(unit.GetComponent<Mason>());
                        break;

                    case Zone.Profession.alchemist:
                        unit.GetComponent<Alchemist>().nearesdZone.isWorkerPlaced = false;
                        Destroy(unit.GetComponent<Alchemist>());
                        break;

                    case Zone.Profession.altarShrine:
                        unit.GetComponent<Summoner>().nearesdZone.isWorkerPlaced = false;
                        Destroy(unit.GetComponent<Summoner>());
                        break;

                    case Zone.Profession.fighter:
                        break;

                    default:
                        break;
                }

                revokeSpecificWorker(unit);

                break;
            }
        }
    }

    private void assignSpecificWorker(Unit unit, Zone.Profession profession)
    {
        unit.assignedZoneGroup.units.Add(unit);
        unit.beforeAssignedZoneGroup = unit.assignedZoneGroup;
        unit.profession = profession;
    }

    private void revokeSpecificWorker(Unit unit)
    {
        unit.assignedZoneGroup.capacity--;
        unit.assignedZoneGroup.units.Remove(unit);
        unit.assignedZoneGroup = null;
        unit.profession = Zone.Profession.none;
        unit.inZone = false;
    }
}
