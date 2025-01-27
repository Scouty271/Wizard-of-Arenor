using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public enum States
    {
        Idle,
        Attack
    }
    public States state;

    private void Start()
    {
        state = States.Idle;
    }

    private void Update()
    {
        var value = Random.Range(0, 100);

        var moveToX = Random.Range(-5, 6);
        var moveToY = Random.Range(-5, 6);

        if (value == 1)
        {
            GetComponent<AgentMovement>().destination = new Vector3(transform.position.x + moveToX, transform.position.y + moveToY, transform.position.z);
        }
    }
}
