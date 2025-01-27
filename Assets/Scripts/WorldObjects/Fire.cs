using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float TimeToLive;
    public float liveTime;

    private void Update()
    {
        liveTime += Time.deltaTime;

        if (liveTime > TimeToLive)
        {
            Destroy(gameObject);
        }
    }
}
