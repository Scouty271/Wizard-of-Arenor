using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentMovement : MonoBehaviour
{
    public Vector3 destination;
    NavMeshAgent agent;

    void Start()
    {
        destination = transform.position;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (agent.destination.x != destination.x || agent.destination.y != destination.y && agent.isActiveAndEnabled)
        {
            agent.SetDestination(destination);
        }
    }
}
