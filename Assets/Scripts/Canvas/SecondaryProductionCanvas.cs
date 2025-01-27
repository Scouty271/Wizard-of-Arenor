using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryProductionCanvas : MonoBehaviour
{
    public Text textWorkerCapacity;

    public Text textAvaliableUnits;

    public ZoneGroup currZoneGroup;

    public int counterAvaliableUnits;

    public VerticalLayoutGroup buttonList;

    public Job jobPrefab;

    public ButtonJob jobButton;
    public List<ButtonJob> jobButtonsWaiting = new List<ButtonJob>();
    public List<ButtonJob> jobButtonsInProcess = new List<ButtonJob>();

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

    public void refreshJobButtons()
    {
        foreach (var button in jobButtonsWaiting)
        {
            Destroy(button.gameObject);
        }

        jobButtonsWaiting.Clear();


        foreach (var job in currZoneGroup.GetComponent<ZoneGroup>().jobs)
        {
            jobButton.job = job;

            switch (job.jobType)
            {
                case Job.JobType.createhealingpotion:
                    jobButton.standartButtonName = "Heiltrank herstellen";
                    break;
                default:
                    break;
            }

            jobButtonsWaiting.Add(Instantiate(jobButton, buttonList.transform));
        }

    }

    public void createJob(Job.JobType type)
    {
        jobPrefab.jobType = type;
        currZoneGroup.jobs.Add(Instantiate(jobPrefab, currZoneGroup.transform));
    }
}
