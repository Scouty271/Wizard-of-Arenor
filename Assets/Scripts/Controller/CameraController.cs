using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    public Player player;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cam.transform.position.z);
    }
}
