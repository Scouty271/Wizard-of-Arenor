using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerHandling : MonoBehaviour
{
   private Player player;

    public ZoneGroup enteredZonegroup;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void OnTriggerStay(Collider _collision)
    {
        if (_collision.gameObject.GetComponent<Item>())
        {
            player.collision = _collision;
            player.canPickUp = true;
        }

        if (_collision.gameObject.GetComponent<Zone>())
        {
            player.collision = _collision;
            player.canOpenWorkbench = true;

            enteredZonegroup = _collision.gameObject.GetComponent<Zone>().GetComponentInParent<ZoneGroup>();
        }
    }

    private void OnTriggerExit(Collider _collision)
    {
        if (_collision.gameObject.GetComponent<Item>())
        {
            player.collision = null;
            player.canPickUp = false;
        }

        if (_collision.gameObject.GetComponent<Zone>())
        {
            player.collision = null;
            player.canOpenWorkbench = false;

            enteredZonegroup = null;
        }
    }
}
