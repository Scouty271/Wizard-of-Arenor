using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Zone.Profession profession;

    public ZoneGroup assignedZoneGroup;
    public ZoneGroup beforeAssignedZoneGroup;

    public bool inZone;

    public Collider collision;

    private void Start()
    {
        profession = Zone.Profession.none;
        inZone = false;
    }

    private void OnTriggerStay(Collider _collision)
    {
        collision = _collision;

        if (_collision.gameObject.GetComponentInParent<ZoneGroup>() == assignedZoneGroup)
        {
            inZone = true;
        }
        else
        {
            inZone = false;
        }
    }

    private void OnTriggerExit(Collider _collision)
    {
        if (_collision.gameObject.GetComponentInParent<ZoneGroup>() == assignedZoneGroup)
        {
            inZone = false;
        }

    }
}
