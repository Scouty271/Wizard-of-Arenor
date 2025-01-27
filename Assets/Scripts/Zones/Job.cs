using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : MonoBehaviour
{
    public enum JobType
    {
        createhealingpotion,
        //Scientist
        research_StonePick,

        //Summoner
        summonUnit

    }
    public JobType jobType;

    public Job(JobType type)
    {
        jobType = type;
    }
}
