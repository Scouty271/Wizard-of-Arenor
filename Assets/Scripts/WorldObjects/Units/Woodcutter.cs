using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Woodcutter : MonoBehaviour
{
    public Zone nearesdZone;
    public Tree nearesdTree;

    public float posValueNearesdTree;

    public float workSpeed = 1;
    public float workProgress;

    public enum miniJobs
    {
        none,
        moveToWorkstation,
        moveToTree,
        fellTree
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
                    miniJob = miniJobs.moveToTree;
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
                            posValueNearesdTree = posValue;
                        }

                        if (posValue < posValueNearesdTree)
                        {
                            nearesdZone.isWorkerPlaced = false;
                            nearesdZone = zone;
                            posValueNearesdTree = posValue;
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
            ///////////////////////////////////////////////////////////////////////////////////////
            case miniJobs.moveToTree:
                nearesdZone.isWorkerPlaced = false;
                foreach (Tree tree in FindObjectOfType<MapArenor>().trees)
                {
                    if (!tree.inWorkerProcess)
                    {
                        posValueX = tree.transform.position.x - GetComponent<Unit>().assignedZoneGroup.childZones[0].transform.position.x;
                        if (posValueX < 0) posValueX = -posValueX;

                        posValueY = tree.transform.position.y - GetComponent<Unit>().assignedZoneGroup.childZones[0].transform.position.y;
                        if (posValueY < 0) posValueY = -posValueY;

                        posValue = posValueX + posValueY;

                        if (nearesdTree == null)
                        {
                            nearesdTree = tree;
                            posValueNearesdTree = posValue;
                        }

                        if (posValue < posValueNearesdTree)
                        {

                            nearesdTree = tree;
                            posValueNearesdTree = posValue;

                        }
                    }
                }

                GetComponent<AgentMovement>().destination = new Vector3(nearesdTree.transform.position.x + 0.5f, nearesdTree.transform.position.y + 0.5f, transform.position.z);

                nearesdTree.inWorkerProcess = true;
                break;
            //////////////////////////////////////////////////////////////////////////////////////
            case miniJobs.fellTree:
                workProgress += workSpeed;
                if (workProgress >= 100)
                {
                    FindObjectOfType<MapArenor>().trees.Remove(nearesdTree);

                    Destroy(nearesdTree.gameObject);

                    nearesdTree.GetComponent<NavMeshModifier>().overrideArea = false;
                    FindObjectOfType<MapArenor>().navMesh.BuildNavMesh();

                    workProgress = 0;

                    miniJob = miniJobs.none;

                    FindObjectOfType<InventarRohstoffe>().holzAnzahl += 10;
                    FindObjectOfType<EventCanvas>().refreshGoodText();
                }
                break;            
        }
    }

    private void OnTriggerStay(Collider _collision)
    {
        if (_collision.GetComponent<Tree>() != null && miniJob == miniJobs.moveToTree && nearesdTree == _collision.GetComponent<Tree>())
        {
            miniJob = miniJobs.fellTree;
        }
    }
}
