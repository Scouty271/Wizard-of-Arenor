using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Raycast : MonoBehaviour
{
    public MapArenor map;

    public Player player;

    public Camera cam;

    public GameObject objects;

    public Ray ray;
    public RaycastHit hit;

    public Vector3 mousePos;

    public GameObject hittedObject;

    private void Start()
    {
        ray = new Ray();
    }

    private void Update()
    {
        ray.origin = cam.ScreenToWorldPoint(Input.mousePosition);
        ray.direction = this.transform.TransformDirection(new Vector3(0, 0, 1));

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            hittedObject = hit.collider.gameObject;
        }

        if (Input.GetMouseButton(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
        {
            player.GetComponent<AgentMovement>().destination = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }
}
