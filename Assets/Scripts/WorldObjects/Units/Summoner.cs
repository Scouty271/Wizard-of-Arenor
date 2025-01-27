using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    public Zone nearesdZone;

    public float posValueNearesdObjectToFind;

    public float workSpeed = 1;
    public float workProgress;

    public Job currJob;

    public enum miniJobs
    {
        none,
        moveToWorkstation,
        summonUnit
    }
    public miniJobs miniJob;

    private void Start()
    {
        miniJob = miniJobs.moveToWorkstation;
        GetComponent<Unit>().inZone = false;
    }

    private void Update()
    {
        Vector2 workerPos = new Vector2(transform.position.x, transform.position.y);

        float posValue;
        float posValueX;
        float posValueY;

        switch (miniJob)
        {
            case miniJobs.none:
                if (GetComponent<Unit>().assignedZoneGroup.jobs.Count > 0 && currJob == null)
                {
                    currJob = GetComponent<Unit>().assignedZoneGroup.jobs[0];
                    GetComponent<Unit>().assignedZoneGroup.jobs.Remove(currJob);

                    switch (currJob.jobType)
                    {
                        case Job.JobType.summonUnit:
                            miniJob = miniJobs.summonUnit;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (!GetComponent<Unit>().inZone)
                        miniJob = miniJobs.moveToWorkstation;
                }

                break;
            //////////////////////////////////////////////////////////////////////////////////////
            case miniJobs.moveToWorkstation:
                if (!GetComponent<Unit>().inZone)
                {
                    foreach (Zone zone in GetComponent<Unit>().assignedZoneGroup.childZones)
                    {
                        if (!zone.isWorkerPlaced)
                        {
                            posValueX = zone.transform.position.x - workerPos.x;
                            if (posValueX < 0) posValueX = -posValueX;

                            posValueY = zone.transform.position.y - workerPos.y;
                            if (posValueY < 0) posValueY = -posValueY;

                            posValue = posValueX + posValueY;

                            if (nearesdZone == null)
                            {
                                nearesdZone = zone;
                                posValueNearesdObjectToFind = posValue;
                            }

                            if (posValue < posValueNearesdObjectToFind)
                            {
                                nearesdZone.isWorkerPlaced = false;
                                nearesdZone = zone;
                                posValueNearesdObjectToFind = posValue;
                            }
                        }
                    }

                    GetComponent<AgentMovement>().destination = new Vector3(nearesdZone.transform.position.x + 0.5f, nearesdZone.transform.position.y + 0.5f, transform.position.z);
                    nearesdZone.isWorkerPlaced = true;
                }
                if (GetComponent<Unit>().inZone)
                {
                    miniJob = miniJobs.none;
                }
                break;
            ///////////////////////////////////////////////////////////////////////////////////////
            case miniJobs.summonUnit:
                workProgress += (Time.deltaTime * workSpeed);
                if (workProgress >= 10)
                {
                    //Ergebnis 
                    FindObjectOfType<UnitController>().units.Add(Instantiate(FindObjectOfType<UnitContainer_Arenor>().unit, new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity, FindObjectOfType<UnitContainer_Arenor>().transform));
                    //

                    miniJob = miniJobs.none;
                    workProgress = 0;
                    currJob = null;
                }
                break;
            default:
                break;
        }
    }
}
