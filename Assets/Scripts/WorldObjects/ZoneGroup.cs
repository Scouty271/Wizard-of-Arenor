using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGroup : MonoBehaviour
{
	public Zone.Profession profession;

	public List<Zone> childZones;

	public List<Unit> units;

	public List<Job> jobs;

    public int maxCapacity;
    public int capacity;
	public void addJob(Job job)
	{
        job.gameObject.SetActive(false);
		jobs.Add(Instantiate(job, gameObject.transform));
	}
}
