using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class PortalSpawner : MonoBehaviour
{
    public Portal portal;
    public MapArenor mapArenor;

    public BuildController buildController;

    public float timerForNextSpawn = 5;

    private void Awake()
    {
        timerForNextSpawn = 5;
    }

    private void Update()
    {
        if (timerForNextSpawn > -10)
        {
            timerForNextSpawn -= Time.deltaTime;
        }

        if (timerForNextSpawn < 0 && FindObjectOfType<QuestManager>().portaleGespawnt < 5 && !FindObjectOfType<CheatController>().noPortals)
        {
            buildController.build(portal);

            timerForNextSpawn = 5;
        }
    }
}
