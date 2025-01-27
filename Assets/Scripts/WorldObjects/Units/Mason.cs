using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mason : MonoBehaviour
{
    public Zone nearesdZone;
    public Wall nearesdWall;

    public float posValueNearesdWall;

    public float workSpeed = 1;
    public float workProgress;

    public enum miniJobs
    {
        none,
        moveToWorkstation,
        moveToStone,
        cutStone
    }
    public miniJobs miniJob;

    private void Start()
    {
        miniJob = miniJobs.moveToWorkstation;
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
                if (GetComponent<Unit>().inZone)
                {
                    miniJob = miniJobs.moveToStone;
                }
                else
                {
                    miniJob = miniJobs.moveToWorkstation;
                }

                break;
            //////////////////////////////////////////////////////////////////////////////////////
            case miniJobs.moveToWorkstation:
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
                            posValueNearesdWall = posValue;
                        }

                        if (posValue < posValueNearesdWall)
                        {
                            nearesdZone.isWorkerPlaced = false;
                            nearesdZone = zone;
                            posValueNearesdWall = posValue;
                        }
                    }
                }

                GetComponent<AgentMovement>().destination = new Vector3(nearesdZone.transform.position.x + 0.5f, nearesdZone.transform.position.y + 0.5f, transform.position.z);
                nearesdZone.isWorkerPlaced = true;

                if (GetComponent<Unit>().inZone)
                {
                    miniJob = miniJobs.none;
                }

                break;
            /////////////////////////////////////////////////////////////////////////////////////
            case miniJobs.moveToStone:
                nearesdZone.isWorkerPlaced = false;
                foreach (Wall wall in FindObjectOfType<MapArenor>().walls)
                {
                    if (!wall.inWorkerProcess)
                    {
                        posValueX = wall.transform.position.x - GetComponent<Unit>().assignedZoneGroup.childZones[0].transform.position.x;
                        if (posValueX < 0) posValueX = -posValueX;

                        posValueY = wall.transform.position.y - GetComponent<Unit>().assignedZoneGroup.childZones[0].transform.position.y;
                        if (posValueY < 0) posValueY = -posValueY;

                        posValue = posValueX + posValueY;

                        if (nearesdWall == null)
                        {
                            nearesdWall = wall;
                            posValueNearesdWall = posValue;
                        }

                        if (posValue < posValueNearesdWall)
                        {

                            nearesdWall = wall;
                            posValueNearesdWall = posValue;

                        }
                    }
                }
                GetComponent<AgentMovement>().destination = new Vector3(nearesdWall.transform.position.x + 0.5f, nearesdWall.transform.position.y + 0.5f, transform.position.z);

                nearesdWall.inWorkerProcess = true;
                break;
            case miniJobs.cutStone:
                workProgress += workSpeed;
                if (workProgress >= 100)
                {
                    FindObjectOfType<MapArenor>().walls.Remove(nearesdWall);

                    Destroy(nearesdWall.gameObject);

                    nearesdWall.GetComponent<NavMeshModifier>().overrideArea = false;
                    FindObjectOfType<MapArenor>().navMesh.BuildNavMesh();

                    workProgress = 0;

                    miniJob = miniJobs.none;

                    FindObjectOfType<InventarRohstoffe>().steinAnzahl += 10;

                    FindObjectOfType<EventCanvas>().refreshGoodText();
                }
                break;

            default:
                break;
        }
    }

    private void OnTriggerStay(Collider _collision)
    {
        if (_collision.GetComponent<Wall>() != null && miniJob == miniJobs.moveToStone && nearesdWall == _collision.GetComponent<Wall>())
        {
            miniJob = miniJobs.cutStone;
        }
    }
}
