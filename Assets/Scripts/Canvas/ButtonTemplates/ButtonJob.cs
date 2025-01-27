using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class ButtonJob : MonoBehaviour
{
    public Job job;

    public string standartButtonName;

    private void Start()
    {
        switch (job.jobType)
        {
            case Job.JobType.createhealingpotion:
                standartButtonName = "Heiltrank herstellen";
                break;
            case Job.JobType.summonUnit:
                standartButtonName = "Einheit beschwören";
                break;
            default:
                break;
        }

        GetComponentInChildren<Text>().text = standartButtonName;
    }

    public void OnClick()
    {
        FindObjectOfType<SecondaryProductionCanvas>().jobButtonsWaiting.Remove(this);
        

        Destroy(job);
        Destroy(gameObject);
    }
}
